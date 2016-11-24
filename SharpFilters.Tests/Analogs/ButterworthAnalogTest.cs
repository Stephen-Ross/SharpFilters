// Copyright © Stephen Ross 2016

using System;
using System.Collections.Generic;
using System.Numerics;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using SharpFilters.Analogs;
using SharpFilters.Factories.Models;
using SharpFilters.Models;
using SharpFilters.Tests.TestsCommon;
using Xunit;

namespace SharpFilters.Tests.Analogs
{
    public class ButterworthAnalogTest
    {
        [Theory]
        [InlineAutoMoqData(0)]
        internal void CalculateAnalog_ThrowsExceptionIfOrderIsLessThanOne_Test(
            int order, ButterworthAnalog butterworthAnalog)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => butterworthAnalog.CalculateAnalog(order));
        }

        [Theory]
        [InlineAutoMoqData(1)]
        internal void CalculateAnalog_CorrectlyCalculatesTheKCoefficient_Test(
            int order, [Frozen] Mock<IPolesCoefficientsFactory> polesCoefficientsFactory,
            ButterworthAnalog butterworthAnalog)
        {
            butterworthAnalog.CalculateAnalog(order);

            polesCoefficientsFactory.Verify(
                mock => mock.Build(1.0d, It.IsAny<IReadOnlyList<Complex>>(), It.IsAny<IReadOnlyList<Complex>>()),
                Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(1)]
        [InlineAutoMoqData(2)]
        [InlineAutoMoqData(3)]
        [InlineAutoMoqData(4)]
        [InlineAutoMoqData(5)]
        internal void CalculateAnalog_CorrectlyCalculatesThePCoefficient_Test(
            int order, [Frozen] Mock<IPolesCoefficientsFactory> polesCoefficientsFactory,
            ButterworthAnalog butterworthAnalog)
        {
            var expected = new List<Complex>();

            var start = -order + 1;

            for (var i = 0; i < order; i++, start += 2)
            {
                expected.Add(-Complex.Exp(new Complex(0.0d, 1.0d) * Math.PI * start / (2 * order)));
            }

            butterworthAnalog.CalculateAnalog(order);

            polesCoefficientsFactory.Verify(
                mock => mock.Build(It.IsAny<double>(), expected, It.IsAny<IReadOnlyList<Complex>>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(1)]
        internal void CalculateAnlog_CorrectlyCalculatesTheZCoefficient_TesT(
            int order, [Frozen] Mock<IPolesCoefficientsFactory> polesCoefficientFactory,
            ButterworthAnalog butterworthAnalog)
        {
            butterworthAnalog.CalculateAnalog(order);

            polesCoefficientFactory.Verify(
                mock => mock.Build(It.IsAny<double>(), It.IsAny<IReadOnlyList<Complex>>(), new List<Complex>()),
                Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(1)]
        internal void CalculateAnalog_SetsTheCoefficientsPropertyToTheBuiltCoefficients_Test(
            int order, [Frozen] Mock<IPolesCoefficientsFactory> polesCoefficientsFactory,
            IPolesCoefficients polesCoefficients, ButterworthAnalog butterworthAnalog)
        {
            polesCoefficientsFactory.Setup(
                mock =>
                    mock.Build(It.IsAny<double>(), It.IsAny<IReadOnlyList<Complex>>(),
                        It.IsAny<IReadOnlyList<Complex>>())).Returns(polesCoefficients);

            butterworthAnalog.CalculateAnalog(order);

            Assert.Equal(polesCoefficients, butterworthAnalog.Coefficients);
        }
    }
}