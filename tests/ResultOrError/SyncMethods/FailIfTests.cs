using FluentAssertions;
using ResultOrError.Enums;
using ResultOrError.Models;
using ResultOrError.Partials;

namespace Tests;

public class FailIfTests
{
    [Fact]
    public void CallingFailIf_WhenFailsIf_ShouldReturnError()
    {
        // Arrange
        ResultOrError<int> errorOrInt = 5;

        // Act
        ResultOrError<int> result = errorOrInt
            .FailIf(num => num > 3, Error.Failure());

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Failure);
    }

    [Fact]
    public async Task CallingFailIfExtensionMethod_WhenFailsIf_ShouldReturnError()
    {
        // Arrange
        ResultOrError<int> errorOrInt = 5;

        // Act
        ResultOrError<int> result = await errorOrInt
            .ThenAsync(num => Task.FromResult(num))
            .FailIf(num => num > 3, Error.Failure());

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Failure);
    }

    [Fact]
    public void CallingFailIf_WhenDoesNotFailIf_ShouldReturnValue()
    {
        // Arrange
        ResultOrError<int> errorOrInt = 5;

        // Act
        ResultOrError<int> result = errorOrInt
            .FailIf(num => num > 10, Error.Failure());

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().Be(5);
    }

    [Fact]
    public void CallingFailIf_WhenIsError_ShouldNotInvokeFailIfFunc()
    {
        // Arrange
        ResultOrError<string> errorOrString = Error.NotFound();

        // Act
        ResultOrError<string> result = errorOrString
            .FailIf(str => str == string.Empty, Error.Failure());

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.NotFound);
    }

    [Fact]
    public void CallingFailIfWithErrorBuilder_WhenFailsIf_ShouldReturnError()
    {
        // Arrange
        ResultOrError<int> errorOrInt = 5;

        // Act
        ResultOrError<int> result = errorOrInt
            .FailIf(num => num > 3, (num) => Error.Failure(description: $"{num} is greater than 3"));

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Failure);
        result.FirstError.Description.Should().Be("5 is greater than 3");
    }

    [Fact]
    public async Task CallingFailIfExtensionMethodWithErrorBuilder_WhenFailsIf_ShouldReturnError()
    {
        // Arrange
        ResultOrError<int> errorOrInt = 5;

        // Act
        ResultOrError<int> result = await errorOrInt
            .ThenAsync(num => Task.FromResult(num))
            .FailIf(num => num > 3, (num) => Error.Failure(description: $"{num} is greater than 3"));

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Failure);
        result.FirstError.Description.Should().Be("5 is greater than 3");
    }

    [Fact]
    public void CallingFailIfWithErrorBuilder_WhenDoesNotFailIf_ShouldReturnValue()
    {
        // Arrange
        ResultOrError<int> errorOrInt = 5;

        // Act
        ResultOrError<int> result = errorOrInt
            .FailIf(num => num > 10, (num) => Error.Failure(description: $"{num} is greater than 10"));

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().Be(5);
    }

    [Fact]
    public void CallingFailIfWithErrorBuilder_WhenIsError_ShouldNotInvokeFailIfFunc()
    {
        // Arrange
        ResultOrError<int> errorOrInt = Error.NotFound();

        // Act
        ResultOrError<int> result = errorOrInt
            .FailIf(num => num > 3, (num) => Error.Failure(description: $"{num} is greater than 3"));

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.NotFound);
    }
}
