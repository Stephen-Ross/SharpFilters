using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SharpFilters.Extensions;
using SharpFilters.Factories.Models;
using SharpFilters.Models;

namespace SharpFilters.Transformers
{
    internal class DigitalTransformer : IDigitalTransformer
    {
        private readonly IPolesCoefficientsFactory polesCoefficientsFactory;

        public DigitalTransformer(IPolesCoefficientsFactory polesCoefficientsFactory)
        {
            this.polesCoefficientsFactory = polesCoefficientsFactory;
        }

        public IPolesCoefficients Transform(IPolesCoefficients polesCoefficients, double sampleRate)
        {
            var degree = polesCoefficients.P.Count - polesCoefficients.Z.Count;

            var zZ = polesCoefficients.Z.Add(sampleRate).Divide(sampleRate.Subtract(polesCoefficients.Z));
            var pZ = polesCoefficients.P.Add(sampleRate).Divide(sampleRate.Subtract(polesCoefficients.P));

            var z = new List<Complex>(zZ);

            for (var i = 0; i < degree; i++)
            {
                z.Add(-1.0d);
            }

            var kZ = polesCoefficients.K *
                     (sampleRate.Subtract(polesCoefficients.Z).Product() /
                      sampleRate.Subtract(polesCoefficients.P).Product());

            return this.polesCoefficientsFactory.Build(kZ.Real, pZ.ToList(), z);
        }
    }
}