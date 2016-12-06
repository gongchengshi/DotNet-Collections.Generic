///////////////////////////////////////////////////////////////////////////////
//  COPYRIGHT (c) 2011 Schweitzer Engineering Laboratories, Pullman, WA
//////////////////////////////////////////////////////////////////////////////

using SEL.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using SEL.UnitTest;

namespace SEL.Collections.Generic.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for IListExtensionsTest and is intended
    ///to contain all IListExtensionsTest Unit Tests
    ///</summary>
    [TestClass]
    public class IListExtensionsTest
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



        [TestMethod()]
        public void RemoveAllTestOdds()
        {
            List<int> target = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            int expected = 5;
            int actual = (target as IList<int>).RemoveAll(new Predicate<int>(IdentifyOddNums));

            Assert.AreEqual(expected, actual);

            // The result should only include event numbers
            int[] expectedResult = new int[] { 2, 4, 6, 8, 10 };

            SELAssert.AreCollectionsEqual(expectedResult, target);
        }

        [TestMethod()]
        public void RemoveAllPrimesTest()
        {
            List<int> target = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            int expected = 4;
            int actual = (target as IList<int>).RemoveAll(new Predicate<int>(IsPrime));

            Assert.AreEqual(expected, actual);

            // The result should only include event numbers
            int[] expectedResult = new int[] { 1, 4, 6, 8, 9, 10 };

            SELAssert.AreCollectionsEqual(expectedResult, target);
        }


        // Predicate that removes odd numbers
        private static bool IdentifyOddNums(int i)
        {
            if (i % 2 == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool IsPrime(int i)
        {

            if (i == 1)
            {
                return false;
            }
            bool foundDivisor = false;

            for (int j = 2; j <= Math.Sqrt(i); j++)
            {
                if (i % j == 0)
                {
                    foundDivisor = true;
                    break;
                }
            }

            return !foundDivisor;
        }


        [TestMethod()]
        public void RemoveAllTest()
        {
            this.RemoveAllTestOdds();
            this.RemoveAllPrimesTest();            
        }

        [TestMethod()]
        public void RemoveFirstPrimeTest()
        {
            List<int> list = new List<int>() { 12, 15, 17, 18, 19, 20, 21, 22, 23 };

            // Remove the first prime number
            Predicate<int> match = new Predicate<int>(IsPrime);

            bool expected = true;
            bool actual;

            // Call remove with the prime predicate.
            actual = (list as IList<int>).RemoveFirst<int>(match);

            Assert.AreEqual(actual, expected);

            // See if the first prime was removed.
            int[] expectedResult = new int[] { 12, 15, 18, 19, 20, 21, 22, 23 };
            SELAssert.AreCollectionsEqual(expectedResult, list);
        }

        /// <summary>
        /// This tests the false case where it tries to remove the first prime from the list
        /// but there are none.
        /// </summary>
        [TestMethod()]
        public void RemoveFirstPrimeTestFalseCase()
        {
            List<int> list = new List<int>() { 12, 15, 18,  20, 22 };

            // Remove the first prime number
            Predicate<int> match = new Predicate<int>(IsPrime);

            bool actual;

            // Call remove with the prime predicate.
            actual = (list as IList<int>).RemoveFirst<int>(match);

            // See if the first prime was removed.
            int[] expectedResult = new int[] { 12, 15, 18, 20, 22 };
            SELAssert.AreCollectionsEqual(expectedResult, list);
        }

        [TestMethod()]
        public void RemoveFirstOddTest()
        {
            List<int> list = new List<int>() { 12, 15, 17, 18, 19, 20, 21, 22, 23 };

            // Remove the first prime number
            Predicate<int> match = new Predicate<int>(IdentifyOddNums);

            bool actual;

            // Call remove with the prime predicate.
            actual = (list as IList<int>).RemoveFirst<int>(match);            

            // See if the first prime was removed.
            int[] expectedResult = new int[] { 12, 17, 18, 19, 20, 21, 22, 23 };
            SELAssert.AreCollectionsEqual(expectedResult, list);
        }

       
        [TestMethod]
        public void FindIndexTest()
        {
            IList<int> list = new List<int>() { 1, 2, 5, 9 };

            Assert.AreEqual(0, list.FindIndex(i => i == 1));
            Assert.AreEqual(2, list.FindIndex(i => i > 2));
            Assert.AreEqual(3, list.FindIndex(i => i == 9));
            Assert.AreEqual(-1, list.FindIndex(i => i < 0));
        }
    }
}
