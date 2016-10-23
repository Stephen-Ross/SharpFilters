using System.Collections.Generic;

namespace SharpFilters.Models
{
    internal class PolynomialCoefficients : IPolynomialCoefficients
    {
        private readonly IReadOnlyList<double> a;

        private readonly IReadOnlyList<double> b;

        public PolynomialCoefficients(IReadOnlyList<double> a, IReadOnlyList<double> b)
        {
            this.a = a;
            this.b = b;
        }

        public IReadOnlyList<double> A
        {
            get { return a; }
        }

        public IReadOnlyList<double> B
        {
            get { return b; }
        }
    }
}