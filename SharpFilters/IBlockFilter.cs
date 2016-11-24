// Copyright © Stephen Ross 2016

using System.Collections.Generic;

namespace SharpFilters
{
    public interface IBlockFilter
    {
        IReadOnlyList<double> Filter(IReadOnlyList<double> data);
    }
}