using System;
using System.Collections.Generic;
using System.Numerics;
using SharpFilters.Extensions;
using SharpFilters.Factories.Models;
using SharpFilters.Models;

namespace SharpFilters.Analogs
{
    internal class ChebyshevTypeIAnalog : IChebyshevTypeIAnalog
    {
        private readonly IPolesCoefficientsFactory polesCoefficientsFactory;

        private IPolesCoefficients polesCoefficients;

        public ChebyshevTypeIAnalog(IPolesCoefficientsFactory polesCoefficientsFactory)
        {
            this.polesCoefficientsFactory = polesCoefficientsFactory;
        }

        public IPolesCoefficients Coefficients
        {
            get { return polesCoefficients; }
            private set { this.polesCoefficients = value; }
        }

        public IPolesCoefficients CalculateAnalog(int order, double ripple)
        {
            if (order < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(order), @"Order must be greater than 0.");
            }

            if (ripple < 1.0d)
            {
                throw new ArgumentOutOfRangeException(nameof(ripple), @"Ripple must be greater than 0.");
            }

            var z = new List<Complex>();

            var eps = Math.Sqrt(Math.Pow(10.0d, 0.1d * ripple) - 1.0d);
            var mu = 1.0d / order * ExtendedMath.ArcSinh(1 / eps);

            var p = new List<Complex>();

            var start = 0;
            for (var i = 0; i < order; i++, start += 2)
            {
                var theta = start * Math.PI / (2.0 * order);

                p.Add(-Complex.Sinh(mu + theta * new Complex(0.0d, 1.0d)));
            }

            var k = p.Negative().Product().Real;
            if (order % 2 == 0)
            {
                k = k / Math.Sqrt(1 + eps * eps);
            }

            return this.polesCoefficientsFactory.Build(k, p, z);
        }
    }
}