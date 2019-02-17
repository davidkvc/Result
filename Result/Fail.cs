using System;
using System.Collections.Generic;
using System.Text;

namespace Result
{
    public struct Fail<TError>
    {
        internal TError Error { get; }

        internal Fail(TError error)
        {
            Error = error;
        }
    }
}
