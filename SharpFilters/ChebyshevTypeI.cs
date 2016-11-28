// Copyright © Stephen Ross 2016

using System;
using SharpFilters.Analogs;
using SharpFilters.Enums;
using SharpFilters.Factories.Analogs;
using SharpFilters.Models;

namespace SharpFilters
{
    /// <summary>
    /// Implementation of the Chebyshev Type I Filter design.
    /// </summary>
    public sealed class ChebyshevTypeI : BaseFilterDesign, IChebyshevTypeI
    {
        private readonly IChebyshevTypeIAnalog chebyshevTypeIAnalog;

        private double ripple;

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
            : base(filterType)
        {
            var chebyshevTypeIFactory = new ChebyshevTypeIAnalogFactory(polesCoefficientsFactory);
            this.chebyshevTypeIAnalog = chebyshevTypeIFactory.Build();

            this.Compose(order, cutoff, ripple);
        }

        /// <inheritdoc />
        public double Ripple
        {
            get { return ripple; }
            private set { ripple = value; }
        }

        /// <inheritdoc />
        public void Compose(int order, double cutoff, double ripple)
        {
            this.Order = order;
            this.Ripple = ripple;

            this.chebyshevTypeIAnalog.CalculateAnalog(order, ripple);
            this.Compose(this.chebyshevTypeIAnalog, cutoff);
        }
    }
}