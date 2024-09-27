using ResultOrError.Contracts;
using ResultOrError.Models;

namespace ResultOrError.Partials
{
    public readonly partial record struct ResultOrError<TValue> : IResultOrError<TValue>
    {
        #region Else
        public ResultOrError<TValue> Else(Func<List<Error>, Error> onError)
        {
            if (!IsError) return Value;

            return onError(Errors);
        }

        public ResultOrError<TValue> Else(Func<List<Error>, List<Error>> onError)
        {
            if (!IsError) return Value;

            return onError(Errors);
        }

        public ResultOrError<TValue> Else(Error error)
        {
            if (!IsError) return Value;

            return error;
        }

        public ResultOrError<TValue> Else(Func<List<Error>, TValue> onError)
        {
            if (!IsError) return Value;

            return onError(Errors);
        }

        public ResultOrError<TValue> Else(TValue onError)
        {
            if (!IsError) return Value;

            return onError;
        }
        #endregion Else

        #region Else Async
        public async Task<ResultOrError<TValue>> ElseAsync(Func<List<Error>, Task<TValue>> onError)
        {
            if (!IsError) return Value;
            return await onError(Errors).ConfigureAwait(false);
        }

        public async Task<ResultOrError<TValue>> ElseAsync(Func<List<Error>, Task<Error>> onError)
        {
            if (!IsError) return Value;
            return await onError(Errors).ConfigureAwait(false);
        }

        public async Task<ResultOrError<TValue>> ElseAsync(Func<List<Error>, Task<List<Error>>> onError)
        {
            if (!IsError) return Value;
            return await onError(Errors).ConfigureAwait(false);
        }

        public async Task<ResultOrError<TValue>> ElseAsync(Task<Error> error)
        {
            if (!IsError) return Value;
            return await error.ConfigureAwait(false);
        }

        public async Task<ResultOrError<TValue>> ElseAsync(Task<TValue> onError)
        {
            if (!IsError) return Value;
            return await onError.ConfigureAwait(false);
        }
        #endregion Else Async
    }
}
