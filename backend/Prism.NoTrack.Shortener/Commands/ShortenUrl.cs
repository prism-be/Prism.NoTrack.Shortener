// -----------------------------------------------------------------------
//  <copyright file="ShortenUrl.cs" company="Prism">
//  Copyright (c) Prism. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Prism.NoTrack.Shortener.Commands;

using FluentValidation;

using LiteDB;

using MediatR;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Nanoid;

using Prism.NoTrack.Shortener.Model;
using Prism.NoTrack.Shortener.Options;

public record ShortenedUrl(string Url);

public record ShortenUrl(string Url) : IRequest<ShortenedUrl?>;

public class ShortenUrlValidator : AbstractValidator<ShortenUrl>
{
    public ShortenUrlValidator()
    {
        this.RuleFor(x => x.Url).NotEmpty().MaximumLength(80000);
    }
}

public class ShortenUrlHandler : IRequestHandler<ShortenUrl, ShortenedUrl?>
{
    private readonly ShortenerConfiguration configuration;
    private readonly ILogger<ShortenUrlHandler> logger;
    private readonly ILiteDatabase liteDatabase;

    public ShortenUrlHandler(ILogger<ShortenUrlHandler> logger, IOptions<ShortenerConfiguration> configuration, ILiteDatabase liteDatabase)
    {
        this.logger = logger;
        this.liteDatabase = liteDatabase;
        this.configuration = configuration.Value;
    }

    public async Task<ShortenedUrl?> Handle(ShortenUrl request, CancellationToken cancellationToken)
    {
        this.logger.LogDebug("Shortening : {request}", request);

        var id = await Nanoid.GenerateAsync(size: 16);

        var collection = this.liteDatabase.GetCollection<Redirection>("customers");

        var redirection = new Redirection(id, request.Url);

        collection.Insert(redirection);
        collection.EnsureIndex(x => x.Id);

        if (string.IsNullOrWhiteSpace(this.configuration.ShortDomain))
        {
            throw new ApplicationException("The ShortDomain is not configured");
        }

        this.logger.LogDebug("The url {url} has been stored with id : {id}", redirection.LongUrl, redirection.Id);
        return new ShortenedUrl($"{this.configuration.ShortDomain.TrimEnd('/')}/r/{id}");
    }
}