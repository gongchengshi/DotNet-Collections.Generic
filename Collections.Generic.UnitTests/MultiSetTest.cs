using SEL.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections;
using SEL.UnitTest;

namespace SEL.Collections.Generic.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for MultiSetTest and is intended
    ///to contain all MultiSetTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MultiSetTest
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
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void AddTest()
        {
            MultiSet<int> target = new MultiSet<int>(); 
            ((ICollection<int>)(target)).Add(7);
            int[] expectedResult = new int[] { 7 };
            SELAssert.AreCollectionsEqual(target, expectedResult);
        }

        [TestMethod()]
        public void ClearTest()
        {
            int[] expectedResult = new int[] { };
            MultiSet<int> target = new MultiSet<int>() { -3, -2, -1, 0, 1, 2, 3 };
            target.Clear();

            SELAssert.AreCollectionsEqual(expectedResult, target);
        }

        [TestMethod()]
        public void ContainsTestTrue()
        {
            MultiSet<int> target = new MultiSet<int>() { -3, -2, -1, 0, 1, 2, 3 };
            Assert.IsTrue(target.Contains(-3));
        }

        [TestMethod()]
        public void ContainsTestFalse()
        {
            MultiSet<int> target = new MultiSet<int>() { -3, -2, -1, 0, 1, 2, 3 };
            Assert.IsFalse(target.Contains(-4));
        }

        /// <summary>
        ///A test for CopyTo
        ///</summary>
        ///
        [TestMethod()]
        public void CopyToTestHelper()
        {
            MultiSet<int> target = new MultiSet<int>() { -3, -2, -1, 0, 1, 2, 3 };
            int[] array = new int[target.Count];
            
            target.CopyTo(array, 0);

            SELAssert.AreCollectionsEqual(target, array);
        }
                   
        [TestMethod()]
        public void RemoveTestHappy()
        {
            MultiSet<int> set = new MultiSet<int> { 1, 2, 3, 4, 5 };
            int[] expectedResult = new int[] { 1, 2, 4, 5 };

            bool result = set.Remove(3);
            Assert.IsTrue(result == true);
            SELAssert.AreCollectionsEqual(expectedResult, set);            
        }

        [TestMethod()]
        public void RemoveTestHappy2()
        {
            MultiSet<int> set = new MultiSet<int> { 1, 2, 3, 3, 4, 5 };
            int[] expectedResult = new int[] { 1, 2, 3, 4, 5 };

            bool result = set.Remove(3);
            Assert.IsTrue(result == false);
            SELAssert.AreCollectionsEqual(expectedResult, set);

        }
        [TestMethod()]
        public void RemoveTestSad()
        {
            MultiSet<int> set = new MultiSet<int> { 1, 2, 3, 4, 5 };
            int[] expectedResult = new int[] { 1, 2, 3, 4, 5 };

            bool result = set.Remove(10);
            Assert.IsTrue(result == false);

            SELAssert.AreCollectionsEqual(expectedResult, set);
        }


        [TestMethod()]
        [DeploymentItem("SEL.Collections.Generic.dll")]
        public void GetEnumeratorTest1()
        {
            // This gets an enumerator of all the keys
            int[] expectedResult = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            MultiSet<int> set = new MultiSet<int>(){1,2,3,4,5,6,7,8};

            IEnumerator<int> enumerator = set.GetEnumerator();

            int counter = 0;
            bool goOn = true;
            while(goOn = enumerator.MoveNext()){
                Assert.IsTrue(enumerator.Current == expectedResult[counter++]);
            } 

        }

        [TestMethod()]
        [DeploymentItem("SEL.Collections.Generic.dll")]
        public void GetEnumeratorTest2()
        {
            // This gets an enumerator of all the keys
            int[] expectedResult = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            MultiSet<int> set = new MultiSet<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };

            System.Collections.IEnumerator enumerator = ((System.Collections.IEnumerable)(set)).GetEnumerator();

            int counter = 0;
            bool goOn = true;
            while (goOn = enumerator.MoveNext())
            {
                Assert.IsTrue((int)enumerator.Current == expectedResult[counter++]);
            }
        }

        /// <summary>
        ///A test for Count
        ///</summary>
        [TestMethod()]
        public void CountTest()
        {
            MultiSet<int> target = new MultiSet<int>() { 1, 2, 3, 4, 5, 6 };
            int actual;
            actual = target.Count;

            Assert.IsTrue(actual == target.Count);
        }

        /// <summary>
        ///A test for IsReadOnly
        ///</summary>
        [TestMethod()]
        public void IsReadOnlyTest()
        {
            MultiSet<int> target = new MultiSet<int>();
            bool actual;
            actual = target.IsReadOnly;

            Assert.IsFalse(actual);
        }


        MultiSet<int> TEST_SET_1 = new MultiSet<int>() { 1, 2, 3 };

        [TestMethod()]
        public void NotImplementedExceptionTests()
        {
            ExceptionAssert.Throws<NotImplementedException>(() => {
                TEST_SET_1.ExceptWith(TEST_SET_1);
            });
            ExceptionAssert.Throws<NotImplementedException>(() =>
            {
                TEST_SET_1.IntersectWith(TEST_SET_1);
            });
            ExceptionAssert.Throws<NotImplementedException>(() =>
            {
                TEST_SET_1.IsProperSubsetOf(TEST_SET_1);
            });
            ExceptionAssert.Throws<NotImplementedException>(() =>
            {
                TEST_SET_1.IsProperSupersetOf(TEST_SET_1);
            });
            ExceptionAssert.Throws<NotImplementedException>(() =>
            {
                TEST_SET_1.IsSubsetOf(TEST_SET_1);
            });
            ExceptionAssert.Throws<NotImplementedException>(() =>
            {
                TEST_SET_1.IsSupersetOf(TEST_SET_1);
            });
            ExceptionAssert.Throws<NotImplementedException>(() =>
            {
                TEST_SET_1.Overlaps(TEST_SET_1);
            });
            ExceptionAssert.Throws<NotImplementedException>(() =>
            {
                TEST_SET_1.SetEquals(TEST_SET_1);
            });
            ExceptionAssert.Throws<NotImplementedException>(() =>
            {
                TEST_SET_1.SymmetricExceptWith(TEST_SET_1);
            });
            ExceptionAssert.Throws<NotImplementedException>(() =>
            {
                TEST_SET_1.UnionWith(TEST_SET_1);
            });
        }
    }
}
