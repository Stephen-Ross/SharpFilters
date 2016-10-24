using System.Collections.Generic;

namespace SharpFilters
{
    public interface IFilter
    {
        IReadOnlyList<double> Filter(IReadOnlyList<double> values);
    }
}