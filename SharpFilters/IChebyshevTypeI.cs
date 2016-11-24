// Copyright © Stephen Ross 2016

namespace SharpFilters
{
    public interface IChebyshevTypeI : IFilterDesign
    {
        void Compose(int order, double cutoff, double ripple);
    }
}