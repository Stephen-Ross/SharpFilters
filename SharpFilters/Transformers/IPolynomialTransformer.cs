// Copyright © Stephen Ross 2016

using SharpFilters.Models;

namespace SharpFilters.Transformers
{
    internal interface IPolynomialTransformer
    {
        IPolynomialCoefficients Transform(IPolesCoefficients polesCoefficients);
    }
}