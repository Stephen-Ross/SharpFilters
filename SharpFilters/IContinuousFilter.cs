// Copyright © Stephen Ross 2016

namespace SharpFilters
{
    /// <summary>
    /// Represents an IIR Filter which runs over one sample at a time.
    /// </summary>
    public interface IContinuousFilter
    {
        /// <summary>
        /// Filters the data provided based on the previously filtered data.
        /// </summary>
        /// <param name="data">
        /// The data to be filtered.
        /// </param>
        /// <returns>
        /// The filtered data.
        /// </returns>
        double Filter(double data);

        /// <summary>
        /// Resets the filtering to save on newing up a new object.
        /// </summary>
        void Reset();
    }
}