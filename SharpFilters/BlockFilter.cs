using System.Collections.Generic;

namespace SharpFilters
{
    public sealed class BlockFilter : IBlockFilter
    {
        private readonly IFilterDesign filterDesign;

        public BlockFilter(IFilterDesign filterDesign)
        {
            this.filterDesign = filterDesign;
        }

        public IReadOnlyList<double> Filter(IReadOnlyList<double> data)
        {
            var filteredData = new List<double>(data.Count);

            var xv = new double[filterDesign.PolynomialCoefficients.B.Count];
            var yv = new double[filterDesign.PolynomialCoefficients.A.Count];

            for (var i = 0; i < data.Count; i++)
            {
                for (var j = 0; j < xv.Length - 1; j++)
                {
                    xv[j] = xv[j + 1];
                    yv[j] = yv[j + 1];
                }

                xv[xv.Length - 1] = data[i];

                var filteredValue = 0.0d;
                var index = xv.Length - 1;
                for (var j = 0; j < xv.Length; j++, index--)
                {
                    filteredValue += this.filterDesign.PolynomialCoefficients.B[j] * xv[index];
                }

                index = yv.Length - 2;
                for (var j = 0; j < yv.Length; j++, index--)
                {
                    filteredValue -= this.filterDesign.PolynomialCoefficients.A[j] * yv[index];
                }

                yv[this.filterDesign.PolynomialCoefficients.A.Count - 1] = filteredValue;
                filteredData.Add(filteredValue);
            }

            return filteredData;
        }
    }
}