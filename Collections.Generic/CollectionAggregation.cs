﻿
// This is T4 generated code
// C# only allows constraining generic parameter types based on inheritance.
// Because ISortedList and IList don't share a common interface T4 had to be used to generate two separate classes.


using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Gongchengshi.Collections.Generic
{

    /// <summary>
    /// Methods to align a group of lists based on ordered unique keys and perform an 
    /// aggregation across common entries in the lists. This only works for list type collections
    /// But it could also be written for iEnumerable with some effort and sacrifice of performace.
    /// </summary>
    public class SortedListAggregation<KeyType, ValueType> : 
        SortedListAggregation<ISortedList<KeyType>, KeyType, ISortedList<ValueType>, ValueType>
        where KeyType : IComparable<KeyType>
    {}

    /// <summary>
    /// In most cases the more simplier version of this class above will be used instead of this.
    /// </summary>
    public class SortedListAggregation<KeyListType, KeyType, ValueListType, ValueType> 
        where KeyListType : ISortedList<KeyType>
        where KeyType : IComparable<KeyType>
        where ValueListType : ISortedList<ValueType>
    {
        /// <summary>
        /// Simplifies iterating through the lists.
        /// </summary>
        public class KeyListIterator
        {
            public KeyListIterator(KeyListType list)
            {
                if (list == null)
                {
                    throw new ArgumentNullException();
                }

                List = list;
                CurrentIndex = (List.Count == 0) ? -1 : 0;
            }

            private readonly KeyListType List;
            public int CurrentIndex { get; private set; }

            public KeyType Current
            {
                get
                {
                    if(PastEnd())
                    {
                        throw new InvalidOperationException();
                    }

                    return List[CurrentIndex];
                }
            }

            public void MoveNext()
            {
			    // Sets iterator to "null" state when you've gone over the end, and keep it there (sink state)
                if (CurrentIndex >= List.Count - 1 || CurrentIndex == -1)
                {
                    CurrentIndex = -1;
                }
                else
                {
                    CurrentIndex++;
                }
            }

            public bool PastEnd()
            {
                return CurrentIndex == -1;
            }

            public static KeyListIterator operator++(KeyListIterator operand)            
            {
                operand.MoveNext();

                return operand;
            }
        }

        /// <summary>
        /// Calls compute on each set of values that correspond to keys that are common across all lists.
        ///
        /// This finds where each of the lists line up (have the same key) by comparing them all to the first list.
        /// When they all line up compute() is called on the values that correspond to the key.
        /// </summary>
        /// <param name="compute">The function to call</param>
        /// <param name="orderedIndexes">The resulting list of keys</param>
        /// <param name="values">The resulting list of values</param>
        /// <param name="input">param array of key list and value lists tuples. This is the input.</param>
        public static void AggregateAndAddToLists(
            Func<KeyType, ValueType[], ValueType> compute,
            KeyListType orderedIndexes, ValueListType values,
            params Tuple<KeyListType, ValueListType>[] input)
        {
            foreach (var item in input)
            {
                if (item == null || item.Item2 == null || item.Item1.Count != item.Item2.Count)
                {
                    throw new ArgumentException();
                }
            }

            // These iterators are used to keep track of the current position in each list.
            var iterators = new KeyListIterator[input.Length];

            for (int i = 0; i < input.Length; ++i)
            {
                iterators[i] = new KeyListIterator(input[i].Item1);
            }

            // Start with the first item in the list. All other lists will be compared with this one.
            var first = iterators[0];

            var args = new ValueType[input.Length];

            while (!first.PastEnd())
            {
                bool equal = true;

                // If it gets all the way through this for loop without setting equal to false then we can
                // compute the aggregate function.
                for (int i = 1; i < iterators.Length; ++i)
                {
                    var other = iterators[i];

                    if (other.PastEnd())
                    {
                        // If any of the lists ends then we can't compute across all list so we should return.
                        return;
                    }

                    var compareResult = first.Current.CompareTo(other.Current);

                    // Go until everything is equal
                    if (compareResult < 0)
                    {                        
                        first.MoveNext();
                        equal = false;
                        break; // Check to see if we are past end
                    }
                    else if (compareResult > 0)
                    {
                        other.MoveNext();
                        equal = false;
                        continue; // Continue checking the rest of the lists
                    }
                }

                if (equal) // They current key of all the lists are the same
                {
                    // Create the parameter to compute
                    for (int j = 0; j < input.Length; ++j)
                    {
                        args[j] = input[j].Item2[iterators[j].CurrentIndex];
                    }

                    orderedIndexes.Add(first.Current);
                    values.Add(compute(first.Current, args));

                    foreach (var it in iterators)
                    {
                        it.MoveNext();
                    }
                }
            }
        }
    }    
}
