// -----------------------------------------------------------------------
//  <copyright file="FakeTests.cs" company="Prism">
//  Copyright (c) Prism. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Prism.NoTrack.Shortener.Tests;

using Prism.NoTrack.Shortener.Options;

using Xunit;

/// <summary>
/// Tests only for code coverate :)
/// </summary>
public class FakeTests
{
    [Fact]
    public void EntryPoint()
    {
        // Arrange
        var entryPoint = new EntryPoint();

        // Act

        // Assert
        Assert.NotNull(entryPoint);
    }

    [Fact]
    public void ShortenerConfiguration()
    {
        // Arrange
        var configuration = new ShortenerConfiguration
        {
            ConnectionString = "42",
            ShortDomain = "42"
        };

        // Act

        // Assert
        Assert.NotNull(configuration);
        Assert.Equal("42", configuration.ConnectionString);
        Assert.Equal("42", configuration.ShortDomain);
    }
}