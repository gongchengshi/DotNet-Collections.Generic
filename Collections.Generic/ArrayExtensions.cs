using Gongchengshi;
using System.Linq;

namespace Gongchengshi.Collections.Generic
{
    public static class ArrayExtensions
    {
       public static T[] GetRange<T>(this T[] @this, int start, int count)
       {
          return @this.Skip(start).Take(count).ToArray();
       }

        public static bool ContentsEqual<T>(this T[] left, T[] right)
        {
            if (left.Length != right.Length)
            {
                return false;
            }

            return !left.Where((t, i) => !t.Equals(right[i])).Any();
        }

        public static bool ContentsEqual(this double[] left, double[] right, double delta)
        {
            if (left.Length != right.Length)
            {
                return false;
            }

            return !left.Where((t, i) => !Compare.AreEqual(t, right[i], delta)).Any();
        }

        public static T[] InitializeToDefault<T>(this T[] array) where T : new()
        {
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = new T();
            }
            return array;
        }

        public static T[] InitializeTo<T>(this T[] array, T value)
        {
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = value;
            }
            return array;
        }
    }
}
