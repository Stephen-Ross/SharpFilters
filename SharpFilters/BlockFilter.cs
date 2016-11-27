// Copyright © Stephen Ross 2016

using System.Collections.Generic;

namespace SharpFilters
{
    /// <summary>
    /// Implementation of a block IIR Filter.
    /// </summary>
    public sealed class BlockFilter : IBlockFilter
    {
        private readonly IFilterDesign filterDesign;

        /// <summary>
        /// Builds a new Block filter using the supplied filter design.
        /// </summary>
        /// <param name="filterDesign">
        /// The design of the filter providing the build polynomials.
        /// </param>
        public BlockFilter(IFilterDesign filterDesign)
        {
            this.filterDesign = filterDesign;
        }

        /// <inheritdoc />
        public IReadOnlyList<double> Filter(IReadOnlyList<double> data)
        {
            var filteredData = new List<double>(data.Count);

            var continiousFilter = new ContinuousFilter(this.filterDesign);

            for (var i = 0; i < data.Count; i++)
            {
                filteredData.Add(continiousFilter.Filter(data[i]));
            }

            return filteredData;
        }
    }
}