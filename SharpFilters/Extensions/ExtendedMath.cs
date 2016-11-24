// Copyright © Stephen Ross 2016

using System;

namespace SharpFilters.Extensions
{
    internal static class ExtendedMath
    {
        public static double ArcSinh(double value)
        {
            return Math.Log(value + Math.Sqrt(Math.Pow(value, 2) + 1));
        }
    }
}