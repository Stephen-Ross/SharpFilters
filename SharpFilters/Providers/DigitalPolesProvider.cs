// Copyright © Stephen Ross 2016

using SharpFilters.Analogs;
using SharpFilters.Models;
using SharpFilters.Transformers;
using static System.Math;

namespace SharpFilters.Providers
{
    internal class DigitalPolesProvider : IDigitalPolesProvider
    {
        private readonly IDigitalTransformer digitalTransformer;

        private readonly ITransformer transformer;

        public DigitalPolesProvider(ITransformer transformer, IDigitalTransformer digitalTransformer)
        {
            this.transformer = transformer;
            this.digitalTransformer = digitalTransformer;
        }

        public IPolesCoefficients GetDigitalPoles(IAnalog analog, double cutoff, double sampleRate)
        {
            var warped = sampleRate * 2.0d * Tan
                             (PI * cutoff / sampleRate);

            var transformedPoles = this.transformer.Transform(analog, warped);

            return this.digitalTransformer.Transform(transformedPoles, sampleRate * 2.0d);
        }
    }
}