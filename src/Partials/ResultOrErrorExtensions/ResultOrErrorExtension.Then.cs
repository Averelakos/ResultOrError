﻿using ResultOrError.Models;

namespace ResultOrError.Partials
{
    public static partial class ResultOrErrorExtension
    {
        /// <summary>
        /// If the state of <paramref name="errorOr"/> is a value, the provided function <paramref name="onValue"/> is executed and its result is returned.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <typeparam name="TNextValue">The type of the result from invoking the <paramref name="onValue"/> function.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="onValue">The function to execute if the state is a value.</param>
        /// <returns>The result from calling <paramref name="onValue"/> if state is value; otherwise the original errors.</returns>
        public static async Task<ResultOrError<TNextValue>> Then<TValue, TNextValue>(this Task<ResultOrError<TValue>> errorOr, Func<TValue, ResultOrError<TNextValue>> onValue)
        {
            var result = await errorOr.ConfigureAwait(false);

            return result.Then(onValue);
        }

        /// <summary>
        /// If the state of <paramref name="errorOr"/> is a value, the provided function <paramref name="onValue"/> is executed and its result is returned.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <typeparam name="TNextValue">The type of the result from invoking the <paramref name="onValue"/> function.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="onValue">The function to execute if the state is a value.</param>
        /// <returns>The result from calling <paramref name="onValue"/> if state is value; otherwise the original errors.</returns>
        public static async Task<ResultOrError<TNextValue>> Then<TValue, TNextValue>(this Task<ResultOrError<TValue>> errorOr, Func<TValue, TNextValue> onValue)
        {
            var result = await errorOr.ConfigureAwait(false);

            return result.Then(onValue);
        }

        /// <summary>
        /// If the state of <paramref name="errorOr"/> is a value, the provided <paramref name="action"/> is invoked.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="action">The action to execute if the state is a value.</param>
        /// <returns>The original <paramref name="errorOr"/>.</returns>
        public static async Task<ResultOrError<TValue>> ThenDo<TValue>(this Task<ResultOrError<TValue>> errorOr, Action<TValue> action)
        {
            var result = await errorOr.ConfigureAwait(false);

            return result.ThenDo(action);
        }

        /// <summary>
        /// If the state of <paramref name="errorOr"/> is a value, the provided function <paramref name="onValue"/> is executed asynchronously and its result is returned.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <typeparam name="TNextValue">The type of the result from invoking the <paramref name="onValue"/> function.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="onValue">The function to execute if the state is a value.</param>
        /// <returns>The result from calling <paramref name="onValue"/> if state is value; otherwise the original errors.</returns>
        public static async Task<ResultOrError<TNextValue>> ThenAsync<TValue, TNextValue>(this Task<ResultOrError<TValue>> errorOr, Func<TValue, Task<ResultOrError<TNextValue>>> onValue)
        {
            var result = await errorOr.ConfigureAwait(false);

            return await result.ThenAsync(onValue).ConfigureAwait(false);
        }

        /// <summary>
        /// If the state of <paramref name="errorOr"/> is a value, the provided function <paramref name="onValue"/> is executed asynchronously and its result is returned.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <typeparam name="TNextValue">The type of the result from invoking the <paramref name="onValue"/> function.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="onValue">The function to execute if the state is a value.</param>
        /// <returns>The result from calling <paramref name="onValue"/> if state is value; otherwise the original errors.</returns>
        public static async Task<ResultOrError<TNextValue>> ThenAsync<TValue, TNextValue>(this Task<ResultOrError<TValue>> errorOr, Func<TValue, Task<TNextValue>> onValue)
        {
            var result = await errorOr.ConfigureAwait(false);

            return await result.ThenAsync(onValue).ConfigureAwait(false);
        }

        /// <summary>
        /// If the state of <paramref name="errorOr"/> is a value, the provided <paramref name="action"/> is executed asynchronously.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="action">The action to execute if the state is a value.</param>
        /// <returns>The original <paramref name="errorOr"/>.</returns>
        public static async Task<ResultOrError<TValue>> ThenDoAsync<TValue>(this Task<ResultOrError<TValue>> errorOr, Func<TValue, Task> action)
        {
            var result = await errorOr.ConfigureAwait(false);

            return await result.ThenDoAsync(action).ConfigureAwait(false);
        }
    }
}
