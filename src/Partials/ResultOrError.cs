using ResultOrError.Contracts;
using ResultOrError.Models;
using System.Diagnostics.CodeAnalysis;

namespace ResultOrError.Partials
{
    public readonly partial record struct ResultOrError<TValue> : IResultOrError<TValue>
    {
        #region Private Properties
        private readonly TValue? _value = default;
        private readonly List<Error>? _errors = null;
        #endregion Private Properties

        #region Constructors
        public ResultOrError() => throw new InvalidOperationException("Default construction of ErrorOr<TValue> is invalid. Please use provided factory methods to instantiate.");
        private ResultOrError(Error error) => _errors = [error];
        private ResultOrError(List<Error> errors)
        {
            if (errors is null)
            {
                throw new ArgumentNullException(nameof(errors));
            }

            if (errors is null || errors.Count == 0)
            {
                throw new ArgumentException("Cannot create an ErrorOr<TValue> from an empty collection of errors. Provide at least one error.", nameof(errors));
            }

            _errors = errors;
        }
        private ResultOrError(TValue value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _value = value;
        }

        #endregion Constructors

        #region Public Properties
        public TValue Value 
        {
            get
            {
                if (IsError)
                {
                    throw new InvalidOperationException("The Value property cannot be accessed when errors have been recorded. Check IsError before accessing Value.");
                }
                return _value;
            }
        }

        public List<Error>? Errors => IsError ? _errors : throw new InvalidOperationException("The Errors property cannot be accessed when no errors have been recorded. Check IsError before accessing Errors.");
        
        [MemberNotNullWhen(true, nameof(_errors))]
        [MemberNotNullWhen(true, nameof(Errors))]
        [MemberNotNullWhen(false, nameof(Value))]
        [MemberNotNullWhen(false, nameof(_value))]
        public bool IsError => _errors is not null;
        public List<Error> ErrorsOrEmptyList => IsError ? _errors : EmptyErrors.Instance;
        public Error FirstError
        {
            get
            {
                if (!IsError)
                {
                    throw new InvalidOperationException("The FirstError property cannot be accessed when no errors have been recorded. Check IsError before accessing FirstError.");
                }

                return _errors[0];
            }
        }
        #endregion Public Properties

        #region Methods
        /// <summary>
        /// Creates an ResultOrError<TValue> from a list of errors.
        /// </summary>
        /// <param name="errors"> list of errors</param>
        /// <returns>ResultOrError<TValue></returns>
        public static ResultOrError<TValue> From(List<Error> errors)
        {
            return errors;
        }
        #endregion Methods
    }
}
