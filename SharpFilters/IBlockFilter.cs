// Copyright © Stephen Ross 2016

using System.Collections.Generic;

namespace SharpFilters
{
    /// <summary>
    /// Represents an IIR Filter which runs over a block of independent data.
    /// </summary>
    public interface IBlockFilter
    {
        /// <summary>
        /// Filters the data supplied.
        /// </summary>
        /// <param name="data">
        /// The data to be filtered.
        /// </param>
        /// <returns>
        /// The filtered data.
        /// </returns>
        IReadOnlyList<double> Filter(IReadOnlyList<double> data);
    }
}