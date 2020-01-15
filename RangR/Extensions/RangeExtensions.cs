namespace RangR.Extensions
{
    public static class RangeExtensions
    {
        public static bool Intersects<T>(this RangeBase<T> range, T v1, T v2)
        {
            return range.Intersects(new RangeBase<T>(v1, v2));
        }
    }
}