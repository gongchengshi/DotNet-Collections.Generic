using System;
using System.Collections.Generic;

namespace Gongchengshi.Collections.Generic
{
    public class ForEach
    {
        public static void Do<T1, T2>(IEnumerable<T1> first, IEnumerable<T2> second, Action<T1, T2> action)
        {
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();

            while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext())
            {
                action(firstEnumerator.Current, secondEnumerator.Current);
            }
        }

        public static void Do<T1, T2, T3>(
            IEnumerable<T1> first,
            IEnumerable<T2> second,
            IEnumerable<T3> third,
            Action<T1, T2, T3> action)
        {
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();
            var thirdEnumerator = third.GetEnumerator();

            while (
                firstEnumerator.MoveNext() &&
                secondEnumerator.MoveNext() &&
                thirdEnumerator.MoveNext())
            {
                action(
                    firstEnumerator.Current,
                    secondEnumerator.Current,
                    thirdEnumerator.Current);
            }
        }

        public static void Do<T1, T2, T3, T4>(
            IEnumerable<T1> first,
            IEnumerable<T2> second,
            IEnumerable<T3> third,
            IEnumerable<T4> fourth,
            Action<T1, T2, T3, T4> action)
        {
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();
            var thirdEnumerator = third.GetEnumerator();
            var fourthEnumerator = fourth.GetEnumerator();

            while (
                firstEnumerator.MoveNext() &&
                secondEnumerator.MoveNext() &&
                thirdEnumerator.MoveNext() &&
                fourthEnumerator.MoveNext())
            {
                action(
                    firstEnumerator.Current,
                    secondEnumerator.Current,
                    thirdEnumerator.Current,
                    fourthEnumerator.Current);
            }
        }

        public static void Do<T1, T2, T3, T4, T5, T6>(
            IEnumerable<T1> first, 
            IEnumerable<T2> second,
            IEnumerable<T3> third,
            IEnumerable<T4> fourth,
            IEnumerable<T5> fifth,
            IEnumerable<T6> sixth, Action<T1, T2, T3, T4, T5, T6> action)
        {
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();
            var thirdEnumerator = third.GetEnumerator();
            var fourthEnumerator = fourth.GetEnumerator();
            var fifthEnumerator = fifth.GetEnumerator();
            var sixthEnumerator = sixth.GetEnumerator();

            while (
                firstEnumerator.MoveNext() && 
                secondEnumerator.MoveNext() &&
                thirdEnumerator.MoveNext() &&
                fourthEnumerator.MoveNext() &&
                fifthEnumerator.MoveNext() &&
                sixthEnumerator.MoveNext())
            {
                action(
                    firstEnumerator.Current, 
                    secondEnumerator.Current,
                    thirdEnumerator.Current,
                    fourthEnumerator.Current,
                    fifthEnumerator.Current,
                    sixthEnumerator.Current);
            }
        }
    }
}