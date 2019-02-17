using System;
using System.Collections.Generic;
using System.Text;

namespace Result
{
    public readonly struct Result<T, TError>
    {
        private readonly T value;
        private readonly TError error;
        private readonly bool hasValue;
        private readonly bool isInitialized;

        private Result(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            this.value = value;
            this.error = default;
            this.hasValue = true;
            this.isInitialized = true;
        }

        private Result(TError error)
        {
            if (error == null)
                throw new ArgumentNullException(nameof(error));

            this.value = default;
            this.error = error;
            this.hasValue = false;
            this.isInitialized = true;
        }

        public static Result<T, TError> Success(T value)
            => new Result<T, TError>(value);

        public static Result<T, TError> Fail(TError error)
            => new Result<T, TError>(error);

        public Result<U, TError> Map<U>(Func<T, U> mapper)
        {
            EnsureInitialized();

            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));

            if (hasValue)
                return new Result<U, TError>(mapper(value));

            return new Result<U, TError>(error);
        }

        public Result<U, UError> Map<U, UError>(Func<T, U> success, Func<TError, UError> error)
        {
            EnsureInitialized();

            if (success == null)
                throw new ArgumentNullException(nameof(success));

            if (error == null)
                throw new ArgumentNullException(nameof(error));

            if (hasValue)
                return new Result<U, UError>(success(value));

            return new Result<U, UError>(error(this.error));
        }

        public Result<TResult, TError> FlatMap<TResult>(Func<T, Result<TResult, TError>> mapper)
        {
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));

            return Match(
                success: mapper,
                fail: exception => new Result<TResult, TError>(exception)
            );
        }

        public U Match<U>(Func<T, U> success, Func<TError, U> fail)
        {
            EnsureInitialized();

            if (success == null)
                throw new ArgumentNullException(nameof(success));

            if (fail == null)
                throw new ArgumentNullException(nameof(fail));

            if (hasValue)
                return success(value);

            return fail(error);
        }

        private void EnsureInitialized()
        {
            if (!isInitialized)
                throw new InvalidOperationException("Result is not initialized");
        }

        public static implicit operator Result<T, TError>(Success<T> success)
            => Success(success.Value);
        public static implicit operator Result<T, TError>(Fail<TError> fail)
            => Fail(fail.Error);
    }
}
