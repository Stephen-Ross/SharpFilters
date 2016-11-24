// Copyright © Stephen Ross 2016

using SharpFilters.Models;

namespace SharpFilters
{
    public interface IFilterDesign
    {
        IPolynomialCoefficients PolynomialCoefficients { get; }
    }
}