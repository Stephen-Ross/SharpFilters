// Copyright © Stephen Ross 2016

using SharpFilters.Models;

namespace SharpFilters.Transformers
{
    internal interface IDigitalTransformer
    {
        IPolesCoefficients Transform(IPolesCoefficients polesCoefficients, double sampleRate);
    }
}