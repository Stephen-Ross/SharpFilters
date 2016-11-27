// Copyright © Stephen Ross 2016

using SharpFilters.Analogs;
using SharpFilters.Enums;
using SharpFilters.Factories.Models;
using SharpFilters.Models;
using SharpFilters.Providers;
using SharpFilters.Transformers;

namespace SharpFilters
{
    public abstract class BaseFilterDesign
    {
        private readonly IIirProvider iirProvider;

        internal readonly IPolesCoefficientsFactory polesCoefficientsFactory;

        protected BaseFilterDesign(FilterType filterType)
        {
            this.polesCoefficientsFactory = new PolesCoefficientsFactory();

            ITransformer transformer;
            if (filterType == FilterType.Highpass)
            {
                transformer = new HighpassTransformer(this.polesCoefficientsFactory);
            }
            else
            {
                transformer = new LowPassTransformer(this.polesCoefficientsFactory);
            }

            this.iirProvider =
                new IirProvider(
                    new DigitalPolesProvider(transformer, new DigitalTransformer(polesCoefficientsFactory)),
                    new PolynomialTransformer(new PolynomialCoefficientsFactory()));
        }

        internal IPolynomialCoefficients Compose(IAnalog analog, double cutoff)
        {
            return this.iirProvider.GetIirCoefficients(analog, cutoff);
        }
    }
}