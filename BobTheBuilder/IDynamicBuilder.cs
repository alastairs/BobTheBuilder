namespace BobTheBuilder
{
    public interface IDynamicBuilder<T> where T : class
    {
        T Build();
    }
}