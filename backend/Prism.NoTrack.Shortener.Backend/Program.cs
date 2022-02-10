// -----------------------------------------------------------------------
//  <copyright file="Program.cs" company="Prism">
//  Copyright (c) Prism. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Prism.NoTrack.Shortener;
using Prism.NoTrack.Shortener.Behaviors;
using Prism.NoTrack.Shortener.Commands;
using Prism.NoTrack.Shortener.Options;
using Prism.NoTrack.Shortener.Queries;

var builder = WebApplication.CreateBuilder(args);

var applicationAssembly = typeof(EntryPoint).Assembly;

builder.Services.AddMediatR(applicationAssembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(applicationAssembly);

builder.Services.Configure<ShortenerConfiguration>(options => builder.Configuration.GetSection("ShortenerConfiguration").Bind(options));

builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseHealthChecks("/health");

app.MapPost("api/shorten", async ([FromBody] ShortenUrl shortenUrl, IMediator mediator) => await mediator.Send(shortenUrl));

app.MapGet("r/{id}", async ([FromRoute] string id, IMediator mediator) =>
{
    var longUrl = await mediator.Send(new GetLongUrl(id));

    if (longUrl == null)
    {
        return Results.NotFound();
    }

    return Results.Redirect(longUrl.Url);
});

app.Run();