using System.Collections.Generic;

namespace SharpFilters
{
    public sealed class BlockFilter : IBlockFilter
    {
        private readonly IFilterDesign filterDesign;

        public BlockFilter(IFilterDesign filterDesign)
        {
            this.filterDesign = filterDesign;
        }

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