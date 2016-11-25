// Copyright © Stephen Ross 2016

using System;
using SharpFilters.Analogs;
using SharpFilters.Enums;
using SharpFilters.Factories.Analogs;
using SharpFilters.Factories.Models;
using SharpFilters.Models;
using SharpFilters.Providers;
using SharpFilters.Transformers;

namespace SharpFilters
{
    /// <summary>
    /// Implementation of the Chebyshev Type I Filter design.
    /// </summary>
    public class ChebyshevTypeI : IChebyshevTypeI
    {
        private readonly IChebyshevTypeIAnalog chebyshevTypeIAnalog;

        private readonly IIirProvider iirProvider;

        private IPolynomialCoefficients polynomialCoefficients;

        /// <summary>
        /// Builds a new Chebyshev Type I filter for the supplied filter type with the specified order, cutoff and ripple.
        /// </summary>
        /// <param name="filterType">
        /// The Filter Type required.
        /// </param>
        /// <param name="order">
        /// The n-th order of the filter.
        /// </param>
        /// <param name="cutoff">
        /// The passband edge frequency. The frequency at which the magnitude response of the filter is -ripple.
        /// </param>
        /// <param name="ripple">
        /// The peak-to-peak passband ripple. Expressed in decibels.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Raised if order is less than 1 or if the ripple is less than or equal to zero.
        /// </exception>
        public ChebyshevTypeI(FilterType filterType, int order, double cutoff, double ripple)
        {
            var polesCoefficientsFactory = new PolesCoefficientsFactory();
            var chebyshevTypeIFactory = new ChebyshevTypeIAnalogFactory(polesCoefficientsFactory);
            this.chebyshevTypeIAnalog = chebyshevTypeIFactory.Build();

            ITransformer transformer;
            if (filterType == FilterType.Highpass)
            {
                transformer = new HighpassTransformer(polesCoefficientsFactory);
            }
            else
            {
                transformer = new LowPassTransformer(polesCoefficientsFactory);
            }

            this.iirProvider =
                new IirProvider(
                    new DigitalPolesProvider(transformer, new DigitalTransformer(polesCoefficientsFactory)),
                    new PolynomialTransformer(new PolynomialCoefficientsFactory()));

            this.Compose(order, cutoff, ripple);
        }

        /// <inheritdoc />
        public IPolynomialCoefficients PolynomialCoefficients
        {
            get
            {
                return polynomialCoefficients;
            }
            private set
            {
                polynomialCoefficients = value;
            }
        }

        /// <inheritdoc />
        public void Compose(int order, double cutoff, double ripple)
        {
            this.chebyshevTypeIAnalog.CalculateAnalog(order, ripple);
            this.PolynomialCoefficients = this.iirProvider.GetIirCoefficients(this.chebyshevTypeIAnalog, cutoff);
        }
    }
}