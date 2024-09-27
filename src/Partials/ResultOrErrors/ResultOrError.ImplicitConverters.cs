using ResultOrError.Contracts;
using ResultOrError.Models;

namespace ResultOrError.Partials
{
    public readonly partial record struct ResultOrError<TValue> : IResultOrError<TValue>
    {
        /// <summary>
        /// Creates an ResultOrError<TValue> from a value
        /// </summary>
        public static implicit operator ResultOrError<TValue>(TValue value)
        {
            return new ResultOrError<TValue>(value);
        }

        /// <summary>
        /// Creates an ResultOrError<TValue> from a error
        /// </summary>
        public static implicit operator ResultOrError<TValue>(Error error)
        {
            return new ResultOrError<TValue>(error);
        }

        /// <summary>
        /// Creates an ResultOrError<TValue> from a list of errors
        /// </summary>
        public static implicit operator ResultOrError<TValue>(List<Error> errors)
        {
            return new ResultOrError<TValue>(errors);
        }

        /// <summary>
        /// Creates an ResultOrError<TValue> from a list of errors
        /// </summary>
        public static implicit operator ResultOrError<TValue>(Error[] errors)
        {
            if(errors is null) throw new ArgumentNullException(nameof(errors));

            return new ResultOrError<TValue>([..errors]);
        }
    }
}
