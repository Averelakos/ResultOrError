using FluentAssertions;
using ResultOrError.Models;
using ResultOrError.Partials;

namespace Tests;

public class ToResultOrErrorTests
{
    [Fact]
    public void ValueToErrorOr_WhenAccessingValue_ShouldReturnValue()
    {
        // Arrange
        int value = 5;

        // Act
        ResultOrError<int> result = value.ToResultOrError();

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().Be(value);
    }

    [Fact]
    public void ErrorToErrorOr_WhenAccessingFirstError_ShouldReturnSameError()
    {
        // Arrange
        Error error = Error.Unauthorized();

        // Act
        ResultOrError<int> result = error.ToResultOrError<int>();

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(error);
    }

    [Fact]
    public void ListOfErrorsToErrorOr_WhenAccessingErrors_ShouldReturnSameErrors()
    {
        // Arrange
        List<Error> errors = [Error.Unauthorized(), Error.Validation()];

        // Act
        ResultOrError<int> result = errors.ToResultOrError<int>();

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().BeEquivalentTo(errors);
    }

    [Fact]
    public void ArrayOfErrorsToErrorOr_WhenAccessingErrors_ShouldReturnSimilarErrors()
    {
        Error[] errors = [Error.Unauthorized(), Error.Validation()];

        ResultOrError<int> result = errors.ToResultOrError<int>();

        result.IsError.Should().BeTrue();
        result.Errors.Should().Equal(errors);
    }
}
