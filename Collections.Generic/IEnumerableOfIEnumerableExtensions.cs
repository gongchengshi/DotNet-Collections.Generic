using System.Collections.Generic;
using System.Linq;

namespace Gongchengshi.Collections.Generic
{
   public static class IEnumerableOfIEnumerableExtensions
   {
      public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<IEnumerable<T>> sequences)
      { 
         IEnumerable<IEnumerable<T>> result = new[] { Enumerable.Empty<T>() };
         return sequences.Aggregate(result, (current, s) => (from seq in current
                                                             from item in s
                                                             select seq.Concat(new[] {item})));
      }
   }
}
