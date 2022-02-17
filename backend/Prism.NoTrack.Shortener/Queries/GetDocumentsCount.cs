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
    private readonly ILiteDatabase liteDatabase;

    public GetDocumentsCountHandler(ILiteDatabase liteDatabase)
    {
        this.liteDatabase = liteDatabase;
    }

    public Task<int> Handle(GetDocumentsCount request, CancellationToken cancellationToken)
    {
        var collection = this.liteDatabase.GetCollection<Redirection>("customers");

        return Task.FromResult(collection.Count());
    }
}