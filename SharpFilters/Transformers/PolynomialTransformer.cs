// Copyright © Stephen Ross 2016

using System.Linq;
using SharpFilters.Extensions;
using SharpFilters.Factories.Models;
using SharpFilters.Models;

namespace SharpFilters.Transformers
{
    internal class PolynomialTransformer : IPolynomialTransformer
    {
        private readonly IPolynomialCoefficientsFactory polynomialCoefficientsFactory;

        public PolynomialTransformer(IPolynomialCoefficientsFactory polynomialCoefficientsFactory)
        {
            this.polynomialCoefficientsFactory = polynomialCoefficientsFactory;
        }

        public IPolynomialCoefficients Transform(IPolesCoefficients polesCoefficients)
        {
            var complexB = polesCoefficients.Z.PolynomialCoefficients().Multiply(polesCoefficients.K);
            var complexA = polesCoefficients.P.PolynomialCoefficients();

            return this.polynomialCoefficientsFactory.Build(complexA.Select(x => x.Real).ToList(),
                complexB.Select(x => x.Real).ToList());
        }
    }
}