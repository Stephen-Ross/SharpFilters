// Copyright © Stephen Ross 2016

using System.Threading;

namespace SharpFilters
{
    internal class ContinuousFilter : IContinuousFilter
    {
        private static SpinLock spinLock = new SpinLock();

        private readonly IFilterDesign filterDesign;

        private double[] xv;

        private double[] yv;

        public ContinuousFilter(IFilterDesign filterDesign)
        {
            this.filterDesign = filterDesign;
            this.xv = new double[filterDesign.PolynomialCoefficients.B.Count];
            this.yv = new double[filterDesign.PolynomialCoefficients.A.Count];
        }

        public double Filter(double data)
        {
            var lockTaken = false;

            try
            {
                spinLock.Enter(ref lockTaken);

                for (var i = 0; i < this.xv.Length - 1; i++)
                {
                    this.xv[i] = this.xv[i + 1];
                    this.yv[i] = this.yv[i + 1];
                }

                this.xv[xv.Length - 1] = data;

                var filteredData = 0.0d;
                var index = this.xv.Length - 1;
                for (var i = 0; i < this.xv.Length; i++, index--)
                {
                    filteredData += this.filterDesign.PolynomialCoefficients.B[i] * this.xv[index];
                }

                index = this.yv.Length - 2;
                for (var i = 0; i < this.yv.Length; i++, index--)
                {
                    filteredData -= this.filterDesign.PolynomialCoefficients.A[i] * this.yv[index];
                }

                this.yv[this.filterDesign.PolynomialCoefficients.A.Count - 1] = filteredData;

                return filteredData;
            }
            finally
            {
                if (lockTaken)
                {
                    spinLock.Exit();
                }
            }
        }

        public void Reset()
        {
            var lockTaken = false;

            try
            {
                spinLock.Enter(ref lockTaken);

                this.xv = new double[filterDesign.PolynomialCoefficients.B.Count];
                this.yv = new double[filterDesign.PolynomialCoefficients.A.Count];
            }
            finally
            {
                if (lockTaken)
                {
                    spinLock.Exit();
                }
            }
        }
    }
}