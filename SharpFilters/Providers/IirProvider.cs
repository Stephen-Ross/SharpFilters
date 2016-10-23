using SharpFilters.Analogs;
using SharpFilters.Models;
using SharpFilters.Transformers;

namespace SharpFilters.Providers
{
    internal class IirProvider : IIirProvider
    {
        private readonly IDigitialPolesProvider digitalPolesProvider;

        private readonly IPolynomialTransformer polynomialTransformer;

        public IirProvider(IDigitialPolesProvider digitalPolesProvider, IPolynomialTransformer polynomialTransformer)
        {
            this.digitalPolesProvider = digitalPolesProvider;
            this.polynomialTransformer = polynomialTransformer;
        }

        public IPolynomialCoefficients GetIirCoefficients(IAnalog analog, double cutoff)
        {
            var digitialPoles = this.digitalPolesProvider.GetDigitalPoles(analog, cutoff, 2.0d);

            return this.polynomialTransformer.Transform(digitialPoles);
        }
    }
}