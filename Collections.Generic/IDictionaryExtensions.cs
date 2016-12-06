using System;
using System.Collections.Generic;

namespace Gongchengshi.Collections.Generic
{
   public static class IDictionaryExtensions
   {
      public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, TValue defaultValue=default(TValue))
      {
         TValue outValue;
         return @this.TryGetValue(key, out outValue) ? outValue : defaultValue;
      }

      public static bool ContentsEqual<TKey, TValue>(this IDictionary<TKey, TValue> left,
                                                     IDictionary<TKey, TValue> right)
      {
         return left.Keys.ContentsEqual(right.Keys) && left.Values.ContentsEqual(right.Values);
      }

      public static bool UnorderedCompare<TKey, TValue>(this IDictionary<TKey, ICollection<TValue>> left,
                                                        IDictionary<TKey, ICollection<TValue>> right)
      {
         if (left.Count != right.Count)
         {
            return false;
         }

         foreach (var item in left)
         {
            ICollection<TValue> rightItem;
            if (!right.TryGetValue(item.Key, out rightItem))
            {
               return false;
            }
            if (!item.Value.UnorderedCompare(rightItem))
            {
               return false;
            }
         }

         return true;
      }

      public static bool ExceptionFreeAdd<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, TValue value)
      {
         try
         {
            @this.Add(key, value);
            return true;
         }
         catch (ArgumentException) // An element with the same key already exists in the Dictionary<TKey, TValue>
         {
            return false;
         }
      }

   }
}