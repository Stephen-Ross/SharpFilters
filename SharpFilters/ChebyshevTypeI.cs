using SharpFilters.Analogs;
using SharpFilters.Factories.Analogs;
using SharpFilters.Factories.Models;
using SharpFilters.Models;
using SharpFilters.Providers;
using SharpFilters.Transformers;

namespace SharpFilters
{
    public class ChebyshevTypeI : IChebyshevTypeI
    {
        private readonly IChebyshevTypeIAnalog chebyshevTypeIAnalog;

        private readonly IIirProvider iirProvider;

        private IPolynomialCoefficients polynomialCoefficients;

        public ChebyshevTypeI(FilterType filterType)
        {
            var polesCoefficientsFactory = new PolesCoefficientsFactory();
            var chebyshevTypeIFactory = new ChebyshevTypeIAnalogFactory(polesCoefficientsFactory);
            this.chebyshevTypeIAnalog = chebyshevTypeIFactory.Build();

            ITransformer transformer;
            if (filterType == FilterType.Highpass)
            {
                transformer = new HighpassTransformer(polesCoefficientsFactory);
            }
            else
            {
                transformer = new LowPassTransformer(polesCoefficientsFactory);
            }

            this.iirProvider =
                new IirProvider(
                    new DigitalPolesProvider(transformer, new DigitalTransformer(polesCoefficientsFactory)),
                    new PolynomialTransformer(new PolynomialCoefficientsFactory()));
        }

        public IPolynomialCoefficients PolynomialCoefficients
        {
            get
            {
                return polynomialCoefficients;
            }
            private set
            {
                polynomialCoefficients = value;
            }
        }

        public void Compose(int order, double cutoff, double ripple)
        {
            this.chebyshevTypeIAnalog.CalculateAnalog(order, ripple);
            this.PolynomialCoefficients = this.iirProvider.GetIirCoefficients(this.chebyshevTypeIAnalog, cutoff);
        }
    }
}