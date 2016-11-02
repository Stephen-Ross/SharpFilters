using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SharpFilters.Analogs;
using SharpFilters.Extensions;
using SharpFilters.Factories.Models;
using SharpFilters.Models;

namespace SharpFilters.Transformers
{
    internal class HighpassTransformer : ITransformer
    {
        private readonly IPolesCoefficientsFactory polesCoefficientsFactory;

        public HighpassTransformer(IPolesCoefficientsFactory polesCoefficientsFactory)
        {
            this.polesCoefficientsFactory = polesCoefficientsFactory;
        }

        public IPolesCoefficients Transform(IAnalog analog, double cutoff)
        {
            var degree = analog.Coefficients.P.Count - analog.Coefficients.Z.Count;

            var zHp = analog.Coefficients.Z.RhsDivide(cutoff);
            var pHp = analog.Coefficients.P.RhsDivide(cutoff);

            var z = new List<Complex>(zHp);

            for (var i = 0; i < degree; i++)
            {
                z.Add(0.0d);
            }

            var kHp = analog.Coefficients.K *
                      (analog.Coefficients.Z.Negative().Product() / analog.Coefficients.P.Negative().Product());

            return this.polesCoefficientsFactory.Build(kHp.Real, pHp.ToList(), z);
        }
    }
}