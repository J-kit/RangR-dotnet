namespace RangR.Maths.Absolution
{
    public interface IAbsolutor<T>
    {
        T Abs(T v1);
    }

    public abstract class Absolutor<T> : IAbsolutor<T>
    {
        public static Absolutor<T> Default { get; set; }

        static Absolutor()
        {
            Absolutor.EnsureInitialized();
        }

        public abstract T Abs(T v1);
    }
}