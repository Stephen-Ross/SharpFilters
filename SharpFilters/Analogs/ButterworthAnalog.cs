// Copyright © Stephen Ross 2016

using System;
using System.Collections.Generic;
using System.Numerics;
using SharpFilters.Factories.Models;
using SharpFilters.Models;
using static System.Math;

namespace SharpFilters.Analogs
{
    internal class ButterworthAnalog : IButterworthAnalog
    {
        private readonly IPolesCoefficientsFactory polesCoefficientsFactory;

        private IPolesCoefficients coefficients;

        public ButterworthAnalog(IPolesCoefficientsFactory polesCoefficientsFactory)
        {
            this.polesCoefficientsFactory = polesCoefficientsFactory;
        }

        public IPolesCoefficients Coefficients
        {
            get { return coefficients; }
            private set { coefficients = value; }
        }

        public void CalculateAnalog(int order)
        {
            if (order < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(order), @"Order must be greater than 0.");
            }

            var z = new List<Complex>();
            var p = new List<Complex>();

            var start = -order + 1;

            for (var i = 0; i < order; i++, start += 2)
            {
                p.Add(-Complex.Exp(new Complex(0.0d, 1.0d) * PI * start / (2 * order)));
            }

            this.Coefficients = this.polesCoefficientsFactory.Build(1.0d, p, z);
        }
    }
}