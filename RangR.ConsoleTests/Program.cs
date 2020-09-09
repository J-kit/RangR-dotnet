using RangR.Maths.Subtraction;
using RangR.Utils;

using System;
using System.Diagnostics;
using System.Linq;

namespace RangR.ConsoleTests
{
    //public ref struct DoSomething
    //{
    //    private TypedReference _tr;

    //    public DoSomething(TypedReference tr)
    //    {
    //        _tr = tr;
    //    }
    //}



    internal class Program
    {
        private static void RefX()
        {
            float i = 21.001f;
            TypedReference tr = __makeref(i);
            Type t = __reftype(tr);
            Console.WriteLine(t.ToString());
            var rv = __refvalue(tr, float);
            Console.WriteLine(rv);

            DisplayNumbersOnConsole(__arglist(1, 2, 3, 5, 6));
        }

        public static void DisplayNumbersOnConsole(__arglist)
        {
            ArgIterator ai = new ArgIterator(__arglist);
            while (ai.GetRemainingCount() > 0)
            {
                TypedReference tr = ai.GetNextArg();
                Console.WriteLine(TypedReference.ToObject(tr));
            }
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Subtractor<DateTime>.Default = new DatetimeSubtractor();

            RefX();

            var rng = new TimeSpanRange(TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(2));

            var enumerated = rng.Enumerate(TimeSpan.FromSeconds(2)).ToList();

            var dtrange = new DateTimeRange(DateTime.Now, TimeSpan.FromDays(20));

            var en = dtrange.Enumerate(TimespanType.Day, 1).ToList();

            //var nit2Range = new[]
            //{
            //    1, 2, 3, 4, 6, 7
            //};

            //var nRange = IntegerRange.FromEnumerable(nit2Range).ToList();

            //var intRanges = new[]
            //{
            //    new IntegerRange(1,2),
            //    new IntegerRange(3,4),
            //    new IntegerRange(6,7),
            //};

            //var intIntersection = intRanges.MergeIntersecting(1);

            //var method = typeof(TimeSpan).GetMethods()
            //    .Where(x => x.IsSpecialName && x.Name == "op_Subtraction" && x.GetParameters().Select(m => m.ParameterType).SequenceEqual(new[] { typeof(TimeSpan), typeof(TimeSpan) }))
            //    .ToList();

            //PerformanceTests();

            var timeRanges = new[]
            {
                new TimeSpanRange(TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(2)),
                new TimeSpanRange(TimeSpan.FromMinutes(3), TimeSpan.FromMinutes(4)),
                new TimeSpanRange(TimeSpan.FromMinutes(6), TimeSpan.FromMinutes(7)),
            };

            var timeIntersection = timeRanges.MergeIntersecting(TimeSpan.FromMinutes(1));
        }

        private static void PerformanceTests()
        {
            var genericSubtractor = Subtractor<TimeSpan>.Default;
            var manualSubtractor = new TimeSpanSubtractor();

            const int tests = 1_000_000_000;

            var result = TimeSpan.FromMinutes(5);

            var subtractionValue = TimeSpan.FromSeconds(1);

            var sw1 = Stopwatch.StartNew();

            for (int i = 0; i < tests; i++)
            {
                result = genericSubtractor.Subtract(result, subtractionValue);
            }
            sw1.Stop();

            result = TimeSpan.FromMinutes(5);
            var sw2 = Stopwatch.StartNew();
            for (int i = 0; i < tests; i++)
            {
                result = manualSubtractor.Subtract(result, subtractionValue);
            }
            sw2.Stop();

            Console.WriteLine($"G {sw1.Elapsed.TotalMilliseconds} ms / M {sw2.Elapsed.TotalMilliseconds}");
            Console.ReadLine();
        }
    }
}