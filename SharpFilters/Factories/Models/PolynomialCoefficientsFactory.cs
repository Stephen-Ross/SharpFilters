// Copyright © Stephen Ross 2016

using System.Collections.Generic;
using SharpFilters.Models;

namespace SharpFilters.Factories.Models
{
    internal class PolynomialCoefficientsFactory : IPolynomialCoefficientsFactory
    {
        public IPolynomialCoefficients Build(IReadOnlyList<double> a, IReadOnlyList<double> b)
        {
            return new PolynomialCoefficients(a, b);
        }
    }
}