// Copyright © Stephen Ross 2016

using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace SharpFilters.Extensions
{
    internal static class DoubleExtensions
    {
        public static IEnumerable<Complex> Subtract(this double value, IEnumerable<Complex> complexes)
        {
            return complexes.Select(x => value - x);
        }
    }
}