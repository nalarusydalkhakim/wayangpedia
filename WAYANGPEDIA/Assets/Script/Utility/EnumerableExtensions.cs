using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public static class EnumerableExtensions
    {
        public static int IndexOf<T>(this IEnumerable<T> items, T value)
        {
            int i = 0;
            foreach (var item in items)
            {
                if (item.Equals(value))
                {
                    return i;
                }
                i++;
            }

            return -1;
        }
    }
}