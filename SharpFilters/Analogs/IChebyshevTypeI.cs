using SharpFilters.Models;

namespace SharpFilters.Analogs
{
    internal interface IChebyshevTypeI : IAnalog
    {
        IPolesCoefficients CalculateAnalog(int order, double ripple);
    }
}