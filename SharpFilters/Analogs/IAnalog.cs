using SharpFilters.Models;

namespace SharpFilters.Analogs
{
    internal interface IAnalog
    {
        IPolesCoefficients Coefficients { get; }
    }
}