///////////////////////////////////////////////////////////////////////////////
//  COPYRIGHT (c) 2011 Schweitzer Engineering Laboratories, Pullman, WA
//////////////////////////////////////////////////////////////////////////////

using SEL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SEL.Collections.Generic.UnitTests
{
    /// <summary>
    ///This is a test class for TupleListTest and is intended
    ///to contain all TupleListTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TupleListTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        /// Generic constructor test. Ensures that exception is not thrown.
        /// </summary>
        public void TupleListConstructorTest1Helper<T1, T2, T3>()
        {
            TupleList<T1, T2, T3> target = new TupleList<T1, T2, T3>();
        }

        /// <summary>
        /// Generic constructor test. Ensures that exception is not thrown.
        /// </summary>
        [TestMethod()]
        public void TupleListConstructorTest1()
        {
            TupleListConstructorTest1Helper<GenericParameterHelper, GenericParameterHelper, GenericParameterHelper>();
        }


        /// <summary>
        /// Simple constructor test for <int> type. The TupleList is largely based on the .Net 
        /// Tuple and List classes, so extensive tests are not needed.
        /// </summary>
        [TestMethod()]

        public void TupleListConstructorTest()
        {
            TupleList<int, int> list = new TupleList<int, int>();
        }

        /// <summary>
        /// Test addition and verification of adding int tuples.
        /// </summary>
        [TestMethod()]
        public void AddTestDoubles()
        {
            System.Random rand = new Random(0);

            // Generate random double values and verify that you can find the values in the tuple "by value" using
            // .Contains.
            Tuple<double, double> firstTuple = new Tuple<double, double>(rand.NextDouble(), rand.NextDouble());
            Tuple<double, double> secondTuple = new Tuple<double, double>(rand.NextDouble(), rand.NextDouble());

            TupleList<double, double> tupleList1 = new TupleList<double, double>();
            tupleList1.Add(firstTuple);
            tupleList1.Add(secondTuple);

            Assert.IsTrue(tupleList1.Contains(new Tuple<double, double>(firstTuple.Item1, firstTuple.Item2)));
            Assert.IsTrue(tupleList1.Contains(new Tuple<double, double>(secondTuple.Item1, secondTuple.Item2)));

        }
        /// <summary>
        /// Test addition and verification of adding double tuples.
        /// </summary>
        [TestMethod()]
        public void AddTestInts2Tuples()
        {
            System.Random rand = new Random(1);

            // Generate random double values and verify that you can find the values in the tuple "by value" using
            // .Contains.
            Tuple<int, int> firstTuple = new Tuple<int, int>(rand.Next(int.MaxValue), rand.Next(int.MaxValue));
            Tuple<int, int> secondTuple = new Tuple<int, int>(rand.Next(int.MaxValue), rand.Next(int.MaxValue));

            TupleList<int, int> tupleList1 = new TupleList<int, int>();
            tupleList1.Add(firstTuple.Item1, firstTuple.Item2);
            tupleList1.Add(secondTuple.Item1, secondTuple.Item2);

            Assert.IsTrue(tupleList1.Contains(new Tuple<int, int>(firstTuple.Item1, firstTuple.Item2)));
            Assert.IsTrue(tupleList1.Contains(new Tuple<int, int>(secondTuple.Item1, secondTuple.Item2)));

            Assert.IsFalse(tupleList1.Contains(new Tuple<int, int>(4, 1)));

        }

        /// <summary>
        /// Test addition and verification of adding double tuples.
        /// </summary>
        [TestMethod()]
        public void AddTestInts3Tuples()
        {
            System.Random rand = new Random(1);

            // Generate random double values and verify that you can find the values in the tuple "by value" using
            // .Contains.
            Tuple<int, int, int> firstTuple = new Tuple<int, int, int>(rand.Next(int.MaxValue), rand.Next(int.MaxValue), rand.Next(int.MaxValue));
            Tuple<int, int, int> secondTuple = new Tuple<int, int, int>(rand.Next(int.MaxValue), rand.Next(int.MaxValue), rand.Next(int.MaxValue));

            TupleList<int, int, int> tupleList1 = new TupleList<int, int, int>();
            tupleList1.Add(firstTuple.Item1, firstTuple.Item2, firstTuple.Item3);
            tupleList1.Add(secondTuple.Item1, secondTuple.Item2, secondTuple.Item3);

            Assert.IsTrue(tupleList1.Contains(new Tuple<int, int, int>(firstTuple.Item1, firstTuple.Item2, firstTuple.Item3)));
            Assert.IsTrue(tupleList1.Contains(new Tuple<int, int, int>(secondTuple.Item1, secondTuple.Item2, secondTuple.Item3)));

            Assert.IsFalse(tupleList1.Contains(new Tuple<int, int, int>(4, 1,1)));

        }

        /// <summary>
        /// Test addition and verification of adding double tuples.
        /// </summary>
        [TestMethod()]
        public void AddTestInts4Tuples()
        {
            System.Random rand = new Random(1);

            // Generate random double values and verify that you can find the values in the tuple "by value" using
            // .Contains.
            Tuple<int, int, int, int> firstTuple = new Tuple<int, int, int, int>(rand.Next(int.MaxValue), rand.Next(int.MaxValue), rand.Next(int.MaxValue), rand.Next(int.MaxValue));
            Tuple<int, int, int, int> secondTuple = new Tuple<int, int, int, int>(rand.Next(int.MaxValue), rand.Next(int.MaxValue), rand.Next(int.MaxValue), rand.Next(int.MaxValue));

            TupleList<int, int, int, int> tupleList1 = new TupleList<int, int, int, int>();
            tupleList1.Add(firstTuple.Item1, firstTuple.Item2, firstTuple.Item3, firstTuple.Item4);
            tupleList1.Add(secondTuple.Item1, secondTuple.Item2, secondTuple.Item3, secondTuple.Item4);

            Assert.IsTrue(tupleList1.Contains(new Tuple<int, int, int, int>(firstTuple.Item1, firstTuple.Item2, firstTuple.Item3, firstTuple.Item4)));
            Assert.IsTrue(tupleList1.Contains(new Tuple<int, int, int, int>(secondTuple.Item1, secondTuple.Item2, secondTuple.Item3, secondTuple.Item4)));

            Assert.IsFalse(tupleList1.Contains(new Tuple<int, int, int, int>(4, 1, 1, 3)));

        }

    }
}
