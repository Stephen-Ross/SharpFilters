using System.Collections.Generic;

namespace SharpFilters
{
    public interface IBlockFilter
    {
        IReadOnlyList<double> Filter(IReadOnlyList<double> data);
    }
}