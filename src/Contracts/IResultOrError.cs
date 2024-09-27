using ResultOrError.Models;

namespace ResultOrError.Contracts
{
    public interface IResultOrError
    {
        /// <summary>
        /// Get the list of errors.
        /// </summary>
        List<Error>? Errors { get; }
        
        /// <summary>
        /// Gets a boolean values indicating whether the state is error. 
        /// </summary>
        bool IsError { get; }
    }

    public interface IResultOrError<out TValue>: IResultOrError
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        TValue Value { get; }
    }
}
