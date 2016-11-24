// Copyright © Stephen Ross 2016

using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SharpFilters.Extensions;
using SharpFilters.Tests.TestsCommon;
using Xunit;

namespace SharpFilters.Tests.Extensions
{
    public class DoubleExtensionsTests
    {
        [Theory]
        [AutoMoqData]
        internal void Subtract_SubtractsTheComplexFromTheValue_Test(
            IEnumerable<Complex> complexs, double value)
        {
            var expected = complexs.Select(x => value - x);

            Assert.Equal(expected, value.Subtract(complexs));
        }
    }
}