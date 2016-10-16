// Stephen Ross 2016.

using System.Collections.Generic;
using System.Numerics;

namespace SharpFilters.Models
{
    internal interface IPolesCoefficients
    {
        double K { get; }

        IReadOnlyList<Complex> P { get; }

        IReadOnlyList<Complex> Z { get; }
    }
}