using System;
using System.Collections.Generic;
using System.Text;

namespace Result
{
    public sealed class Unit
    {
        public static readonly Unit Instance = new Unit();

        private Unit() { }
    }
}
