///////////////////////////////////////////////////////////////////////////////
//  COPYRIGHT (c) 2011 Schweitzer Engineering Laboratories, Pullman, WA
//////////////////////////////////////////////////////////////////////////////

using SEL.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using SEL.UnitTest;

namespace SEL.Collections.Generic.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for ISortedListTest and is intended
    ///to contain all ISortedListTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ISortedListTest
    {


        public static void RunTestOnDerivedClasses()
        {
            ;
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
        ///A test for IndexOf
        ///</summary>
        public static void IndexOfTestHelper(ISortedList<int> target, int value, int index)
        {

            int actual = target.IndexOf(value);

            Assert.IsTrue(actual == index);
        }

        public static void IndexOfTest(ISortedList<int> list, int value, int index)
        {
            IndexOfTestHelper(list, value, index);
        }

        /// <summary>
        ///A test for RemoveAt
        ///</summary>
        public static void RemoveAtTestHelper(ISortedList<int> target, List<int> indexes, ISortedList<int> expectedResult)
        {
            // First, sort the indexes so they are removed in the correct order
            indexes.Sort();

            for (int i = indexes.Count - 1; i >= 0; i--)
            {
                target.RemoveAt(indexes[i]);
            }

            // Compare current target and result. They should be the same.

            SELAssert.AreCollectionsEqual(target, expectedResult);

        }

        /// <summary>
        ///A test for Item
        ///</summary>
        public static void ItemTestHelper(ISortedList<int> target, int index, int expectedValue)
        {
            
            int actual;
            
            actual = target[index];

            Assert.IsTrue(expectedValue == actual);
        }
    }
}
