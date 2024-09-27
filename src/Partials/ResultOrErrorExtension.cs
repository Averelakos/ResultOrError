using ResultOrError.Models;

namespace ResultOrError.Partials
{
    public static partial class ResultOrErrorExtension
    {
        /// <summary>
        /// Creates an ResultOrError instance with the given value/>.
        /// </summary>
        public static ResultOrError<TValue> ToResultOrError<TValue>(this TValue value)
        {
            return value;
        }

        /// <summary>
        /// Creates an ResultOrError instance with the given error/>.
        /// </summary>
        public static ResultOrError<TValue> ToResultOrError<TValue>(this Error error)
        {
            return error;
        }

        /// <summary>
        /// Creates an ResultOrError instance with the given errors/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when errors/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when errors /> is an empty list.</exception>
        public static ResultOrError<TValue> ToResultOrError<TValue>(this List<Error> errors)
        {
            return errors;
        }

        /// <summary>
        /// Creates an ResultOrError instance with the given errors/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when errors/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when errors /> is an empty array.</exception>
        public static ResultOrError<TValue> ToResultOrError<TValue>(this Error[] errors)
        {
            return errors;
        }
    }
}
