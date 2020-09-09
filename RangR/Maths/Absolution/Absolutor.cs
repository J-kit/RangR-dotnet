using System;

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

    public static class Absolutor
    {
        static Absolutor()
        {
            AutoAbsolutor.EnsureInitialized();
            Absolutor<TimeSpan>.Default = new TimespanAbsolutor();
        }

        public static void EnsureInitialized()
        {
            // Just trigger the static initialization
        }
    }

    public class TimespanAbsolutor : Absolutor<TimeSpan>
    {

        public override TimeSpan Abs(TimeSpan v1)
        {
            var ticks = v1.Ticks;
            if (ticks < 0)
            {
                return new TimeSpan(ticks * -1);
            }
            return v1;
        }
    }
}