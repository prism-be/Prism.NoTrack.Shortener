// -----------------------------------------------------------------------
//  <copyright file="ShortenedUrlTest.cs" company="Prism">
//  Copyright (c) Prism. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Prism.NoTrack.Shortener.Tests;

using System;
using System.Threading.Tasks;

using LiteDB;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Moq;

using Prism.NoTrack.Shortener.Commands;
using Prism.NoTrack.Shortener.Model;
using Prism.NoTrack.Shortener.Options;

using Xunit;

public class ShortenedUrlTests
{
    [Fact]
    public async Task Handle_ShouldReturnId()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<ShortenUrlHandler>>();
        var liteCollectionMock = new Mock<ILiteCollection<Redirection>>();
        var databaseMock = new Mock<ILiteDatabase>();
        databaseMock.Setup(x => x.GetCollection<Redirection>("customers", BsonAutoId.ObjectId)).Returns(liteCollectionMock.Object);
        var configuration = Options.Create(new ShortenerConfiguration { ShortDomain = "https://shortDomain.ltd/" });
        var shortenUrlHandler = new ShortenUrlHandler(loggerMock.Object, configuration, databaseMock.Object);

        // Act
        var shortenUrl = new ShortenUrl("https://github.com/prism-be/Prism.NoTrack.Shortener/");
        var shortened = await shortenUrlHandler.Handle(shortenUrl, default);

        // Assert
        Assert.NotNull(shortened);
        Assert.StartsWith(configuration.Value.ShortDomain + "r", shortened!.Url);
        liteCollectionMock.Verify(x => x.Insert(It.IsAny<Redirection>()));
    }

    [Fact]
    public async Task Handle_MustHaveShortDomain()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<ShortenUrlHandler>>();
        var liteCollectionMock = new Mock<ILiteCollection<Redirection>>();
        var databaseMock = new Mock<ILiteDatabase>();
        databaseMock.Setup(x => x.GetCollection<Redirection>("customers", BsonAutoId.ObjectId)).Returns(liteCollectionMock.Object);
        var configuration = Options.Create(new ShortenerConfiguration { ShortDomain = null });
        var shortenUrlHandler = new ShortenUrlHandler(loggerMock.Object, configuration, databaseMock.Object);

        // Act
        var shortenUrl = new ShortenUrl("https://github.com/prism-be/Prism.NoTrack.Shortener/");
        var exception = await Assert.ThrowsAsync<ApplicationException>(async () => await shortenUrlHandler.Handle(shortenUrl, default));

        // Assert
        Assert.NotNull(exception);
        Assert.Equal("The ShortDomain is not configured", exception.Message);
    }

    [Fact]
    public void Validate_Ko_Empty()
    {
        // Arrange
        var validator = new ShortenUrlValidator();

        // Act
        var shortenUrl = new ShortenUrl(string.Empty);
        var result = validator.Validate(shortenUrl);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validate_Ko_Length()
    {
        // Arrange
        var validator = new ShortenUrlValidator();

        // Act
        var shortenUrl = new ShortenUrl(string.Empty.PadRight(80001, '-'));
        var result = validator.Validate(shortenUrl);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validate_Ok()
    {
        // Arrange
        var validator = new ShortenUrlValidator();

        // Act
        var shortenUrl = new ShortenUrl("https://github.com/prism-be/Prism.NoTrack.Shortener/");
        var result = validator.Validate(shortenUrl);

        // Assert
        Assert.True(result.IsValid);
    }
}