// -----------------------------------------------------------------------
//  <copyright file="GetDocumentsCountTests.cs" company="Prism">
//  Copyright (c) Prism. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Prism.NoTrack.Shortener.Tests;

using System.Threading.Tasks;

using LiteDB;

using Moq;

using Prism.NoTrack.Shortener.Model;
using Prism.NoTrack.Shortener.Queries;

using Xunit;

public class GetDocumentsCountTests
{
    [Fact]
    public async Task Handle_Ok()
    {
        // Arrange
        var liteCollectionMock = new Mock<ILiteCollection<Redirection>>();
        liteCollectionMock.Setup(x => x.Count()).Returns(42);
        var databaseMock = new Mock<ILiteDatabase>();
        databaseMock.Setup(x => x.GetCollection<Redirection>("customers", BsonAutoId.ObjectId)).Returns(liteCollectionMock.Object);

        // Act
        var handler = new GetDocumentsCountHandler(databaseMock.Object);
        var result = await handler.Handle(new GetDocumentsCount(), default);

        // Assert
        Assert.Equal(42, result);
    }
}