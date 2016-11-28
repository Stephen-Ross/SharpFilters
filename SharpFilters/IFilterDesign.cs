// Copyright © Stephen Ross 2016

using SharpFilters.Enums;
using SharpFilters.Models;

namespace SharpFilters
{
    /// <summary>
    /// Represents the shared members required for a filter design.
    /// </summary>
    public interface IFilterDesign
    {
        /// <summary>
        /// Gets the cutoff of the filter.
        /// </summary>
        double Cutoff { get; }

        /// <summary>
        /// Gets the filter type of the design.
        /// </summary>
        FilterType FilterType { get; }

        /// <summary>
        /// Gets the n-th order of the filter.
        /// </summary>
        int Order { get; }

        /// <summary>
        /// Gets the <see cref="IPolynomialCoefficients"/> generated during the composition of the filter design.
        /// </summary>
        IPolynomialCoefficients PolynomialCoefficients { get; }
    }
}