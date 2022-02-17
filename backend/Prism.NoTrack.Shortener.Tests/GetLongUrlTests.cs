// -----------------------------------------------------------------------
//  <copyright file="GetLongUrlTests.cs" company="Prism">
//  Copyright (c) Prism. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Prism.NoTrack.Shortener.Tests;

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

using LiteDB;

using Microsoft.Extensions.Logging;

using Moq;

using Prism.NoTrack.Shortener.Model;
using Prism.NoTrack.Shortener.Queries;

using Xunit;

public class GetLongUrlTests
{
    [Fact]
    public async Task Handle_NotFound()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<GetLongUrlHandler>>();
        var liteCollectionMock = new Mock<ILiteCollection<Redirection>>();
        var databaseMock = new Mock<ILiteDatabase>();
        databaseMock.Setup(x => x.GetCollection<Redirection>("customers", BsonAutoId.ObjectId)).Returns(liteCollectionMock.Object);
        var handler = new GetLongUrlHandler(loggerMock.Object, databaseMock.Object);

        // Act
        var result = await handler.Handle(new GetLongUrl("43"), default);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Handle_Ok()
    {
        // Arrange
        var redirection = new Redirection("42", "https://github.com/prism-be/Prism.NoTrack.Shortener/");
        var loggerMock = new Mock<ILogger<GetLongUrlHandler>>();
        var liteCollectionMock = new Mock<ILiteCollection<Redirection>>();
        liteCollectionMock.Setup(x => x.FindOne(It.IsAny<Expression<Func<Redirection, bool>>>())).Returns(redirection);
        var databaseMock = new Mock<ILiteDatabase>();
        databaseMock.Setup(x => x.GetCollection<Redirection>("customers", BsonAutoId.ObjectId)).Returns(liteCollectionMock.Object);
        var handler = new GetLongUrlHandler(loggerMock.Object, databaseMock.Object);

        // Act
        var result = await handler.Handle(new GetLongUrl(redirection.Id), default);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(redirection.LongUrl, result!.Url);
    }

    [Fact]
    public void Validate_Empty()
    {
        // Arrange
        var validator = new GetLongUrlValidator();

        // Act
        var getLongUrl = new GetLongUrl(string.Empty);
        var result = validator.Validate(getLongUrl);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validate_Long()
    {
        // Arrange
        var validator = new GetLongUrlValidator();

        // Act
        var getLongUrl = new GetLongUrl(string.Empty.PadRight(101, '-'));
        var result = validator.Validate(getLongUrl);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validate_Ok()
    {
        // Arrange
        var validator = new GetLongUrlValidator();

        // Act
        var getLongUrl = new GetLongUrl("42");
        var result = validator.Validate(getLongUrl);

        // Assert
        Assert.True(result.IsValid);
    }
}