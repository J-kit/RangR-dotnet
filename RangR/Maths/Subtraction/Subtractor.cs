using System;
using System.Linq;

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
            var defaultInstance = Default;
            if (defaultInstance != null)
            {
                return;
            }

            var type = typeof(T);
            var method = type
                .GetMethods()
                .FirstOrDefault
                (
                    x => x.IsSpecialName &&
                         x.Name == "op_Subtraction" &&
                         x.GetParameters().Select(m => m.ParameterType).SequenceEqual(new[] { type, type }) &&
                         x.ReturnType == typeof(T)
                );
            //method.ReturnType == typeof(T)

            if (method == null)
            {
                return;
            }

            var func = (Func<T, T, T>)method.CreateDelegate(typeof(Func<T, T, T>));
            Default = new FuncSubtractor<T>(func);
        }

        public abstract T Subtract(T v1, T v2);
    }

    internal class FuncSubtractor<T> : Subtractor<T>
    {
        private readonly Func<T, T, T> func;

        public FuncSubtractor(Func<T, T, T> func)
        {
            this.func = func;
        }

        public override T Subtract(T v1, T v2)
        {
            return func(v1, v2);
        }
    }

    public static class Subtractor
    {
        static Subtractor()
        {
            AutoSubtractor.EnsureInitialized();
            Subtractor<DateTime>.Default = new DatetimeSubtractor();
        }

        public static void EnsureInitialized()
        {
            // Just trigger the static initialization
        }
    }
}