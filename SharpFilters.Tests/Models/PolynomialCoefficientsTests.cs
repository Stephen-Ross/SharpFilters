using System.Collections.Generic;
using Ploeh.AutoFixture.Xunit2;
using SharpFilters.Models;
using SharpFilters.Tests.TestsCommon;
using Xunit;

namespace SharpFilters.Tests.Models
{
    public class PolynomialCoefficientsTests
    {
        [Theory]
        [AutoMoqData]
        internal void A_IsCorrectlySet_Test(
            [Frozen(Matching.ParameterName)] IReadOnlyList<double> a,
            PolynomialCoefficients polynomialCoefficients)
        {
            Assert.Equal(a, polynomialCoefficients.A);
        }

        [Theory]
        [AutoMoqData]
        internal void B_IsCorrectlySet_Test(
            [Frozen(Matching.ParameterName)] IReadOnlyList<double> b,
            PolynomialCoefficients polynomialCoefficients)
        {
            Assert.Equal(b, polynomialCoefficients.B);
        }
    }
}