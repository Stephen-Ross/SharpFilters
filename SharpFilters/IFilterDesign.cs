// Copyright © Stephen Ross 2016

using SharpFilters.Models;

namespace SharpFilters
{
    /// <summary>
    /// Represents the shared members required for a filter design.
    /// </summary>
    public interface IFilterDesign
    {
        /// <summary>
        /// Gets the <see cref="IPolynomialCoefficients"/> generated during the composition of the filter design.
        /// </summary>
        IPolynomialCoefficients PolynomialCoefficients { get; }
    }
}