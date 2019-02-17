using System;
using System.Collections.Generic;
using System.Text;

namespace Result
{
    public static class Result
    {
        public static Success<T> Success<T>(T value)
            => new Success<T>(value);

        public static Fail<TError> Fail<TError>(TError error)
            => new Fail<TError>(error);
    }
}
