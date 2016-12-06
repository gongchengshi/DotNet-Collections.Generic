using Gongchengshi;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Gongchengshi.Collections.Generic
{
   public static class IEnumerableExtensions
   {
      public static T FirstOrNew<T>(this IEnumerable<T> @this, Func<T, bool> predicate) where T : new()
      {
         return @this.FirstOr(predicate, () => new T());
      }

      public static T FirstOr<T>(this IEnumerable<T> @this, Func<T, bool> predicate, Func<T> onOr)
      {
         try
         {
            return @this.First(predicate);
         }
         catch (InvalidOperationException)
         {
            return onOr();
         }
      }

      public static int GetContentsHashCode<T>(this IEnumerable<T> source)
      {
         var hashBuilder = new HashBuilder();
         hashBuilder.AddItems(source);
         return hashBuilder.Result;
      }

      public static bool ContentsEqual<T>(this IEnumerable<T> left, IEnumerable<T> right)
      {
         var leftEnumerator = left.GetEnumerator();
         var rightEnumerator = right.GetEnumerator();

         while (true)
         {
            if (!leftEnumerator.MoveNext())
            {
               // If right.Enumerator.MoveNext returns false as well, both lists are the same length.
               return !rightEnumerator.MoveNext();
            }

            if (!rightEnumerator.MoveNext())
            {
               // Got to the end of right before left so they must not be the same length.
               return false;
            }

            if (!leftEnumerator.Current.Equals(rightEnumerator.Current))
            {
               return false;
            }
         }
      }

      public static bool ContentsEqual(this IEnumerable<double> left, IEnumerable<double> right, double delta)
      {
         var leftEnumerator = left.GetEnumerator();
         var rightEnumerator = right.GetEnumerator();

         while (true)
         {
            if (!leftEnumerator.MoveNext())
            {
               // If right.Enumerator.MoveNext returns false as well, both lists are the same length.
               return !rightEnumerator.MoveNext();
            }

            if (!rightEnumerator.MoveNext())
            {
               // Got to the end of right before left so they must not be the same length.
               return false;
            }

            if (!Compare.AreEqual(leftEnumerator.Current, rightEnumerator.Current, delta))
            {
               return false;
            }
         }
      }

      public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
      {
         Contract.Requires(source != null);
         Contract.Requires(action != null);

         foreach (var item in source)
         {
            action(item);
         }
      }

      public static void ForEachNonNull<TSource>(this IEnumerable<TSource> source, Action<TSource> action) where TSource : class
      {
         Contract.Requires(source != null);
         Contract.Requires(action != null);

         source.Where(x => x != null).ForEach(action);
      }

      /// <summary>
      /// Finds the differences between this and the passed in collection.  
      /// Items that are in collection but not in this are put in the Added bucket. 
      /// Items that are in this but not in the collection are put in the Removed bucket.
      /// </summary>
      public static DifferenceInfo<T> GetDifferences<T>(this IEnumerable<T> @this, IEnumerable<T> collection)
      {
         var differences = new DifferenceInfo<T>();

         // Find items that are in left but not in right
         foreach (var item in @this)
         {
            if (!collection.Contains(item))
            {
               differences.Removed.Add(item);
            }
         }

         // Find items that are in right but not in left.
         foreach (var item in collection)
         {
            if (!@this.Contains(item))
            {
               differences.Added.Add(item);
            }
         }

         return differences;
      }

      public static bool IsEmpty<T>(this IEnumerable<T> @this)
      {
         return !@this.Any();
      }

      public static void Split<T>(this IEnumerable<T> @this, Func<T, bool> condition,
          out IEnumerable<T> trueItems, out IEnumerable<T> falseItems)
      {
         trueItems = new List<T>();
         falseItems = new List<T>();

         foreach (var item in @this)
         {
            if (condition(item))
            {
               (trueItems as List<T>).Add(item);
            }
            else
            {
               (falseItems as List<T>).Add(item);
            }
         }
      }

      /// <summary>
      /// Align two enumerables that are comparable, returning a tuple of values that
      /// match in both.  The two enumerables must already be sorted. 
      /// </summary>
      public static IEnumerable<Tuple<TLeft, TRight>> AlignEnumerables<TLeft, TRight>
          (IEnumerable<TLeft> leftEnumerable, IEnumerable<TRight> rightEnumerable)
          where TLeft : IComparable<TRight>
      {
         var leftEnumerator = leftEnumerable.GetEnumerator();
         if (!leftEnumerator.MoveNext())
         {
            yield break;
         }
         var left = leftEnumerator.Current;
         foreach (var right in rightEnumerable)
         {
            if (left.CompareTo(right) > 0)
            {
               continue;
            }

            // While target is later than reference, advance to next reference timestamp.
            while (left.CompareTo(right) < 0)
            {
               if (!leftEnumerator.MoveNext())
               {
                  yield break;
               }
               left = leftEnumerator.Current;
            }

            if (left.CompareTo(right) == 0)
            {
               yield return Tuple.Create(left, right);
            }
         }
      }

      /// <summary>
      /// Given a list of IEnumerators, determine if all of the current values are the same (based on CompareTo)
      /// </summary>
      private static bool IsAligned<T>(IEnumerable<IEnumerator<T>> list) where T : IComparable<T>
      {
         var tmp = list.First().Current;
         foreach (var enumerator in list)
         {
            if (enumerator.Current.CompareTo(tmp) != 0)
            {
               return false;
            }
         }
         return true;
      }

      /// <summary>
      /// Given a list of IEnumerators, return the largest of the current values (based on CompareTo)
      /// </summary>
      private static T GetLargestEnumerator<T>(IEnumerable<IEnumerator<T>> list) where T : IComparable<T>
      {
         T largest = list.First().Current;
         foreach (var enumerator in list)
         {
            if (enumerator.Current.CompareTo(largest) > 0)
            {
               largest = enumerator.Current;
            }
         }
         return largest;
      }

      /// <summary>
      /// Given an array of sorted IEnumerables, return all of the values that are aligned (based on CompareTo == 0)
      /// </summary>
      public static IEnumerable<T[]> AlignEnumerables<T>(IEnumerable<T>[] enums) where T : IComparable<T>
      {
         var enumerators = new List<IEnumerator<T>>();
         bool endOfList = false;
         foreach (var enu in enums)
         {
            var enumerator = enu.GetEnumerator();
            if (!enumerator.MoveNext())
            { yield break; }
            enumerators.Add(enumerator);
         }

         while (!endOfList)
         {
            var largest = GetLargestEnumerator(enumerators);
            foreach (var e in enumerators)
            {
               while (e.Current.CompareTo(largest) < 0)
               {
                  if (!e.MoveNext())
                  {
                     yield break;
                  }
               }
            }
            if (IsAligned(enumerators))
            {
               var row = new T[enumerators.Count()];
               int i = 0;
               foreach (var e in enumerators)
               {
                  row[i++] = e.Current;
                  if (!e.MoveNext())
                  {
                     endOfList = true;
                  }
               }
               yield return row;
            }
         }
      }
   }
}