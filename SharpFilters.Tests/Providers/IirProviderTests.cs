// Copyright © Stephen Ross 2016

using Moq;
using Ploeh.AutoFixture.Xunit2;
using SharpFilters.Analogs;
using SharpFilters.Models;
using SharpFilters.Providers;
using SharpFilters.Tests.TestsCommon;
using SharpFilters.Transformers;
using Xunit;

namespace SharpFilters.Tests.Providers
{
    public class IirProviderTests
    {
        [Theory]
        [AutoMoqData]
        internal void GetIirCoefficients_CorrectlyCallsTheDigitalPolesProvider_Test(
            [Frozen] Mock<IDigitalPolesProvider> digitialPolesProvider, IAnalog analog,
            double cutoff, IirProvider iirProvider)
        {
            iirProvider.GetIirCoefficients(analog, cutoff);

            digitialPolesProvider.Verify(mock => mock.GetDigitalPoles(analog, cutoff, 2.0d), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        internal void GetIirCoefficients_CorrectlyCallsThePolynomialTransformer_Test(
            [Frozen] Mock<IDigitalPolesProvider> digitialPolesProvider,
            [Frozen] Mock<IPolynomialTransformer> polynomialTransformer, IPolesCoefficients polesCoefficients,
            IAnalog analog, double cutoff, IirProvider iirProvider)
        {
            digitialPolesProvider.Setup(mock => mock.GetDigitalPoles(analog, cutoff, 2.0d)).Returns(polesCoefficients);

            iirProvider.GetIirCoefficients(analog, cutoff);

            polynomialTransformer.Verify(mock => mock.Transform(polesCoefficients), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        internal void GetIirCoefficients_CorrectlyReturnsTheValueOfThePolynomialTransformer_Test(
            [Frozen] Mock<IDigitalPolesProvider> digitialPolesProvider,
            [Frozen] Mock<IPolynomialTransformer> polynomialTransformer, IPolesCoefficients polesCoefficients,
            IPolynomialCoefficients polynomialCoefficients, IAnalog analog, double cutoff,
            IirProvider iirProvider)
        {
            digitialPolesProvider.Setup(mock => mock.GetDigitalPoles(analog, cutoff, 2.0d)).Returns(polesCoefficients);
            polynomialTransformer.Setup(mock => mock.Transform(polesCoefficients)).Returns(polynomialCoefficients);

            var actual = iirProvider.GetIirCoefficients(analog, cutoff);

            Assert.Equal(polynomialCoefficients, actual);
        }
    }
}