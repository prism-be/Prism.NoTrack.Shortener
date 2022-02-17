// -----------------------------------------------------------------------
//  <copyright file="ValidationBehaviorTests.cs" company="Prism">
//  Copyright (c) Prism. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Prism.NoTrack.Shortener.Tests;

using System.Collections.Generic;
using System.Threading.Tasks;

using FluentValidation;

using MediatR;

using Moq;

using Prism.NoTrack.Shortener.Behaviors;
using Prism.NoTrack.Shortener.Queries;

using Xunit;

public class ValidationBehaviorTests
{
    [Fact]
    public async Task Handle_Invalid()
    {
        // Arrange
        var validators = new List<IValidator<GetLongUrl>>
        {
            new GetLongUrlValidator()
        };

        var validationBehavior = new ValidationBehavior<GetLongUrl, LongUrl?>(validators);

        // Act and Assert
        var getLongUrl = new GetLongUrl(string.Empty);
        var exception = await Assert.ThrowsAsync<InvalidModelException>(async () => await validationBehavior.Handle(getLongUrl, default, Mock.Of<RequestHandlerDelegate<LongUrl?>>()));

        // Assert
        Assert.NotNull(exception);
        Assert.NotEmpty(exception.Validations);
    }

    [Fact]
    public async Task Handle_NoValidators()
    {
        // Arrange
        var validationBehavior = new ValidationBehavior<GetLongUrl, LongUrl?>(new List<IValidator<GetLongUrl>>());

        // Act and Assert
        var getLongUrl = new GetLongUrl(string.Empty);
        var result = await validationBehavior.Handle(getLongUrl, default, Mock.Of<RequestHandlerDelegate<LongUrl?>>());
        Assert.Null(result);
    }

    [Fact]
    public async Task Handle_Ok()
    {
        // Arrange
        var validators = new List<IValidator<GetLongUrl>>
        {
            new GetLongUrlValidator()
        };

        var validationBehavior = new ValidationBehavior<GetLongUrl, LongUrl?>(validators);

        // Act
        var getLongUrl = new GetLongUrl("42");
        var result = await validationBehavior.Handle(getLongUrl, default, Mock.Of<RequestHandlerDelegate<LongUrl?>>());
        Assert.Null(result);
    }
}