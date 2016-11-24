// Copyright © Stephen Ross 2016

using System.Collections.Generic;

namespace SharpFilters.Models
{
    public interface IPolynomialCoefficients
    {
        IReadOnlyList<double> A { get; }

        IReadOnlyList<double> B { get; }
    }
}