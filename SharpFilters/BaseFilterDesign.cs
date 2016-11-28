// Copyright © Stephen Ross 2016

using SharpFilters.Analogs;
using SharpFilters.Enums;
using SharpFilters.Factories.Models;
using SharpFilters.Models;
using SharpFilters.Providers;
using SharpFilters.Transformers;

namespace SharpFilters
{
    /// <summary>
    /// Base class for Filter Designs.
    /// </summary>
    public abstract class BaseFilterDesign : IFilterDesign
    {
        private readonly IIirProvider iirProvider;

        internal readonly IPolesCoefficientsFactory polesCoefficientsFactory;

        private double cutoff;

        private int order;

        private IPolynomialCoefficients polynomialCoefficients;

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

        /// <inheritdoc />
        public double Cutoff
        {
            get { return cutoff; }
            private set { cutoff = value; }
        }

        /// <inheritdoc />
        public int Order
        {
            get { return order; }
            protected set { order = value; }
        }

        /// <inheritdoc />
        public IPolynomialCoefficients PolynomialCoefficients
        {
            get { return polynomialCoefficients; }
            private set { polynomialCoefficients = value; }
        }

        internal void Compose(IAnalog analog, double cutoff)
        {
            this.Cutoff = cutoff;
            this.PolynomialCoefficients = this.iirProvider.GetIirCoefficients(analog, cutoff);
        }
    }
}