// -----------------------------------------------------------------------
//  <copyright file="GetDocumentsCount.cs" company="Prism">
//  Copyright (c) Prism. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Prism.NoTrack.Shortener.Queries;

using LiteDB;

using MediatR;

using Microsoft.Extensions.Options;

using Prism.NoTrack.Shortener.Model;
using Prism.NoTrack.Shortener.Options;

public record GetDocumentsCount : IRequest<int>;

public class GetDocumentsCountHandler : IRequestHandler<GetDocumentsCount, int>
{
    private readonly ShortenerConfiguration configuration;

    public GetDocumentsCountHandler(IOptions<ShortenerConfiguration> configuration)
    {
        this.configuration = configuration.Value;
    }

    public Task<int> Handle(GetDocumentsCount request, CancellationToken cancellationToken)
    {
        using var database = new LiteDatabase(this.configuration.ConnectionString);
        var collection = database.GetCollection<Redirection>("customers");

        return Task.FromResult(collection.Count());
    }
}