using System;
using System.Collections.Generic;
using System.Text;

namespace Result
{
    public static class ResultLinqExtensions
    {
        public static Result<U, TError> Select<T, U, TError>(this Result<T, TError> result, Func<T, U> selector)
            => result.Map(selector);

        public static Result<U, TError> SelectMany<T, U, TError>(this Result<T, TError> result, Func<T, Result<U,  TError>> selector)
            => result.FlatMap(selector);

        public static Result<V, TError> SelectMany<T, U, V, TError>(this Result<T, TError> result, Func<T, Result<U, TError>> intermediateSelector, Func<T, U, V> selector)
            => result.FlatMap(x => intermediateSelector(x).Map(y => selector(x, y)));
    }
}
