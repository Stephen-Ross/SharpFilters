// Copyright © Stephen Ross 2016

using System.Collections.Generic;
using System.Numerics;
using Ploeh.AutoFixture.Xunit2;
using SharpFilters.Models;
using SharpFilters.Tests.TestsCommon;
using Xunit;

namespace SharpFilters.Tests.Models
{
    public class PolesCoefficientsTests
    {
        [Theory]
        [AutoMoqData]
        internal void K_IsCorrectlySet_Test(
            [Frozen(Matching.ParameterName)] double k, PolesCoefficients polesCoefficients)
        {
            Assert.Equal(k, polesCoefficients.K);
        }

        [Theory]
        [AutoMoqData]
        internal void P_IsCorrectlySet_Test(
            [Frozen(Matching.ParameterName)] IReadOnlyList<Complex> p,
            PolesCoefficients polesCoefficients)
        {
            Assert.Equal(p, polesCoefficients.P);
        }

        [Theory]
        [AutoMoqData]
        internal void Z_IsCorrectlySet_Test(
            [Frozen(Matching.ParameterName)] IReadOnlyList<Complex> z,
            PolesCoefficients polesCoefficients)
        {
            Assert.Equal(z, polesCoefficients.Z);
        }
    }
}