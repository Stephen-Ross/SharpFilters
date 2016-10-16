namespace SharpFilters.Analogs
{
    internal interface IButterworthAnalog : IAnalog
    {
        void CalculateAnalog(int order);
    }
}