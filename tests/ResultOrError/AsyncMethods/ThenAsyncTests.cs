using FluentAssertions;
using ResultOrError.Models;
using ResultOrError.Partials;

namespace Tests;

public class ThenAsyncTests
{
    [Fact]
    public async Task CallingThenAsync_WhenIsSuccess_ShouldInvokeNextThen()
    {
        // Arrange
        ResultOrError<string> errorOrString = "5";

        // Act
        ResultOrError<string> result = await errorOrString
            .ThenAsync(Convert.ToIntAsync)
            .ThenAsync(num => Task.FromResult(num * 2))
            .ThenDoAsync(num => Task.Run(() => { _ = 5; }))
            .ThenAsync(Convert.ToStringAsync);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().BeEquivalentTo("10");
    }

    [Fact]
    public async Task CallingThenAsync_WhenIsError_ShouldReturnErrors()
    {
        // Arrange
        ResultOrError<string> errorOrString = Error.NotFound();

        // Act
        ResultOrError<string> result = await errorOrString
            .ThenAsync(Convert.ToIntAsync)
            .ThenAsync(num => Task.FromResult(num * 2))
            .ThenDoAsync(num => Task.Run(() => { _ = 5; }))
            .ThenAsync(Convert.ToStringAsync);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().BeEquivalentTo(errorOrString.FirstError);
    }
}
