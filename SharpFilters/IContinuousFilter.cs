// Copyright © Stephen Ross 2016

namespace SharpFilters
{
    public interface IContinuousFilter
    {
        double Filter(double data);

        void Reset();
    }
}