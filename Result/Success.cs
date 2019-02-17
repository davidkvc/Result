using System;
using System.Collections.Generic;
using System.Text;

namespace Result
{
    public struct Success<T>
    {
        internal T Value { get; }

        internal Success(T value)
        {
            Value = value;
        }
    }
}
