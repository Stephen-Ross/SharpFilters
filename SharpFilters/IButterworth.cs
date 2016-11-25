// Copyright © Stephen Ross 2016

using System;

namespace SharpFilters
{
    /// <summary>
    /// Represents the filter design for a Butterworth filter.
    /// </summary>
    public interface IButterworth : IFilterDesign
    {
        /// <summary>
        /// Composes the filter based upon the order and cutoff supplied.
        /// </summary>
        /// <param name="order">
        /// The n-th order of the filter.
        /// </param>
        /// <param name="cutoff">
        /// The cutoff frequency of the filter. The frequency at which the magnitude response of the filter is 1 / √2.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if the order supplied is less than 1.
        /// </exception>
        void Compose(int order, double cutoff);
    }
}