// Stephen Ross 2016.

using System.Collections.Generic;
using System.Numerics;
using SharpFilters.Models;

namespace SharpFilters.Factories.Models
{
    internal class PolesCoefficientsFactory : IPolesCoefficientsFactory
    {
        public IPolesCoefficients Build(double k, IReadOnlyList<Complex> p, IReadOnlyList<Complex> z)
        {
            return new PolesCoefficients(k, p, z);
        }
    }
}