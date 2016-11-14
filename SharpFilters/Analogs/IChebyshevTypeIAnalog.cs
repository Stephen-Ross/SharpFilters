using SharpFilters.Models;

namespace SharpFilters.Analogs
{
    internal interface IChebyshevTypeIAnalog : IAnalog
    {
        IPolesCoefficients CalculateAnalog(int order, double ripple);
    }
}