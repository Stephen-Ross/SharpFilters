using SharpFilters.Models;

namespace SharpFilters
{
    public interface IFilterDesign
    {
        IPolynomialCoefficients PolynomialCoefficients { get; }
    }
}