using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SharpFilters.Extensions;
using SharpFilters.Tests.TestsCommon;
using Xunit;

namespace SharpFilters.Tests.Extensions
{
    public class ComplexCollectionExtensionsTests
    {
        [Theory]
        [AutoMoqData]
        internal void Add_CorrectlyAddsTheDoubleToTheCollection_Test(
            double value, IEnumerable<Complex> complexs)
        {
            var expected = complexs.Select(x => x + value);

            Assert.Equal(expected, complexs.Add(value));
        }

        [Theory]
        [AutoMoqData]
        internal void Divide_CorrectlyDividesTheCollectionsMemberWise_Test(
            IEnumerable<Complex> lhsComplexes, IEnumerable<Complex> rhsComplexes)
        {
            var expected = new List<Complex>();

            using (var lhsEnumerator = lhsComplexes.GetEnumerator())
            using (var rhsEnumerator = rhsComplexes.GetEnumerator())
            {
                while (lhsEnumerator.MoveNext() && rhsEnumerator.MoveNext())
                {
                    expected.Add(lhsEnumerator.Current / rhsEnumerator.Current);
                }
            }

            Assert.Equal(expected, lhsComplexes.Divide(rhsComplexes));
        }

        [Theory]
        [AutoMoqData]
        internal void Multiply_CorrectlyMultiplesTheCollectionByTheDouble_Test(
            double value, IEnumerable<Complex> complexs)
        {
            var expected = complexs.Select(x => x * value);

            Assert.Equal(expected, complexs.Multiply(value));
        }

        [Theory]
        [AutoMoqData]
        internal void Negative_CorrectlyNegatesEachMember_Test(
            IEnumerable<Complex> complexs)
        {
            var expected = complexs.Select(x => -x);

            Assert.Equal(expected, complexs.Negative());
        }

        [Theory]
        [AutoMoqData]
        internal void PlynomialCoefficients_ConvertsTheValuesToPolynomialCoefficients_Test(
            IEnumerable<Complex> complexs)
        {
            var expected = new List<Complex>
            {
                1.0d
            };

            foreach (var complex in complexs)
            {
                var value = -complex;
                expected.Add(expected[expected.Count - 1] * value);
                for (var i = expected.Count - 2; i >= 1; i--)
                {
                    expected[i] = expected[i] + value * expected[i - 1];
                }
            }

            Assert.Equal(expected, complexs.PolynomialCoefficients());
        }

        [Theory]
        [AutoMoqData]
        internal void Product_CorrectlyCalculatesTheProductOfAllTheValues_Test(
            IEnumerable<Complex> complexs)
        {
            var result = new Complex(1.0d, 1.0d);

            var expected = complexs.Aggregate(result, (complex, complex1) => complex * complex1);

            Assert.Equal(expected, complexs.Product());
        }

        [Theory]
        [AutoMoqData]
        internal void RhsDivide_CorrectlyDividesByTheDoubleMemberwise_Test(
            double value, IEnumerable<Complex> complexs)
        {
            var expected = complexs.Select(x => value / x);

            Assert.Equal(expected, complexs.RhsDivide(value));
        }
    }
}