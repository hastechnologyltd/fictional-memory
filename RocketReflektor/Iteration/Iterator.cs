using System.Collections.Generic;

namespace RocketReflektor.Iteration
{
    public static class IterationExtension
    {
        public static IEnumerable<TOut> Items<TOut>(this IIterator<TOut> iterator)
        {
            var items = new List<TOut>();
            for (var i = 1; i < iterator.Count + 1; i++)
            {
                items.Add(iterator.Item(i));
            }
            return items;
        }
    }
}
