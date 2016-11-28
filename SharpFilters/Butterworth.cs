// Copyright © Stephen Ross 2016

using System;
using SharpFilters.Analogs;
using SharpFilters.Enums;
using SharpFilters.Factories.Analogs;
using SharpFilters.Models;

namespace SharpFilters
{
    /// <summary>
    /// Implementation of the Butterworth Filter design.
    /// </summary>
    public sealed class Butterworth : BaseFilterDesign, IButterworth
    {
        private readonly IButterworthAnalog butterworthAnalog;

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
            : base(filterType)
        {
            var butterworthAnalogFactory = new ButterworthAnalogFactory(polesCoefficientsFactory);
            this.butterworthAnalog = butterworthAnalogFactory.Build();

            this.Compose(order, cutoff);
        }

        /// <inheritdoc />
        public void Compose(int order, double cutoff)
        {
            this.Order = order;

            this.butterworthAnalog.CalculateAnalog(order);
            this.Compose(this.butterworthAnalog, cutoff);
        }
    }
}