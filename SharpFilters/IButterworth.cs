namespace SharpFilters
{
    public interface IButterworth : IFilter
    {
        void Compose(int order, double cutoff);
    }
}