// Copyright © Stephen Ross 2016

namespace SharpFilters
{
    public interface IButterworth : IFilterDesign
    {
        void Compose(int order, double cutoff);
    }
}