﻿// Copyright © Stephen Ross 2016

using SharpFilters.Analogs;
using SharpFilters.Models;

namespace SharpFilters.Providers
{
    internal interface IDigitalPolesProvider
    {
        IPolesCoefficients GetDigitalPoles(IAnalog analog, double cutoff, double sampleRate);
    }
}