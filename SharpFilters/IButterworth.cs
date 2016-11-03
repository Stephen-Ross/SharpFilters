namespace SharpFilters
{
    public interface IButterworth : IFilterDesign
    {
        void Compose(int order, double cutoff);
    }
}