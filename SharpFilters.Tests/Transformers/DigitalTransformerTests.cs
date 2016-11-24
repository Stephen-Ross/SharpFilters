// Copyright © Stephen Ross 2016

using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using SharpFilters.Extensions;
using SharpFilters.Factories.Models;
using SharpFilters.Models;
using SharpFilters.Tests.TestsCommon;
using SharpFilters.Transformers;
using Xunit;

namespace SharpFilters.Tests.Transformers
{
    public class DigitalTransformerTests
    {
        [Theory]
        [AutoMoqData]
        internal void Transform_CorrectlyTransformsTheZCoefficient_Test(
            [Frozen] Mock<IPolesCoefficientsFactory> polesCoefficientFactory, IPolesCoefficients polesCoefficients,
            double sampleRate, DigitalTransformer digitalTransformer)
        {
            var degree = polesCoefficients.P.Count - polesCoefficients.Z.Count;

            var expectedZ = polesCoefficients.Z.Add(sampleRate).Divide(sampleRate.Subtract(polesCoefficients.Z));

            var expected = new List<Complex>(expectedZ);

            for (var i = 0; i < degree; i++)
            {
                expected.Add(-1.0d);
            }

            digitalTransformer.Transform(polesCoefficients, sampleRate);

            polesCoefficientFactory.Verify(
                mock => mock.Build(It.IsAny<double>(), It.IsAny<IReadOnlyList<Complex>>(), expected), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        internal void Transform_CorrectlyTransformsThePCoefficient_Test(
            [Frozen] Mock<IPolesCoefficientsFactory> polesCoefficientsFactory, IPolesCoefficients polesCoefficients,
            double sampleRate, DigitalTransformer digitalTransformer)
        {
            var expectedP =
                polesCoefficients.P.Add(sampleRate).Divide(sampleRate.Subtract(polesCoefficients.Z)).ToList();

            digitalTransformer.Transform(polesCoefficients, sampleRate);

            polesCoefficientsFactory.Verify(
                mock => mock.Build(It.IsAny<double>(), expectedP, It.IsAny<IReadOnlyList<Complex>>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        internal void Transform_CorrectlyTransformsTheKCoefficient_Test(
            [Frozen] Mock<IPolesCoefficientsFactory> polesCoefficientsFactory, IPolesCoefficients polesCoefficients,
            double sampleRate, DigitalTransformer digitalTransformer)
        {
            var expectedK = polesCoefficients.K *
                            (sampleRate.Subtract(polesCoefficients.Z).Product() /
                             sampleRate.Subtract(polesCoefficients.P).Product());

            digitalTransformer.Transform(polesCoefficients, sampleRate);

            polesCoefficientsFactory.Verify(
                mock =>
                    mock.Build(expectedK.Real, It.IsAny<IReadOnlyList<Complex>>(),
                        It.IsAny<IReadOnlyList<Complex>>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        internal void Transform_CorrectlyReturnsTheResultOfThePolesFactory_Test(
            [Frozen] Mock<IPolesCoefficientsFactory> polesCoefficientsFactory, IPolesCoefficients polesCoefficients,
            IPolesCoefficients expectedCoefficients, double sampleRate, DigitalTransformer digitalTransformer)
        {
            polesCoefficientsFactory.Setup(
                mock =>
                    mock.Build(It.IsAny<double>(), It.IsAny<IReadOnlyList<Complex>>(),
                        It.IsAny<IReadOnlyList<Complex>>())).Returns(expectedCoefficients);

            var actual = digitalTransformer.Transform(polesCoefficients, sampleRate);

            Assert.Equal(expectedCoefficients, actual);
        }
    }
}