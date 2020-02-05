namespace RangR.Maths.Subtraction
{
    public interface ISubtractor<T>
    {
        T Subtract(T v1, T v2);
    }

    public abstract class Subtractor<T> : ISubtractor<T>
    {
        public static Subtractor<T> Default { get; set; }

        static Subtractor()
        {
            Subtractor.EnsureInitialized();
        }

        public abstract T Subtract(T v1, T v2);
    }
}