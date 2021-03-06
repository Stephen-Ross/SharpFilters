﻿// Copyright © Stephen Ross 2016

using System;
using System.Linq;
using SharpFilters.Analogs;
using SharpFilters.Extensions;
using SharpFilters.Factories.Models;
using SharpFilters.Models;

namespace SharpFilters.Transformers
{
    internal class LowPassTransformer : ITransformer
    {
        private readonly IPolesCoefficientsFactory polesCoefficientsFactory;

        public LowPassTransformer(IPolesCoefficientsFactory polesCoefficientsFactory)
        {
            this.polesCoefficientsFactory = polesCoefficientsFactory;
        }

        public IPolesCoefficients Transform(IAnalog analog, double cutoff)
        {
            var degree = analog.Coefficients.P.Count - analog.Coefficients.Z.Count;

            var zLp = analog.Coefficients.Z.Multiply(cutoff).ToList();
            var pLp = analog.Coefficients.P.Multiply(cutoff).ToList();
            var kLp = analog.Coefficients.K * Math.Pow(cutoff, degree);

            return this.polesCoefficientsFactory.Build(kLp, pLp, zLp);
        }
    }
}