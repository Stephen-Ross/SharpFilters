using System.Collections.Generic;

namespace SharpFilters.Models
{
    internal interface IPolynomialCoefficients
    {
        IReadOnlyList<double> A { get; }

        IReadOnlyList<double> B { get; }
    }
}