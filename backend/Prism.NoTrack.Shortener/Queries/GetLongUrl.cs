﻿// -----------------------------------------------------------------------
//  <copyright file="GetLongUrl.cs" company="Prism">
//  Copyright (c) Prism. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Prism.NoTrack.Shortener.Queries;

using FluentValidation;

using LiteDB;

using MediatR;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Prism.NoTrack.Shortener.Model;
using Prism.NoTrack.Shortener.Options;

public record LongUrl(string Url);

public record GetLongUrl(string Id) : IRequest<LongUrl?>;

public class GetLongUrlValidator : AbstractValidator<GetLongUrl>
{
    public GetLongUrlValidator()
    {
        this.RuleFor(x => x.Id).NotEmpty().MaximumLength(100);
    }
}

public class GetLongUrlHandler : IRequestHandler<GetLongUrl, LongUrl?>
{
    private readonly ShortenerConfiguration configuration;
    private readonly ILogger<GetLongUrlHandler> logger;

    public GetLongUrlHandler(ILogger<GetLongUrlHandler> logger, IOptions<ShortenerConfiguration> configuration)
    {
        this.logger = logger;
        this.configuration = configuration.Value;
    }

    public Task<LongUrl?> Handle(GetLongUrl request, CancellationToken cancellationToken)
    {
        this.logger.LogDebug("Requesting : {request}", request);

        using var database = new LiteDatabase(this.configuration.ConnectionString);
        var collection = database.GetCollection<Redirection>("customers");

        var longUrl = collection.FindOne(x => x.Id == request.Id);

        return Task.FromResult(longUrl == null ? null : new LongUrl(longUrl.LongUrl));
    }
}