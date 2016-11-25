// Copyright © Stephen Ross 2016

using System;

namespace SharpFilters
{
    /// <summary>
    /// Represents the filter design for a Chebyshev Type I filter.
    /// </summary>
    public interface IChebyshevTypeI : IFilterDesign
    {
        /// <summary>
        /// Composes the filter based button the order, cutoff and ripple supplied.
        /// </summary>
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
        void Compose(int order, double cutoff, double ripple);
    }
}