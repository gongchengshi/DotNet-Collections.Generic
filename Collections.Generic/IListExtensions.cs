using System;
using System.Collections.Generic;

namespace Gongchengshi.Collections.Generic
{
    public static class IListExtensions
    {
        /// <summary>
        /// RemoveAll() for IList that works the same as List(T)
        /// </summary>        
        public static int RemoveAll<T>(this IList<T> list, Predicate<T> match)
        {
            int numRemoved = 0;
            for(int i = 0; i < list.Count; ++i)
            {
                if (match(list[i]))
                {
                    list.RemoveAt(i--);
                    ++numRemoved;
                }
            }

            return numRemoved;
        }         

        /// <summary>
        /// Removes the first element in the list that matches the predicate.
        /// </summary>
        /// <returns>True if an item was removed.</returns>
        public static bool RemoveFirst<T>(this IList<T> list, Predicate<T> match)
        {
            for (int i = 0; i < list.Count; ++i)
            {
                if (match(list[i]))
                {
                    list.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        public static int FindIndex<T>(this IList<T> list, Predicate<T> match)
        {
            for (int i = 0; i < list.Count; ++i)
            {
                if (match(list[i]))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
