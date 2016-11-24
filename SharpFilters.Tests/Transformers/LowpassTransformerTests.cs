// Copyright © Stephen Ross 2016

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using SharpFilters.Analogs;
using SharpFilters.Extensions;
using SharpFilters.Factories.Models;
using SharpFilters.Models;
using SharpFilters.Tests.TestsCommon;
using SharpFilters.Transformers;
using Xunit;

namespace SharpFilters.Tests.Transformers
{
    public class LowpassTransformerTests
    {
        [Theory]
        [AutoMoqData]
        internal void Transform_CorrectlyTransformsTheZCoefficient_Test(
            [Frozen] Mock<IPolesCoefficientsFactory> polesCoefficientsFactory, Mock<IAnalog> analog,
            IPolesCoefficients polesCoefficients, double cutoff, LowPassTransformer lowPassTransformer)
        {
            var expected = polesCoefficients.Z.Multiply(cutoff).ToList();

            analog.SetupGet(mock => mock.Coefficients).Returns(polesCoefficients);

            lowPassTransformer.Transform(analog.Object, cutoff);

            polesCoefficientsFactory.Verify(
                mock => mock.Build(It.IsAny<double>(), It.IsAny<IReadOnlyList<Complex>>(), expected), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        internal void Transform_CorrectlyTransformsThePCoefficient_Test(
            [Frozen] Mock<IPolesCoefficientsFactory> polesCoefficientsFactory, Mock<IAnalog> analog,
            IPolesCoefficients polesCoefficients, double cutoff, LowPassTransformer lowPassTransformer)
        {
            var expected = polesCoefficients.P.Multiply(cutoff).ToList();

            analog.SetupGet(mock => mock.Coefficients).Returns(polesCoefficients);

            lowPassTransformer.Transform(analog.Object, cutoff);

            polesCoefficientsFactory.Verify(
                mock => mock.Build(It.IsAny<double>(), expected, It.IsAny<IReadOnlyList<Complex>>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        internal void Transform_CorrectlyTransformsTheKCoefficient_Test(
            [Frozen] Mock<IPolesCoefficientsFactory> polesCoefficientsFactory, Mock<IAnalog> analog,
            IPolesCoefficients polesCoefficients, double cutoff, LowPassTransformer lowPassTransformer)
        {
            var expected = polesCoefficients.K * Math.Pow(cutoff, polesCoefficients.P.Count - polesCoefficients.Z.Count);

            analog.SetupGet(mock => mock.Coefficients).Returns(polesCoefficients);

            lowPassTransformer.Transform(analog.Object, cutoff);

            polesCoefficientsFactory.Verify(
                mock => mock.Build(expected, It.IsAny<IReadOnlyList<Complex>>(), It.IsAny<IReadOnlyList<Complex>>()),
                Times.Once);
        }

        [Theory]
        [AutoMoqData]
        internal void Transform_ReturnsTheResultOfThePolesFactory_Test(
            [Frozen] Mock<IPolesCoefficientsFactory> polesCoefficientsFactory, IAnalog analog,
            double cutoff, IPolesCoefficients expected, LowPassTransformer lowPassTransformer)
        {
            polesCoefficientsFactory.Setup(
                mock =>
                    mock.Build(It.IsAny<double>(), It.IsAny<IReadOnlyList<Complex>>(),
                        It.IsAny<IReadOnlyList<Complex>>())).Returns(expected);

            var actual = lowPassTransformer.Transform(analog, cutoff);

            Assert.Equal(expected, actual);
        }
    }
}