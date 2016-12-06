using System;
using System.Collections.Generic;
using System.Linq;

namespace Gongchengshi.Collections.Generic
{
    public static class ICollectionExtensions
    {
        /// <summary>
        /// Update the items in collection with withItems.  This is functionally equivalent to:
        ///    collection.Clear();
        ///    foreach (var item in withItems)
        ///        collection.Add(item);
        /// But, it has the advantage of only changing the items that actually changed.  This 
        /// eliminates unneeded collection changed events.
        /// </summary>
        public static void UpdateItems<T>(this ICollection<T> collection, IEnumerable<T> withItems)
        {
            var toRemove = collection.Where(e => !withItems.Contains(e)).ToArray();
            foreach (var remove in toRemove)
                collection.Remove(remove);

            foreach (var add in withItems)
                if (!collection.Contains(add))
                    collection.Add(add);
        }

        public static bool ContentsEqual<T>(this ICollection<T> left, ICollection<T> right)
        {
            if (left.Count != right.Count)
            {
                return false;
            }

            var rightEnumerator = right.GetEnumerator();
            rightEnumerator.MoveNext();

            foreach (var item in left)
            {
                if (!item.Equals(rightEnumerator.Current))
                {
                    return false;
                }

                rightEnumerator.MoveNext();
            }

            return true;
        }

        public static void AddRange<T>(this ICollection<T> @this, IEnumerable<T> collection)
        {
           foreach (var item in collection)
           {
              @this.Add(item);
           }
        }

        public static bool UnorderedCompare<T>(this ICollection<T> left, ICollection<T> right)
        {
            if (left.Count != right.Count)
            {
                return false;
            }

            foreach (var item in left)
            {
                if (!right.Contains(item))
                {
                    return false;
                }
            }

            return true;
        }

        static public void IfNotContainsAdd<T>(this ICollection<T> @this, T item)
        {
           if (!@this.Contains(item))
           {
              @this.Add(item);
           }
        }
    }
}
