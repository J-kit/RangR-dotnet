namespace Ranging.Abstractions
{
    public interface IHasEnd<T>
    {
        T End { get; set; }
    }
}