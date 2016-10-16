// Stephen Ross 2016.

using System.Collections.Generic;
using System.Numerics;

namespace SharpFilters.Models
{
    internal struct PolesCoefficeints : IPolesCoefficients
    {
        private readonly double k;

        private readonly IReadOnlyList<Complex> p;

        private readonly IReadOnlyList<Complex> z;

        public PolesCoefficeints(double k, IReadOnlyList<Complex> p, IReadOnlyList<Complex> z)
        {
            this.k = k;
            this.p = p;
            this.z = z;
        }

        public double K
        {
            get { return this.k; }
        }

        public IReadOnlyList<Complex> P
        {
            get { return this.p; }
        }

        public IReadOnlyList<Complex> Z
        {
            get { return this.z; }
        }
    }
}