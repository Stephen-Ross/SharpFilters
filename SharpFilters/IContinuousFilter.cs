namespace SharpFilters
{
    public interface IContinuousFilter
    {
        double Filter(double data);

        void Reset();
    }
}