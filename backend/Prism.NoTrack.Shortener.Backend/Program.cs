using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Prism.NoTrack.Shortener;
using Prism.NoTrack.Shortener.Behaviors;
using Prism.NoTrack.Shortener.Commands;
using Prism.NoTrack.Shortener.Options;

var builder = WebApplication.CreateBuilder(args);

var applicationAssembly = typeof(EntryPoint).Assembly;

builder.Services.AddMediatR(applicationAssembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(applicationAssembly);

builder.Services.Configure<ShortenerConfiguration>( options => builder.Configuration.GetSection("ShortenerConfiguration").Bind(options));

builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseHealthChecks("/health");

app.MapPost("api/shorten", async ([FromBody] ShortenUrl shortenUrl, IMediator mediator) => await mediator.Send(shortenUrl));

app.Run();