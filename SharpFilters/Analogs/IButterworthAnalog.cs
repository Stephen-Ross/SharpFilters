// Copyright © Stephen Ross 2016

namespace SharpFilters.Analogs
{
    internal interface IButterworthAnalog : IAnalog
    {
        void CalculateAnalog(int order);
    }
}