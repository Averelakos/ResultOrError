﻿using ResultOrError.Contracts;
using ResultOrError.Models;

namespace ResultOrError.Partials
{

    public readonly partial record struct ResultOrError<TValue> : IResultOrError<TValue>
    {
        /// <summary>
        /// If the state is value, the provided function <paramref name="onValue"/> is invoked.
        /// If <paramref name="onValue"/> returns true, the given <paramref name="error"/> will be returned, and the state will be error.
        /// </summary>
        /// <param name="onValue">The function to execute if the state is value.</param>
        /// <param name="error">The <see cref="Error"/> to return if the given <paramref name="onValue"/> function returned true.</param>
        /// <returns>The given <paramref name="error"/> if <paramref name="onValue"/> returns true; otherwise, the original <see cref="ErrorOr"/> instance.</returns>
        public ResultOrError<TValue> FailIf(Func<TValue, bool> onValue, Error error)
        {
            if (IsError)
            {
                return this;
            }

            return onValue(Value) ? error : this;
        }

        /// <summary>
        /// If the state is value, the provided function <paramref name="onValue"/> is invoked.
        /// If <paramref name="onValue"/> returns true, the given <paramref name="errorBuilder"/> function will be executed, and the state will be error.
        /// </summary>
        /// <param name="onValue">The function to execute if the state is value.</param>
        /// <param name="errorBuilder">The error builder function to execute and return if the given <paramref name="onValue"/> function returned true.</param>
        /// <returns>The given <paramref name="errorBuilder"/> functions return value if <paramref name="onValue"/> returns true; otherwise, the original <see cref="ErrorOr"/> instance.</returns>
        public ResultOrError<TValue> FailIf(Func<TValue, bool> onValue, Func<TValue, Error> errorBuilder)
        {
            if (IsError)
            {
                return this;
            }

            return onValue(Value) ? errorBuilder(Value) : this;
        }

        /// <summary>
        /// If the state is value, the provided function <paramref name="onValue"/> is invoked asynchronously.
        /// If <paramref name="onValue"/> returns true, the given <paramref name="error"/> will be returned, and the state will be error.
        /// </summary>
        /// <param name="onValue">The function to execute if the statement is value.</param>
        /// <param name="error">The <see cref="Error"/> to return if the given <paramref name="onValue"/> function returned true.</param>
        /// <returns>The given <paramref name="error"/> if <paramref name="onValue"/> returns true; otherwise, the original <see cref="ErrorOr"/> instance.</returns>
        public async Task<ResultOrError<TValue>> FailIfAsync(Func<TValue, Task<bool>> onValue, Error error)
        {
            if (IsError)
            {
                return this;
            }

            return await onValue(Value).ConfigureAwait(false) ? error : this;
        }

        /// <summary>
        /// If the state is value, the provided function <paramref name="onValue"/> is invoked.
        /// If <paramref name="onValue"/> returns true, the given <paramref name="errorBuilder"/> function will be executed, and the state will be error.
        /// </summary>
        /// <param name="onValue">The function to execute if the state is value.</param>
        /// <param name="errorBuilder">The error builder function to execute and return if the given <paramref name="onValue"/> function returned true.</param>
        /// <returns>The given <paramref name="errorBuilder"/> functions return value if <paramref name="onValue"/> returns true; otherwise, the original <see cref="ErrorOr"/> instance.</returns>
        public async Task<ResultOrError<TValue>> FailIfAsync(Func<TValue, Task<bool>> onValue, Func<TValue, Task<Error>> errorBuilder)
        {
            if (IsError)
            {
                return this;
            }

            return await onValue(Value).ConfigureAwait(false) ? await errorBuilder(Value).ConfigureAwait(false) : this;
        }
    }
}
