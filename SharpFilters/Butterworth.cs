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
    /// Implementation of the Butterworth Filter design.
    /// </summary>
    public class Butterworth : IButterworth
    {
        private readonly IButterworthAnalog butterworthAnalog;

        private readonly IIirProvider iirProvider;

        private IPolynomialCoefficients polynomialCoefficients;

        /// <summary>
        /// Builds a new Butterworth design filter for the supplied filter type with the specified order and cutoff.
        /// </summary>
        /// <param name="filterType">
        /// The Filter Type required.
        /// </param>
        /// <param name="order">
        /// The n-th order of the filter.
        /// </param>
        /// <param name="cutoff">
        /// The cutoff frequency of the filter. The frequency at which the magnitude response of the filter is 1 / √2.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if the order supplied is less than 1.
        /// </exception>
        public Butterworth(FilterType filterType, int order, double cutoff)
        {
            var polesCoefficientsFactory = new PolesCoefficientsFactory();
            var butterworthAnalogFactory = new ButterworthAnalogFactory(polesCoefficientsFactory);
            this.butterworthAnalog = butterworthAnalogFactory.Build();

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

            this.Compose(order, cutoff);
        }

        /// <inheritdoc />
        public IPolynomialCoefficients PolynomialCoefficients
        {
            get { return this.polynomialCoefficients; }
            private set { this.polynomialCoefficients = value; }
        }

        /// <inheritdoc />
        public void Compose(int order, double cutoff)
        {
            this.butterworthAnalog.CalculateAnalog(order);
            this.PolynomialCoefficients = this.iirProvider.GetIirCoefficients(this.butterworthAnalog, cutoff);
        }
    }
}