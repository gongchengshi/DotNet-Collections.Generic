///////////////////////////////////////////////////////////////////////////////
//  COPYRIGHT (c) 2011 Schweitzer Engineering Laboratories, Pullman, WA
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEL.UnitTest;

namespace SEL.Collections.Generic.UnitTests
{
    [TestClass]
    public class CollectionAggregationTest
    {
        static CollectionAggregationTest()
        {
            PerformanceIndexList1 = new SortedList<Index>(600);
            PerformanceValueList1 = new SortedList<double>(PerformanceIndexList1.Count);

            int index = 1;
            for (int i = 0; i < PerformanceIndexList1.Count; ++i)
            {
                PerformanceIndexList1.Add(I(index++));
                PerformanceValueList1.Add(index);
            }

            PerformanceIndexList2 = new SortedList<Index>(300);
            PerformanceValueList2 = new SortedList<double>(PerformanceIndexList2.Count);

            index = 1;
            for (int i = 0; i < PerformanceIndexList2.Count; ++i)
            {
                PerformanceIndexList2.Add(I(index));
                PerformanceValueList2.Add(index);
                index += 2;
            }
        }

        static SortedList<Index> PerformanceIndexList1;
        static SortedList<Index> PerformanceIndexList2;
        static SortedList<double> PerformanceValueList1;
        static SortedList<double> PerformanceValueList2;

        public struct Index : IComparable<Index>
        {
            public Index(int index)
            {
                I = index;
            }

            public int I;

            public int CompareTo(Index other)
            {
                if (I < other.I)
                    return -1;
                if (I > other.I)
                    return 1;
                return 0;
            }
        }

        private static Index I(int index)
        {
            return new Index(index);
        }

        public class SortedListAggregation : SortedListAggregation<Index, double>
        { }

        private static void AggregateLists(
            Func<Index, double[], double> compute,
            ISortedList<Index> expectedIndexes, ISortedList<double> expectedValues,
            params Tuple<ISortedList<Index>, ISortedList<double>>[] args)
        {
            SortedList<Index> indexes = new SortedList<Index>();
            SortedList<double> values = new SortedList<double>();

            SortedListAggregation.AggregateAndAddToLists(compute, indexes, values, args);

            Assert.AreEqual(indexes.Count, values.Count);
            Assert.AreEqual(expectedIndexes.Count, indexes.Count);

            for (int i = 0; i < indexes.Count; ++i)
            {
                Assert.AreEqual(expectedIndexes[i], indexes[i]);
                Assert.AreEqual(expectedValues[i], values[i]);
            }
        }

        public void AggregateLists(SortedList<Index> expectedIndexes, params SortedList<Index>[] input)
        {
            SortedList<double> expectedValues = new SortedList<double>(expectedIndexes.Count);

            var args = new Tuple<ISortedList<Index>, ISortedList<double>>[input.Length];

            foreach(var i in expectedIndexes)
            {
                expectedValues.Add(Math.Pow(i.I, input.Length));
            }

            for (int i = 0; i < input.Length; ++i)
            {
                var values = new SortedList<double>(input[i].Count);

                foreach(var index in input[i])
                {
                    values.Add(index.I);
                }

                args[i] = CreateTuple(input[i], values);
            }

            AggregateLists((t, a) =>
                    {
                        double result = 1;
                        foreach (var value in a)
                        {
                            result *= value;
                        }
                        return result;
                    }, 
                    expectedIndexes, expectedValues, args);
        }

        private static Tuple<ISortedList<Index>, ISortedList<double>> CreateTuple(SortedList<Index> indexes, SortedList<double> values)
        {
            return new Tuple<ISortedList<Index>, ISortedList<double>>(indexes, values);
        }

        [TestMethod]
        public void TestAggregateLists1()
        {
            AggregateLists(
                new SortedList<Index> { I(1), I(2), I(3), I(4), I(5), I(6), I(7), I(8), I(9), I(10) },
                new SortedList<Index> { I(1), I(2), I(3), I(4), I(5), I(6), I(7), I(8), I(9), I(10) },
                new SortedList<Index> { I(1), I(2), I(3), I(4), I(5), I(6), I(7), I(8), I(9), I(10) });
        }

        [TestMethod]
        public void TestAggregateLists2()
        {
            AggregateLists(
                new SortedList<Index> { I(2), I(3), I(4), I(5), I(6), I(7), I(8), I(9), I(10) },
                new SortedList<Index> { I(2), I(3), I(4), I(5), I(6), I(7), I(8), I(9), I(10) },
                new SortedList<Index> { I(1), I(2), I(3), I(4), I(5), I(6), I(7), I(8), I(9), I(10) });
        }

        [TestMethod]
        public void TestAggregateLists3()
        {
            AggregateLists(
                new SortedList<Index> { I(1), I(2), I(3), I(4), I(5), I(6), I(7), I(8), I(9) },
                new SortedList<Index> { I(1), I(2), I(3), I(4), I(5), I(6), I(7), I(8), I(9) },
                new SortedList<Index> { I(1), I(2), I(3), I(4), I(5), I(6), I(7), I(8), I(9), I(10) });
        }

        [TestMethod]
        public void TestAggregateLists4()
        {
            AggregateLists(
                new SortedList<Index> { I(3), I(4), I(5), I(6), I(7), I(8), I(9), I(10) },
                new SortedList<Index> { I(1), I(2), I(3), I(4), I(5), I(6), I(7), I(8), I(9), I(10) },
                new SortedList<Index> { I(3), I(4), I(5), I(6), I(7), I(8), I(9), I(10) });
        }

        [TestMethod]
        public void TestAggregateLists5()
        {
            AggregateLists(
                new SortedList<Index> { I(1), I(2), I(3), I(4), I(5), I(6), I(7), I(8), I(9), I(10) },
                new SortedList<Index> { I(1), I(2), I(3), I(4), I(5), I(6), I(7), I(8), I(9), I(10) },
                new SortedList<Index> { I(1), I(2), I(3), I(4), I(5), I(6), I(7), I(8), I(9), I(10) });
        }

        [TestMethod]
        public void TestAggregateLists6()
        {
            AggregateLists(
                new SortedList<Index> { },
                new SortedList<Index> { I(11) },
                new SortedList<Index> { I(1), I(2), I(3), I(4), I(5), I(6), I(7), I(8), I(9), I(10) });
        }

        [TestMethod]
        public void TestAggregateLists7()
        {
            AggregateLists(
                new SortedList<Index> { },
                new SortedList<Index> { I(1), I(2), I(3), I(4), I(5), I(6), I(7), I(8), I(9), I(10) },
                new SortedList<Index> { I(11) });
        }

        [TestMethod]
        public void TestAggregateLists8()
        {
            AggregateLists(
                new SortedList<Index> { },
                new SortedList<Index> { },
                new SortedList<Index> { I(1), I(2), I(3), I(4), I(5), I(6), I(7), I(8), I(9), I(10) });
        }

        [TestMethod]
        public void TestAggregateLists9()
        {
            AggregateLists(
                new SortedList<Index> { },
                new SortedList<Index> { I(1), I(2), I(3), I(4), I(5), I(6), I(7), I(8), I(9), I(10) },
                new SortedList<Index> { });
        }

        [TestMethod]
        public void TestAggregateLists10()
        {
            AggregateLists(
                new SortedList<Index> { I(2), I(4), I(6), I(8), I(10) },
                new SortedList<Index> { I(2), I(4), I(6), I(8), I(10) },
                new SortedList<Index> { I(1), I(2), I(3), I(4), I(5), I(6), I(7), I(8), I(9), I(10) });
        }

        [TestMethod]
        public void TestAggregateLists11()
        {
            AggregateLists(
                new SortedList<Index> { I(3), I(4), I(7), I(8) },
                new SortedList<Index> { I(3), I(4), I(7), I(8) },
                new SortedList<Index> { I(1), I(2), I(3), I(4), I(5), I(6), I(7), I(8), I(9), I(10) });
        }

        [TestMethod]
        public void TestAggregateLists12()
        {
            AggregateLists(
                new SortedList<Index> { I(1), I(2), I(7), I(8), I(9) },
                new SortedList<Index> { I(1), I(2), I(7), I(8), I(9) },
                new SortedList<Index> { I(1), I(2), I(3), I(4), I(5), I(6), I(7), I(8), I(9), I(10) });
        }

        [TestMethod]
        public void TestAggregateLists13()
        {
            var expectedIndexes = new SortedList<Index> { I(1), I(3), I(9) };

            AggregateLists(
                expectedIndexes,
                new SortedList<Index> { I(1), I(2), I(3), I(4), I(8), I(9), I(10) },
                new SortedList<Index> { I(1), I(2), I(3), I(4), I(5), I(6), I(7), I(8), I(9), I(10) },
                new SortedList<Index> { I(1), I(3), I(5), I(7), I(9) },
                new SortedList<Index> { I(1), I(3), I(6), I(9) });
        }

        [TestMethod]
        public void TestAggregateListsPerformance()
        {
            var indexes = new SortedList<Index>();
            var values = new SortedList<double>();

            for (int i = 0; i < 1000; ++i)
            {
                SortedListAggregation.AggregateAndAddToLists(
                    (t, args) => args[0] + args[1],
                    indexes, values,
                    Tuple.Create<ISortedList<Index>, ISortedList<double>>(
                        PerformanceIndexList1, PerformanceValueList1),
                    Tuple.Create<ISortedList<Index>, ISortedList<double>>(
                        PerformanceIndexList2, PerformanceValueList2));
            }

           
        }

         [TestMethod()]
        public void KeyListNullArgTest()
        {
           
           

        }
    }
}
