using System.Linq;
using SEL.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SEL.Collections.Generic.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for IEnumerableExtensionsTest and is intended
    ///to contain all IEnumerableExtensionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IEnumerableExtensionsTest
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
        ///A test for ContentsEqual
        ///</summary>
        public void ContentsEqualTestHelper<T>()
        {
            IEnumerable<T> left = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> right = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = IEnumerableExtensions.ContentsEqual<T>(left, right);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        int[] arr1 = new int[] { 1, 2, 3 };
        int[] arr2 = new int[] { 1, 2, 3 };
        int[] arr3 = new int[] { 1, 2, 3, 4 };
        int[] arr4 = new int[] { 1, 2, 3, 5 };

        double[] darr1 = new double[] { 1, 2, 3 };
        double[] darr2 = new double[] { 1.1, 1.9, 3 };
        double[] darr3 = new double[] { 1, 2, 3, 4 };
        double[] darr4 = new double[] { 1, 2, 3, 5 };

        [TestMethod()]
        public void ContentsEqualTestHappy1()
        {
            Assert.IsTrue(ArrayExtensions.ContentsEqual<int>(arr1, arr2));
            Assert.IsTrue(ArrayExtensions.ContentsEqual(darr1, darr2, 0.11));
            Assert.IsTrue(IEnumerableExtensions.ContentsEqual<int>(arr1, arr2));
            Assert.IsTrue(IEnumerableExtensions.ContentsEqual(darr1, darr2, 0.11));
        }
        [TestMethod()]
        public void ContentsEqualSad1()
        {
            Assert.IsFalse(ArrayExtensions.ContentsEqual<int>(arr3, arr1));
            Assert.IsFalse(ArrayExtensions.ContentsEqual(darr3, darr1, 0.11));
            Assert.IsFalse(IEnumerableExtensions.ContentsEqual<int>(arr3, arr1));
            Assert.IsFalse(IEnumerableExtensions.ContentsEqual(darr3, darr1, 0.11));
        }
      

        [TestMethod()]
        public void ContentsEqualTestSad2()
        {
            Assert.IsFalse(ArrayExtensions.ContentsEqual<int>(arr3, arr4));
            Assert.IsFalse(ArrayExtensions.ContentsEqual(darr3, darr4, 0.11));
            Assert.IsFalse(IEnumerableExtensions.ContentsEqual<int>(arr3, arr4));
            Assert.IsFalse(IEnumerableExtensions.ContentsEqual(darr3, darr4, 0.11));
        }

        [TestMethod()]
        public void GetContentsHashCodeTest()
        {
            Assert.IsTrue(IEnumerableExtensions.GetContentsHashCode<int>(arr1) ==
                          IEnumerableExtensions.GetContentsHashCode<int>(arr2));
        }

        [TestMethod()]
        public void InitializeToTest()
        {
            var foos = new Foo[3];
            Assert.IsNull(foos[2]);
            foos.InitializeToDefault();
            Assert.IsNotNull(foos[0]); 
            Assert.IsNotNull(foos[2]);

            var ints = new int[3];
            Assert.AreEqual(0, ints[2]);
            ints.InitializeTo(42);
            Assert.AreEqual(42, ints[0]);
            Assert.AreEqual(42, ints[2]);
        }

        class Foo
        {
        }

        [TestMethod]
        public void TestForEach()
        {
            var concat = string.Empty;
            arr1.ForEach(v => concat += v.ToString());
            Assert.AreEqual("123", concat);
        }

        [TestMethod]
        public void TestForEachNonNull()
        {
            var foos = new List<Foo> {new Foo(), null, new Foo()};
            var count = 0;
            foos.ForEachNonNull(f => count++);
            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void TestSplit()
        {
            var ints = Enumerable.Range(1, 3);
            IEnumerable<int> odds, evens;
            ints.Split(i => i % 2 == 0, out evens, out odds);
            CollectionAssert.AreEqual(new [] { 1, 3 }.ToList(), odds.ToList());
            CollectionAssert.AreEqual(new [] { 2 }.ToList(), evens.ToList());

        }


        [TestMethod]
        public void TestAlignEnumerable()
        {
            List<string> left = new List<string> { "A", "C", "E", "G", "H", "I" };
            List<string> right = new List<string> { "B", "C", "D", "F", "G", "I" };
            var result = IEnumerableExtensions.AlignEnumerables(left, right);
            result.ForEach(v => Assert.AreEqual(v.Item1, v.Item2));
            var expected = new[] { "C", "G", "I" }.ToList();
            var actual = result.Select(v => v.Item1).ToList();
            CollectionAssert.AreEqual(expected, actual);

            result = IEnumerableExtensions.AlignEnumerables(new string[0], right);
            Assert.IsFalse(result.Any());

            result = IEnumerableExtensions.AlignEnumerables(left, new[] { " " });
            Assert.IsFalse(result.Any());

            result = IEnumerableExtensions.AlignEnumerables(left, new[] { "J" });
            Assert.IsFalse(result.Any());

        }

        [TestMethod]
        public void TestAlignEnumerables()
        {
            var one = new List<TestInt> { new TestInt(1), new TestInt(2), new TestInt(3), new TestInt(4), new TestInt(5), 
                                            new TestInt(6), new TestInt(7), new TestInt(8)};
            var two = new List<TestInt> {new TestInt(2), new TestInt(4), new TestInt(6), new TestInt(8) };
            var three = new List<TestInt> { new TestInt(1), new TestInt(3), new TestInt(4), new TestInt(7), new TestInt(8)};
            var four = new List<TestInt> { new TestInt(-1), new TestInt(2), new TestInt(3), new TestInt(4),
                                            new TestInt(7), new TestInt(8), new TestInt(10)};

            var result = new List<TestInt[]>(IEnumerableExtensions.AlignEnumerables(new[] { one, two, three, four }));
            foreach (var arr in result)
            {
                Assert.AreEqual(arr[0], arr[1]);
                Assert.AreEqual(arr[1], arr[2]);
                Assert.AreEqual(arr[2], arr[3]);
            }

            var expected1 = new[] { new TestInt(4), new TestInt(4), new TestInt(4), new TestInt(4) };
            var expected2 = new[] { new TestInt(8), new TestInt(8), new TestInt(8), new TestInt(8) };
            var expected = new List<TestInt[]>() { expected1, expected2 };

            if (expected.Count() != result.Count())
            {
                Assert.Fail();
            }
            else
            {
                for (int i = 0; i < expected.Count; i++)
                {
                    CollectionAssert.AreEqual(expected[i], result[i]);
                }
            }

            result = new List<TestInt[]>(IEnumerableExtensions.AlignEnumerables(new [] {new List<TestInt>(), one, two, three}));
            Assert.IsFalse(result.Any());

            result = new List<TestInt[]>(IEnumerableExtensions.AlignEnumerables(new[] { new List<TestInt>(){new TestInt(11)}, one, two, three }));
            Assert.IsFalse(result.Any());

            result = new List<TestInt[]>(IEnumerableExtensions.AlignEnumerables(new[] { new List<TestInt>(){new TestInt(0)}, one, two, three }));
            Assert.IsFalse(result.Any());

        }
    }
    class TestInt : IComparable<TestInt>
    {
        int val { get; set; }

        public TestInt(int newVal)
        {
            val = newVal;
        }

        public int CompareTo(TestInt other)
        {
            return val.CompareTo(other.val);
        }

        public override bool Equals(object obj)
        {
            if (obj is TestInt)
            {
                return val.Equals((obj as TestInt).val);
            }
            return false;
        }
    }
}
