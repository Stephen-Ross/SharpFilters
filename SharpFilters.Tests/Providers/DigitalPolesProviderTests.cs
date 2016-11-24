// Copyright © Stephen Ross 2016

using System;
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
    public class DigitalPolesProviderTests
    {
        [Theory]
        [AutoMoqData]
        internal void GetDigitalPoles_CorrectlyCallsTheTransformer_Test(
            [Frozen] Mock<ITransformer> transformer, IAnalog analog, double cutoff,
            double sampleRate, DigitalPolesProvider digitalPolesProvider)
        {
            var expectedCutoff = sampleRate * 2.0d * Math.Tan(Math.PI * cutoff / sampleRate);

            digitalPolesProvider.GetDigitalPoles(analog, cutoff, sampleRate);

            transformer.Verify(mock => mock.Transform(analog, expectedCutoff), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        internal void GetDigitalPoles_CorrectlyCallsTheDigitalTransformer_Test(
            [Frozen] Mock<ITransformer> transformer, [Frozen] Mock<IDigitalTransformer> digitalTransformer,
            IPolesCoefficients polesCoefficients, IAnalog analog, double cutoff, double sampleRate,
            DigitalPolesProvider digitalPolesProvider)
        {
            transformer.Setup(mock => mock.Transform(analog, It.IsAny<double>())).Returns(polesCoefficients);

            digitalPolesProvider.GetDigitalPoles(analog, cutoff, sampleRate);

            digitalTransformer.Verify(mock => mock.Transform(polesCoefficients, sampleRate * 2.0d), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        internal void GetDigitalPoles_ReturnsTheResultsOfTheDigitalTransformer_Test(
            [Frozen] Mock<ITransformer> transformer, [Frozen] Mock<IDigitalTransformer> digitalTransformer,
            IPolesCoefficients transformedCoefficients, IPolesCoefficients expectedCoefficients,
            IAnalog analog, double cutoff, double sampleRate, DigitalPolesProvider digitalPolesProvider)
        {
            transformer.Setup(mock => mock.Transform(analog, It.IsAny<double>())).Returns(transformedCoefficients);
            digitalTransformer.Setup(mock => mock.Transform(transformedCoefficients, It.IsAny<double>()))
                .Returns(expectedCoefficients);

            Assert.Equal(expectedCoefficients, digitalPolesProvider.GetDigitalPoles(analog, cutoff, sampleRate));
        }
    }
}