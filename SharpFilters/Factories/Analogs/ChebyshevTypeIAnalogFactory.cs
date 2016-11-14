using SharpFilters.Analogs;
using SharpFilters.Factories.Models;

namespace SharpFilters.Factories.Analogs
{
    internal class ChebyshevTypeIAnalogFactory : IChebyshevTypeIAnalogFactory
    {
        private readonly IPolesCoefficientsFactory polesCoefficientsFactory;

        public ChebyshevTypeIAnalogFactory(IPolesCoefficientsFactory polesCoefficientsFactory)
        {
            this.polesCoefficientsFactory = polesCoefficientsFactory;
        }

        public IChebyshevTypeIAnalog Build()
        {
            return new ChebyshevTypeIAnalog(this.polesCoefficientsFactory);
        }
    }
}