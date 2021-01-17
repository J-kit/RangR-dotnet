using RangR.Maths.Subtraction;
using RangR.Utils;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RangR.ConsoleTests
{
    public static class Ext
    {
        //public static void AddMany(ICollection<string> collection, string[] source)
        //{
        //    for (int i = 0; i < source.Length; i++)
        //    {
        //        collection.Add(source[i]);
        //    }
        //}

        //public static void AddMany(ICollection<int> collection, int[] source)
        //{
        //    for (int i = 0; i < source.Length; i++)
        //    {
        //        collection.Add(source[i]);
        //    }
        //}

        //public static void AddMany(ICollection<KeyValuePair<string, string>> collection, KeyValuePair<string, string>[] source)
        //{
        //    for (int i = 0; i < source.Length; i++)
        //    {
        //        collection.Add(source[i]);
        //    }
        //}

        public static TCollection AddMany<TType, TCollection>(this TCollection collection, TType[] source)
            where TCollection : ICollection<TType>
        {
            for (int i = 0; i < source.Length; i++)
            {
                collection.Add(source[i]);
            }

            return collection;
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var source = new string[] { "Hallo", "welt" };

            List<string> list = new List<string>();
            List<string> newList = list.AddMany(source);

            var dict = new HashSet<string>();

            Ext.AddMany(dict, source);

            Dictionary<string, string> dst = new Dictionary<string, string>()
                .AddMany(new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("a","a"),
                    new KeyValuePair<string, string>("aa","aa"),
                });

            Console.WriteLine("Hello World!");

            var ranges = new List<DateTimeRange>
            {
                new DateTimeRange(new DateTime(2020, 01, 01), new DateTime(2020, 01, 15)),
                new DateTimeRange(new DateTime(2020, 01, 10), new DateTime(2020, 02, 01)),
                new DateTimeRange(new DateTime(2020, 02, 03), new DateTime(2020, 02, 20)),
                new DateTimeRange(new DateTime(2020, 03, 01), new DateTime(2020, 04, 01)),
            };

            var intersecting = ranges.MergeIntersecting(TimeSpan.FromDays(2));

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

        private static void TestFromEnumerable()
        {
            var dates = new List<DateTime>
            {
                new DateTime(2020, 09, 10, 10, 00, 0),
                new DateTime(2020, 09, 10, 10, 20, 0),
                new DateTime(2020, 09, 10, 10, 40, 0),
                new DateTime(2020, 09, 10, 10, 50, 0),
                new DateTime(2020, 09, 10, 11, 00, 0),
                new DateTime(2020, 09, 10, 11, 30, 0),
                new DateTime(2020, 09, 10, 12, 00, 0),

                new DateTime(2020, 09, 10, 13, 00, 0),
                new DateTime(2020, 09, 10, 13, 30, 0),
                new DateTime(2020, 09, 10, 14, 00, 0),
            };
            //var rs = DateTimeRange.FromEnumerable(dates, DateTime.MinValue.AddHours(0.5)).ToList();
            //TimeSpanRange.FromEnumerable()

            var ranges = RangeBase<long>.FromEnumerable
            (
                dates.Select(x => x.ToFileTimeUtc()),
                (long)TimeSpan.FromHours(0.5).Ticks
            ).Select(x => new DateTimeRange(DateTime.FromFileTimeUtc(x.Start), DateTime.FromFileTimeUtc(x.End))).ToList();
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