﻿// Copyright © Stephen Ross 2016

using SharpFilters.Analogs;
using SharpFilters.Models;

namespace SharpFilters.Transformers
{
    internal interface ITransformer
    {
        IPolesCoefficients Transform(IAnalog analog, double cutoff);
    }
}