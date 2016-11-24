// Copyright © Stephen Ross 2016

using SharpFilters.Analogs;
using SharpFilters.Factories.Models;

namespace SharpFilters.Factories.Analogs
{
    internal class ButterworthAnalogFactory : IButterworthAnalogFactory
    {
        private readonly IPolesCoefficientsFactory polesCoefficientsFactory;

        public ButterworthAnalogFactory(IPolesCoefficientsFactory polesCoefficientsFactory)
        {
            this.polesCoefficientsFactory = polesCoefficientsFactory;
        }

        public IButterworthAnalog Build()
        {
            return new ButterworthAnalog(this.polesCoefficientsFactory);
        }
    }
}