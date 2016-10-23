using System.Collections.Generic;
using SharpFilters.Models;

namespace SharpFilters.Factories.Models
{
    internal interface IPolynomialCoefficientsFactory
    {
        IPolynomialCoefficients Build(IReadOnlyList<double> a, IReadOnlyList<double> b);
    }
}