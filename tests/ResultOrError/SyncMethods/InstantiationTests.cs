namespace Tests;

using FluentAssertions;
using ResultOrError.Factories;
using ResultOrError.Models;
using ResultOrError.Partials;

public class ErrorOrInstantiationTests
{
    private record Person(string Name);

    [Fact]
    public void CreateFromFactory_WhenAccessingValue_ShouldReturnValue()
    {
        // Arrange
        IEnumerable<string> value = ["value"];

        // Act
        ResultOrError<IEnumerable<string>> errorOrPerson = ResultOrErrorFactory.From(value);

        // Assert
        errorOrPerson.IsError.Should().BeFalse();
        errorOrPerson.Value.Should().BeSameAs(value);
    }

    [Fact]
    public void CreateFromFactory_WhenAccessingErrors_ShouldThrow()
    {
        // Arrange
        IEnumerable<string> value = ["value"];
        ResultOrError<IEnumerable<string>> errorOrPerson = ResultOrErrorFactory.From(value);

        // Act
        Func<List<Error>> errors = () => errorOrPerson.Errors;

        // Assert
        errors.Should().ThrowExactly<InvalidOperationException>();
    }

    [Fact]
    public void CreateFromFactory_WhenAccessingErrorsOrEmptyList_ShouldReturnEmptyList()
    {
        // Arrange
        IEnumerable<string> value = ["value"];
        ResultOrError<IEnumerable<string>> errorOrPerson = ResultOrErrorFactory.From(value);

        // Act
        List<Error> errors = errorOrPerson.ErrorsOrEmptyList;

        // Assert
        errors.Should().BeEmpty();
    }

    [Fact]
    public void CreateFromFactory_WhenAccessingFirstError_ShouldThrow()
    {
        // Arrange
        IEnumerable<string> value = ["value"];
        ResultOrError<IEnumerable<string>> errorOrPerson = ResultOrErrorFactory.From(value);

        // Act
        Func<Error> action = () => errorOrPerson.FirstError;

        // Assert
        action.Should().ThrowExactly<InvalidOperationException>();
    }

    [Fact]
    public void CreateFromValue_WhenAccessingValue_ShouldReturnValue()
    {
        // Arrange
        IEnumerable<string> value = ["value"];

        // Act
        ResultOrError<IEnumerable<string>> errorOrPerson = ResultOrErrorFactory.From(value);

        // Assert
        errorOrPerson.IsError.Should().BeFalse();
        errorOrPerson.Value.Should().BeSameAs(value);
    }

    [Fact]
    public void CreateFromValue_WhenAccessingErrors_ShouldThrow()
    {
        // Arrange
        IEnumerable<string> value = ["value"];
        ResultOrError<IEnumerable<string>> errorOrPerson = ResultOrErrorFactory.From(value);

        // Act
        Func<List<Error>> action = () => errorOrPerson.Errors;

        // Assert
        action.Should().ThrowExactly<InvalidOperationException>();
    }

    [Fact]
    public void CreateFromValue_WhenAccessingErrorsOrEmptyList_ShouldReturnEmptyList()
    {
        // Arrange
        IEnumerable<string> value = ["value"];
        ResultOrError<IEnumerable<string>> errorOrPerson = ResultOrErrorFactory.From(value);

        // Act
        List<Error> errors = errorOrPerson.ErrorsOrEmptyList;

        // Assert
        errors.Should().BeEmpty();
    }

    [Fact]
    public void CreateFromValue_WhenAccessingFirstError_ShouldThrow()
    {
        // Arrange
        IEnumerable<string> value = ["value"];
        ResultOrError<IEnumerable<string>> errorOrPerson = ResultOrErrorFactory.From(value);

        // Act
        Func<Error> action = () => errorOrPerson.FirstError;

        // Assert
        action.Should().ThrowExactly<InvalidOperationException>();
    }

    [Fact]
    public void CreateFromErrorList_WhenAccessingErrors_ShouldReturnErrorList()
    {
        // Arrange
        List<Error> errors = new() { Error.Validation("User.Name", "Name is too short") };
        ResultOrError<Person> errorOrPerson = ResultOrError<Person>.From(errors);

        // Act & Assert
        errorOrPerson.IsError.Should().BeTrue();
        errorOrPerson.Errors.Should().ContainSingle().Which.Should().Be(errors.Single());
    }

    [Fact]
    public void CreateFromErrorList_WhenAccessingErrorsOrEmptyList_ShouldReturnErrorList()
    {
        // Arrange
        List<Error> errors = new() { Error.Validation("User.Name", "Name is too short") };
        ResultOrError<Person> errorOrPerson = ResultOrError<Person>.From(errors);

        // Act & Assert
        errorOrPerson.IsError.Should().BeTrue();
        errorOrPerson.ErrorsOrEmptyList.Should().ContainSingle().Which.Should().Be(errors.Single());
    }

    [Fact]
    public void CreateFromErrorList_WhenAccessingValue_ShouldThrowInvalidOperationException()
    {
        // Arrange
        List<Error> errors = new() { Error.Validation("User.Name", "Name is too short") };
        ResultOrError<Person> errorOrPerson = ResultOrError<Person>.From(errors);

        // Act
        var act = () => errorOrPerson.Value;

        // Assert
        act.Should().Throw<InvalidOperationException>()
           .And.Message.Should().Be("The Value property cannot be accessed when errors have been recorded. Check IsError before accessing Value.");
    }

    [Fact]
    public void ImplicitCastResult_WhenAccessingResult_ShouldReturnValue()
    {
        // Arrange
        Person result = new Person("Amici");

        // Act
        ResultOrError<Person> errorOr = result;

        // Assert
        errorOr.IsError.Should().BeFalse();
        errorOr.Value.Should().Be(result);
    }

    [Fact]
    public void ImplicitCastResult_WhenAccessingErrors_ShouldThrow()
    {
        ResultOrError<Person> errorOrPerson = new Person("Amichai");

        // Act
        Func<List<Error>> action = () => errorOrPerson.Errors;

        // Assert
        action.Should().ThrowExactly<InvalidOperationException>();
    }

    [Fact]
    public void ImplicitCastResult_WhenAccessingFirstError_ShouldThrow()
    {
        ResultOrError<Person> errorOrPerson = new Person("Amichai");

        // Act
        Func<Error> action = () => errorOrPerson.FirstError;

        // Assert
        action.Should().ThrowExactly<InvalidOperationException>();
    }

    [Fact]
    public void ImplicitCastPrimitiveResult_WhenAccessingResult_ShouldReturnValue()
    {
        // Arrange
        const int result = 4;

        // Act
        ResultOrError<int> errorOrInt = result;

        // Assert
        errorOrInt.IsError.Should().BeFalse();
        errorOrInt.Value.Should().Be(result);
    }

    [Fact]
    public void ImplicitCastErrorOrType_WhenAccessingResult_ShouldReturnValue()
    {
        // Act
        ResultOrError<Success> errorOrSuccess = Result.Success;
        ResultOrError<Created> errorOrCreated = Result.Created;
        ResultOrError<Deleted> errorOrDeleted = Result.Deleted;
        ResultOrError<Updated> errorOrUpdated = Result.Updated;

        // Assert
        errorOrSuccess.IsError.Should().BeFalse();
        errorOrSuccess.Value.Should().Be(Result.Success);

        errorOrCreated.IsError.Should().BeFalse();
        errorOrCreated.Value.Should().Be(Result.Created);

        errorOrDeleted.IsError.Should().BeFalse();
        errorOrDeleted.Value.Should().Be(Result.Deleted);

        errorOrUpdated.IsError.Should().BeFalse();
        errorOrUpdated.Value.Should().Be(Result.Updated);
    }

    [Fact]
    public void ImplicitCastSingleError_WhenAccessingErrors_ShouldReturnErrorList()
    {
        // Arrange
        Error error = Error.Validation("User.Name", "Name is too short");

        // Act
        ResultOrError<Person> errorOrPerson = error;

        // Assert
        errorOrPerson.IsError.Should().BeTrue();
        errorOrPerson.Errors.Should().ContainSingle().Which.Should().Be(error);
    }

    [Fact]
    public void ImplicitCastError_WhenAccessingValue_ShouldThrowInvalidOperationException()
    {
        // Arrange
        ResultOrError<Person> errorOrPerson = Error.Validation("User.Name", "Name is too short");

        // Act
        var act = () => errorOrPerson.Value;

        // Assert
        act.Should().Throw<InvalidOperationException>()
           .And.Message.Should().Be("The Value property cannot be accessed when errors have been recorded. Check IsError before accessing Value.");
    }

    [Fact]
    public void ImplicitCastSingleError_WhenAccessingFirstError_ShouldReturnError()
    {
        // Arrange
        Error error = Error.Validation("User.Name", "Name is too short");

        // Act
        ResultOrError<Person> errorOrPerson = error;

        // Assert
        errorOrPerson.IsError.Should().BeTrue();
        errorOrPerson.FirstError.Should().Be(error);
    }

    [Fact]
    public void ImplicitCastErrorList_WhenAccessingErrors_ShouldReturnErrorList()
    {
        // Arrange
        List<Error> errors = new()
        {
            Error.Validation("User.Name", "Name is too short"),
            Error.Validation("User.Age", "User is too young"),
        };

        // Act
        ResultOrError<Person> errorOrPerson = errors;

        // Assert
        errorOrPerson.IsError.Should().BeTrue();
        errorOrPerson.Errors.Should().HaveCount(errors.Count).And.BeEquivalentTo(errors);
    }

    [Fact]
    public void ImplicitCastErrorArray_WhenAccessingErrors_ShouldReturnErrorArray()
    {
        // Arrange
        Error[] errors =
        [
            Error.Validation("User.Name", "Name is too short"),
            Error.Validation("User.Age", "User is too young"),
        ];

        // Act
        ResultOrError<Person> errorOrPerson = errors;

        // Assert
        errorOrPerson.IsError.Should().BeTrue();
        errorOrPerson.Errors.Should().HaveCount(errors.Length).And.BeEquivalentTo(errors);
    }

    [Fact]
    public void ImplicitCastErrorList_WhenAccessingFirstError_ShouldReturnFirstError()
    {
        // Arrange
        List<Error> errors = new()
        {
            Error.Validation("User.Name", "Name is too short"),
            Error.Validation("User.Age", "User is too young"),
        };

        // Act
        ResultOrError<Person> errorOrPerson = errors;

        // Assert
        errorOrPerson.IsError.Should().BeTrue();
        errorOrPerson.FirstError.Should().Be(errors[0]);
    }

    [Fact]
    public void ImplicitCastErrorArray_WhenAccessingFirstError_ShouldReturnFirstError()
    {
        // Arrange
        Error[] errors =
        [
            Error.Validation("User.Name", "Name is too short"),
            Error.Validation("User.Age", "User is too young"),
        ];

        // Act
        ResultOrError<Person> errorOrPerson = errors;

        // Assert
        errorOrPerson.IsError.Should().BeTrue();
        errorOrPerson.FirstError.Should().Be(errors[0]);
    }

    [Fact]
    public void CreateErrorOr_WhenUsingEmptyConstructor_ShouldThrow()
    {
        // Act
        Func<ResultOrError<int>> action = () => new ResultOrError<int>();

        // Assert
        action.Should().ThrowExactly<InvalidOperationException>();
    }

    [Fact]
    public void CreateErrorOr_WhenEmptyErrorsList_ShouldThrow()
    {
        // Act
        Func<ResultOrError<int>> errorOrInt = () => new List<Error>();

        // Assert
        var exception = errorOrInt.Should().ThrowExactly<ArgumentException>().Which;
        exception.Message.Should().Be("Cannot create an ErrorOr<TValue> from an empty collection of errors. Provide at least one error. (Parameter 'errors')");
        exception.ParamName.Should().Be("errors");
    }

    [Fact]
    public void CreateErrorOr_WhenEmptyErrorsArray_ShouldThrow()
    {
        // Act
        Func<ResultOrError<int>> errorOrInt = () => Array.Empty<Error>();

        // Assert
        var exception = errorOrInt.Should().ThrowExactly<ArgumentException>().Which;
        exception.Message.Should().Be("Cannot create an ErrorOr<TValue> from an empty collection of errors. Provide at least one error. (Parameter 'errors')");
        exception.ParamName.Should().Be("errors");
    }

    [Fact]
    public void CreateErrorOr_WhenNullIsPassedAsErrorsList_ShouldThrowArgumentNullException()
    {
        Func<ResultOrError<int>> act = () => default(List<Error>)!;

        act.Should().ThrowExactly<ArgumentNullException>()
           .And.ParamName.Should().Be("errors");
    }

    [Fact]
    public void CreateErrorOr_WhenNullIsPassedAsErrorsArray_ShouldThrowArgumentNullException()
    {
        Func<ResultOrError<int>> act = () => default(Error[])!;

        act.Should().ThrowExactly<ArgumentNullException>()
           .And.ParamName.Should().Be("errors");
    }

    [Fact]
    public void CreateErrorOr_WhenValueIsNull_ShouldThrowArgumentNullException()
    {
        Func<ResultOrError<int?>> act = () => default(int?);

        act.Should().ThrowExactly<ArgumentNullException>()
           .And.ParamName.Should().Be("value");
    }
}
