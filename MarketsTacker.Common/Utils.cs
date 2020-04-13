using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketsTracker.Common
{
    public static class Utils
    {
        public static IEnumerable<IEnumerable<T>> Split<T>(this T[] array, int size)
        {
            for (var i = 0; i < (float)array.Length / size; i++)
            {
                yield return array.Skip(i * size).Take(size);
            }
        }
        public static void GetPagingData(int page, int amount, out int from, out int to)
        {
            from = (page - 1) * amount;
            to = page * amount;
        }
    }
}
