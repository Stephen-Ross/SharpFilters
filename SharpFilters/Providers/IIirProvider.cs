// Copyright © Stephen Ross 2016

using SharpFilters.Analogs;
using SharpFilters.Models;

namespace SharpFilters.Providers
{
    internal interface IIirProvider
    {
        IPolynomialCoefficients GetIirCoefficients(IAnalog analog, double cutoff);
    }
}