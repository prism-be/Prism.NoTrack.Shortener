// -----------------------------------------------------------------------
//  <copyright file="Program.cs" company="Prism">
//  Copyright (c) Prism. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

using LiteDB;

using Microsoft.Azure.Cosmos;

using Prism.NoTrack.Shortener.Model;

[assembly: ExcludeFromCodeCoverage]

var cosmosDbConnectionString = Environment.GetEnvironmentVariable("COSMOSDB_CONNECTIONSTRING");
var liteDbConnectionString = Environment.GetEnvironmentVariable("LITEDB_CONNECTIONSTRING");

using var cosmosClient = new CosmosClient(cosmosDbConnectionString);
var database = cosmosClient.GetDatabase("shortener");
var container = database.GetContainer("redirections");
var queryDefinition = new QueryDefinition("SELECT * FROM c");

var iterator = container.GetItemQueryIterator<dynamic>(queryDefinition);

var results = new List<dynamic>();
while (iterator.HasMoreResults)
{
    var result = await iterator.ReadNextAsync();
    results.AddRange(result.Resource);
}

foreach (var result in results)
{
    using var liteDatabase = new LiteDatabase(liteDbConnectionString);
    var collection = liteDatabase.GetCollection<Redirection>("customers");

    var redirection = new Redirection(result.id.ToString(), result.longUrl.ToString());

    collection.Insert(redirection);
    collection.EnsureIndex(x => x.Id);
}