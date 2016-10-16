// Stephen Ross 2016.

using System.Collections.Generic;
using System.Numerics;
using SharpFilters.Models;

namespace SharpFilters.Factories.Models
{
    internal interface IPolesCoefficientsFactory
    {
        IPolesCoefficients Build(double k, IReadOnlyList<Complex> p, IReadOnlyList<Complex> z);
    }
}