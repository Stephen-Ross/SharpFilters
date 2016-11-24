// Copyright © Stephen Ross 2016

using SharpFilters.Analogs;
using SharpFilters.Enums;
using SharpFilters.Factories.Analogs;
using SharpFilters.Factories.Models;
using SharpFilters.Models;
using SharpFilters.Providers;
using SharpFilters.Transformers;

namespace SharpFilters
{
    public class Butterworth : IButterworth
    {
        private readonly IButterworthAnalog butterworthAnalog;

        private readonly IIirProvider iirProvider;

        private IPolynomialCoefficients polynomialCoefficients;

        public Butterworth(FilterType filterType)
        {
            var polesCoefficientsFactory = new PolesCoefficientsFactory();
            var butterworthAnalogFactory = new ButterworthAnalogFactory(polesCoefficientsFactory);
            this.butterworthAnalog = butterworthAnalogFactory.Build();

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
            get { return this.polynomialCoefficients; }
            private set { this.polynomialCoefficients = value; }
        }

        public void Compose(int order, double cutoff)
        {
            this.butterworthAnalog.CalculateAnalog(order);
            this.PolynomialCoefficients = this.iirProvider.GetIirCoefficients(this.butterworthAnalog, cutoff);
        }
    }
}