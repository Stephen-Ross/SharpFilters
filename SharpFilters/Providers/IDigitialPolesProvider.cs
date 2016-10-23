using SharpFilters.Analogs;
using SharpFilters.Models;

namespace SharpFilters.Providers
{
    internal interface IDigitialPolesProvider
    {
        IPolesCoefficients GetDigitalPoles(IAnalog analog, double cutoff, double sampleRate);
    }
}