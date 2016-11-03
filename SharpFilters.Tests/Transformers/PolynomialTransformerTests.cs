using System.Collections.Generic;
using System.Linq;
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
    public class PolynomialTransformerTests
    {
        [Theory]
        [AutoMoqData]
        internal void Transform_CorrectlyCalculatesTheBCoefficient_Test(
            [Frozen] Mock<IPolynomialCoefficientsFactory> polynomialCoefficientsFactory,
            IPolesCoefficients polesCoefficients, PolynomialTransformer polynomialTransformer)
        {
            var expected =
                polesCoefficients.Z.PolynomialCoefficients().Multiply(polesCoefficients.K).Select(x => x.Real).ToList();

            polynomialTransformer.Transform(polesCoefficients);

            polynomialCoefficientsFactory.Verify(mock => mock.Build(It.IsAny<IReadOnlyList<double>>(), expected),
                Times.Once);
        }

        [Theory]
        [AutoMoqData]
        internal void Transform_CorrectlyCalculatesTheACoefficient_Test(
            [Frozen] Mock<IPolynomialCoefficientsFactory> polynomialCoefficientsFactory,
            IPolesCoefficients polesCoefficients, PolynomialTransformer polynomialTransformer)
        {
            var expected = polesCoefficients.P.PolynomialCoefficients().Select(x => x.Real).ToList();

            polynomialTransformer.Transform(polesCoefficients);

            polynomialCoefficientsFactory.Verify(mock => mock.Build(expected, It.IsAny<IReadOnlyList<double>>()),
                Times.Once);
        }

        [Theory]
        [AutoMoqData]
        internal void Transform_CorrectlyReturnsTheResultOfThePolynomialFactory_Test(
            [Frozen] Mock<IPolynomialCoefficientsFactory> polynomialCoefficientsFactory,
            IPolesCoefficients polesCoefficients, IPolynomialCoefficients expected,
            PolynomialTransformer polynomialTransformer)
        {
            polynomialCoefficientsFactory.Setup(
                    mock => mock.Build(It.IsAny<IReadOnlyList<double>>(), It.IsAny<IReadOnlyList<double>>()))
                .Returns(expected);

            var actual = polynomialTransformer.Transform(polesCoefficients);

            Assert.Equal(expected, actual);
        }
    }
}