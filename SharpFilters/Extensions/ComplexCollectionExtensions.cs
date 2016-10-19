using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace SharpFilters.Extensions
{
    internal static class ComplexCollectionExtensions
    {
        public static IEnumerable<Complex> Add(this IEnumerable<Complex> complexs, double value)
        {
            return complexs.Select(x => x + value);
        }

        public static IEnumerable<Complex> Divide(this IEnumerable<Complex> lhsComplexs,
            IEnumerable<Complex> rhsComplexes)
        {
            using (var lhsEnumerator = lhsComplexs.GetEnumerator())
            using (var rhsEnumerator = rhsComplexes.GetEnumerator())
            {
                while (lhsEnumerator.MoveNext() && rhsEnumerator.MoveNext())
                {
                    yield return lhsEnumerator.Current / rhsEnumerator.Current;
                }
            }
        }

        public static IEnumerable<Complex> Multiply(this IEnumerable<Complex> complexs, double value)
        {
            return complexs.Select(x => x * value);
        }

        public static IEnumerable<Complex> Negative(this IEnumerable<Complex> complexs)
        {
            return complexs.Select(x => -x);
        }

        public static Complex Product(this IEnumerable<Complex> complexs)
        {
            var result = new Complex(1.0d, 1.0d);

            return complexs.Aggregate(result, (current, complex) => current * complex);
        }

        public static IEnumerable<Complex> RhsDivide(this IEnumerable<Complex> complexs, double value)
        {
            return complexs.Select(x => value / x);
        }
    }
}