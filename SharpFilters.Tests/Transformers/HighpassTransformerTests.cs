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
    public class HighpassTransformerTests
    {
        [Theory]
        [AutoMoqData]
        internal void Transform_CorrectlyTransformsTheZCoefficient_Test(
            [Frozen] Mock<IPolesCoefficientsFactory> polesCoefficientsFactory, Mock<IAnalog> analog,
            IPolesCoefficients polesCoefficients, double cutoff, HighpassTransformer highpassTransformer)
        {
            var degree = polesCoefficients.P.Count - polesCoefficients.Z.Count;

            var z = polesCoefficients.Z.RhsDivide(cutoff).ToList();

            var expected = new List<Complex>(z);

            for (var i = 0; i < degree; i++)
            {
                expected.Add(0.0d);
            }

            analog.SetupGet(mock => mock.Coefficients).Returns(polesCoefficients);

            highpassTransformer.Transform(analog.Object, cutoff);

            polesCoefficientsFactory.Verify(
                mock => mock.Build(It.IsAny<double>(), It.IsAny<IReadOnlyList<Complex>>(), expected), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        internal void Transform_CorrectlyTransformsThePCoefficient_Test(
            [Frozen] Mock<IPolesCoefficientsFactory> polesCoefficientsFactory, Mock<IAnalog> analog,
            IPolesCoefficients polesCoefficients, double cutoff, HighpassTransformer highpassTransformer)
        {
            var expected = polesCoefficients.P.RhsDivide(cutoff).ToList();

            analog.SetupGet(mock => mock.Coefficients).Returns(polesCoefficients);

            highpassTransformer.Transform(analog.Object, cutoff);

            polesCoefficientsFactory.Verify(
                mock => mock.Build(It.IsAny<double>(), expected, It.IsAny<IReadOnlyList<Complex>>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        internal void Transform_CorrectlTransformsTheKCoefficient_Test(
            [Frozen] Mock<IPolesCoefficientsFactory> polesCoefficientsFactory, Mock<IAnalog> analog,
            IPolesCoefficients polesCoefficients, double cutoff, HighpassTransformer highpassTransformer)
        {
            var expected = polesCoefficients.K *
                           (polesCoefficients.Z.Negative().Product() / polesCoefficients.P.Negative().Product());

            analog.SetupGet(mock => mock.Coefficients).Returns(polesCoefficients);

            highpassTransformer.Transform(analog.Object, cutoff);

            polesCoefficientsFactory.Verify(
                mock =>
                        mock.Build(expected.Real, It.IsAny<IReadOnlyList<Complex>>(), It.IsAny<IReadOnlyList<Complex>>()),
                Times.Once);
        }

        [Theory]
        [AutoMoqData]
        internal void Transform_CorrectlyReturnsTheResultFromThePolesFactory_Test(
            [Frozen] Mock<IPolesCoefficientsFactory> polesCoefficientsFactory, IAnalog analog,
            double cutoff, IPolesCoefficients expected, HighpassTransformer highpassTransformer)
        {
            polesCoefficientsFactory.Setup(
                mock =>
                    mock.Build(It.IsAny<double>(), It.IsAny<IReadOnlyList<Complex>>(),
                        It.IsAny<IReadOnlyList<Complex>>())).Returns(expected);

            var actual = highpassTransformer.Transform(analog, cutoff);

            Assert.Equal(expected, actual);
        }
    }
}