// Copyright © Stephen Ross 2016

using System.Collections.Generic;
using SharpFilters.Models;

namespace SharpFilters.Factories.Models
{
    internal interface IPolynomialCoefficientsFactory
    {
        IPolynomialCoefficients Build(IReadOnlyList<double> a, IReadOnlyList<double> b);
    }
}