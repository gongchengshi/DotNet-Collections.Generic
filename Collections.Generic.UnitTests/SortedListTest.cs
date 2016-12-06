///////////////////////////////////////////////////////////////////////////////
//  COPYRIGHT (c) 2011 Schweitzer Engineering Laboratories, Pullman, WA
//////////////////////////////////////////////////////////////////////////////

using SEL.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections;
using SEL.UnitTest;

namespace SEL.Collections.Generic.UnitTests
{   
    /// <summary>
    ///This is a test class for SortedListTest and is intended
    ///to contain all SortedListTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SortedListTest
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
        ///A test for SortedList`1 Constructor
        ///</summary>
        public void SortedListConstructorTestHelper<T>()
        {
            IComparer<T> comparer = null; // TODO: Initialize to an appropriate value
            SortedList<T> target = new SortedList<T>(comparer);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        
        public void SortedListConstructorTest()
        {
            SortedListConstructorTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for SortedList`1 Constructor
        ///</summary>
        public void SortedListConstructorTest1Helper<T>()
        {
            SortedList<T> target = new SortedList<T>();
        }

        [TestMethod()]
        public void SortedListConstructorTest1()
        {
            SortedListConstructorTest1Helper<GenericParameterHelper>();
        }

        [TestMethod()]
        public void AddTest()
        {
            int[] originalData = new int[]{ 5, 7, 4, 2, 1, 6, 3 };

            SortedList<int> list = new SortedList<int>();
            for (int i = 0; i < originalData.Length; i++)
            {
                list.Add(originalData[i]);
            }
            
            int[] expectedArray = new int[] { 1, 2, 3, 4, 5, 6, 7 };

            // Compare lists
            for (int i = 0; i < expectedArray.Length; i++)
            {
                Assert.AreEqual(expectedArray[i], list[i]);
            }

            
        }

        [TestMethod()]
        public void AddTest2()
        {
            int[] originalData = new int[] { 5};

            SortedList<int> list = new SortedList<int>();
            for (int i = 0; i < originalData.Length; i++)
            {
                list.Add(originalData[i]);
            }

            int[] expectedArray = new int[] { 5 };

            SELAssert.AreCollectionsEqual(expectedArray, list);
        }

        [TestMethod()]
        public void AddNullCase()
        {
            SortedList_Accessor<string> target = new SortedList_Accessor<string>() { "A", "B" };
       
            ExceptionAssert.Throws<ArgumentException>(() => { target.Add(null);});
        }

        [TestMethod()]
        public void CheckCapacityTest()
        {
            CheckCapacityTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for CheckCapacity
        ///</summary>
        public void CheckCapacityTestHelper<T>()
        {
            SortedList_Accessor<T> target = new SortedList_Accessor<T>(); 
            int min = 10; 
            target.ResizeIfLessThan(min);
            Assert.IsTrue(target.Capacity >= min);
        }


        /// <summary>
        ///A test for Clear
        ///</summary>
        public void ClearTestHelper<T>()
        {
            SortedList<T> target = new SortedList<T>() { default(T) };
            target.Clear();
            Assert.IsTrue(target.Count == 0);
        }

        [TestMethod()]
        public void ClearTest()
        {
            ClearTestHelper<int>();
        }

        /// <summary>
        ///A test for Contains
        ///</summary>
        public void ContainsTestHelperTrue<T>()
        {
            SortedList<int> target = new SortedList<int> { 4, 6, 77, 45 };
            int value = target[3];
            bool expected = true; 
            bool actual;
            actual = target.Contains(value);
            Assert.AreEqual(expected, actual);
            
        }

        /// <summary>
        ///A test for Contains
        ///</summary>
        public void ContainsTestHelperFalse<T>()
        {
            SortedList<int> target = new SortedList<int> { 4, 6, 77, 45 };
            int value = -1;
            bool expected = false;
            bool actual;
            actual = target.Contains(value);
            Assert.AreEqual(expected, actual);

        }
        [TestMethod()]
        public void ContainsTest()
        {
            ContainsTestHelperTrue<int>();
            ContainsTestHelperFalse<int>();
        }

        /// <summary>
        ///A test for CopyTo
        ///</summary>
        public void CopyToTestHelper<T>()
        {
            SortedList<int> target = new SortedList<int>() { 1, 2, 3, 4, 5, 6, 7 };
            int[] array = new int[target.Count];

            int arrayIndex = 0; 
            target.CopyTo(array, arrayIndex);

            SELAssert.AreCollectionsEqual(array, target);
        }

        [TestMethod()]
        public void CopyToTest()
        {
            CopyToTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for GetEnumerator
        ///</summary>
        public void GetEnumeratorTestHelper<T>()
        {
            
        }

        [TestMethod()]
        public void GetEnumeratorTest()
        {
            SortedList<int> target = new SortedList<int>() { 4, 2, 5, 6, 7, 4 };
            int[] expectedResult = new int[] { 2, 4, 4, 5, 6, 7 };
            int size = target.Count;
             
            IEnumerator<int> actual;
            actual = target.GetEnumerator();

            int counter = 0;
            for (int i = 0; actual.MoveNext() == true; i++ )
            {
                Assert.IsTrue(expectedResult[counter] == actual.Current);
                counter++;
            }
            
            Assert.IsTrue(counter == size);
        }

        /// <summary>
        ///A test for IndexOf
        ///</summary>
        public void IndexOfTestHelperTrue()
        {
            SortedList<int> target = new SortedList<int>() { 3,2,5,6,7,4 };

            int value = 3;
            int expected = 1;

            ISortedListTest.IndexOfTest(target, value, expected);           
        }

        /// <summary>
        ///A test for IndexOf
        ///</summary>
        public void IndexOfTestHelperFalse()
        {
            SortedList<int> target = new SortedList<int>() { 3, 2, 5, 6, 7, 4 };

            int value = 8;
            int expected = -1;

            // Assert that the value "value" doesn't exist in the list.
            ISortedListTest.IndexOfTest(target, value, expected);
        }

        [TestMethod()]
        public void IndexOfTest()
        {
            IndexOfTestHelperTrue();
            IndexOfTestHelperFalse();
        }

        [TestMethod()]
        public void IndexOfTestHelperNull()
        {
            SortedList<string> list = new SortedList<string>();
            ExceptionAssert.Throws<ArgumentException>(() => { list.IndexOf(null); });
        }
 
        [TestMethod()]
        public void InsertRangeTest1()
        {
            SortedList_Accessor<int> list = new SortedList_Accessor<int>() { 1, 2, 3 };
            bool exceptionThrown = false;
            try { list.Insert(-1, 0); }
            catch (ArgumentException) { exceptionThrown = true; }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod()]
        public void InsertRangeTest2()
        {
            SortedList_Accessor<int> list = new SortedList_Accessor<int>() { 1, 2, 3 };
            bool exceptionThrown = false;
            try { list.Insert(54, 0); }
            catch (ArgumentException) { exceptionThrown = true; }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod()]
        [DeploymentItem("SEL.Collections.Generic.dll")]
        public void InsertTest()
        {
            SortedList_Accessor<int> target = new SortedList_Accessor<int>() { 1, 2, 6, 7, 8 };
            int index = 3;
            int value = 5;
            target.Insert(index, value);

            int[] expectedResult = new int[] { 1, 2, 6, 5, 7, 8 };
            // Compare target against expected result
            // Note, the result is not expected to be sorted because the Insert() method is not public
            // and is executed before the sorting step.

            SELAssert.AreCollectionsEqual(target, expectedResult);            
        }

        [TestMethod()]
        [DeploymentItem("SEL.Collections.Generic.dll")]
        public void InsertTestNullCase1()
        {
            SortedList_Accessor<string> target = new SortedList_Accessor<string>() { "A", "B", "C" };
            int index = 2;
            ExceptionAssert.Throws<ArgumentException>(() => { target.Insert(index, null); });
        }

        [TestMethod()]
        [DeploymentItem("SEL.Collections.Generic.dll")]
        public void InsertTestNullCase2()
        {
            SortedList_Accessor<int?> target = new SortedList_Accessor<int?>() { 1, 2, 6, 7, 8 };
            int index = -1;
            bool exceptionThrown = false;
            try
            {
                target.Insert(index, null);
            }
            catch (ArgumentException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod()]
        [DeploymentItem("SEL.Collections.Generic.dll")]
        public void InsertTestNullCase3()
        {
            SortedList_Accessor<int?> target = new SortedList_Accessor<int?>() { 1, 2, 6, 7, 8 };
            int index = 100;
            bool exceptionThrown = false;
            try
            {
                target.Insert(index, 1);
            }
            catch (ArgumentException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        /// <summary>
        ///A test for Remove
        ///</summary>
        public void RemoveTestHelperTrue()
        {
            SortedList<int> target = new SortedList<int>() { 1, 2, 3, 4, 5 };

            int value = 4; 

            bool expected = true; 
            bool actual;
            actual = target.Remove(value);
            Assert.AreEqual(expected, actual);

            // Expected result list
            SortedList<int> expectedResult = new SortedList<int> { 1, 2, 3, 5 };

            SELAssert.AreCollectionsEqual(expectedResult, target);  
        }

        /// <summary>
        ///A test for Remove
        ///</summary>
        public void RemoveTestHelperFalse()
        {
            SortedList<int> target = new SortedList<int>() { 1, 2, 3, 4, 5 };

            int value = 7;

            bool expected = false;
            bool actual;
            actual = target.Remove(value);
            Assert.AreEqual(expected, actual);

            // Expected result list
            SortedList<int> expectedResult = new SortedList<int> { 1, 2, 3, 4, 5 };

            SELAssert.AreCollectionsEqual(expectedResult, target);

        }

        [TestMethod()]
        public void RemoveTest()
        {
            RemoveTestHelperFalse();
            RemoveTestHelperTrue();
        }

         [TestMethod()]
        public void RemoveAtTest1()
        {
            SortedList<int> target = new SortedList<int>() { 5, 4, 3, 2, 1 };
            List<int> removedIndexes = new List<int>() { 0, 3, 4 };
            SortedList<int> expectedResult = new SortedList<int>() { 2, 3 };

            // Make sure that when the elements at indexes 0, 3, and 4 are removed, only 2 and 3 are left.
            ISortedListTest.RemoveAtTestHelper(target, removedIndexes, expectedResult);
        }

         [TestMethod()]
         public void RemoveAtTest2()
         {
             SortedList<int> target = new SortedList<int>() { 5, 4, 3, 2, 1 };
             bool exceptionThrown = false;
             try { target.RemoveAt(-1); }
             catch (ArgumentOutOfRangeException) { exceptionThrown = true; }

             Assert.IsTrue(exceptionThrown == true);
         }

         [TestMethod()]
         public void RemoveAtTest3()
         {
             SortedList<int> target = new SortedList<int>() { 5, 4, 3, 2, 1 };
             bool exceptionThrown = false;
             try { target.RemoveAt(232); }
             catch (ArgumentOutOfRangeException) { exceptionThrown = true; }

             Assert.IsTrue(exceptionThrown == true);
         }

        /// <summary>
        ///A test for System.Collections.IEnumerable.GetEnumerator
        ///</summary>
        public void GetEnumeratorTest1Helper<T>()
        {
            IEnumerable target = new SortedList<int>(){5,2,4,7,4,3,4};
            int[] expected = new int[]{2,3,4,4,4,5,7};
            
            IEnumerator actual;
            actual = target.GetEnumerator();
        
            // Get enumerator and validate function by manually iterating over collection
            for (int i = 0; i < expected.Length; i++)
            {
                actual.MoveNext();
                Assert.IsTrue(((int)(actual.Current) == expected[i]));
            }

            // Make sure we've got to the end of the collection.
            Assert.IsTrue(actual.MoveNext() == false);
        }


        [TestMethod()]
        [DeploymentItem("SEL.Collections.Generic.dll")]
        public void GetEnumeratorTest1()
        {
            GetEnumeratorTest1Helper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Capacity
        ///</summary>
        [TestMethod()]
        public void CapacityTest1()
        {
            SortedList<int> target = new SortedList<int>() { 3, 4, 6, 34, 2, 3, 4, 234 };
            int expected = 443; 
            int actual;
            target.Capacity = expected;
            actual = target.Capacity;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Capacity
        ///</summary>
        [TestMethod()]
        public void CapacityTest2()
        {
            SortedList_Accessor<int> target = new SortedList_Accessor<int>();
            //SortedList<int> target = new SortedList<int>();
            //target.l
            int expected = 0;
            int actual;
            target.Capacity = 0;
            actual = target.Capacity;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Capacity
        ///</summary>
        [TestMethod()]
        public void CapacityTestExceptionCase1()
        {
            SortedList<int> target = new SortedList<int>() { 3, 4, 6, 34, 2, 3, 4, 234 };
            
            bool exceptionThrown = false;
            try { target.Capacity = 2; }
            catch (ArgumentException) { exceptionThrown = true; }

            Assert.IsTrue(exceptionThrown == true);
        }

        /// <summary>
        ///A test for Capacity
        ///</summary>
        [TestMethod()]
        public void CapacityTestHelper3()
        {
            SortedList<int> target = new SortedList<int>();

            target.Capacity = 0;
            Assert.IsTrue(target.Capacity == 0);
        }

        /// <summary>
        ///A test for Count
        ///</summary>
        public void CountTestHelper<T>()
        {
            SortedList<int> target = new SortedList<int>() { 4, 3, 5, 8, 4, 5, 6, 3, 5, 6, 7 };
            // There should be 11 elementets reported by the target.
            int expected = 11;
            
            int actual;
            actual = target.Count;

            Assert.IsTrue(expected == actual);
        }

        [TestMethod()]
        public void CountTest()
        {
            CountTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for IsReadOnly
        ///</summary>
        public void IsReadOnlyTestHelper<T>()
        {
            SortedList<T> target = new SortedList<T>(); 
            bool actual;
            actual = target.IsReadOnly;

            Assert.IsTrue(actual == false);
        }

        [TestMethod()]
        public void IsReadOnlyTest()
        {
            IsReadOnlyTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        public void ItemTestHelper<T>()
        {
            SortedList<int> target = new SortedList<int>() { 4, 6, 8, 10 };
            // Check to make sure that the retreived item at index 2 is 8
            ISortedListTest.ItemTestHelper(target, 2, 8);
        }

        [TestMethod()]
        public void ItemTest()
        {
            ItemTestHelper<GenericParameterHelper>();
        }

        [TestMethod()]
        public void Setter()
        {
            SortedList<int> list = new SortedList<int>() {1, 2, 3};
            list[0] = 2;
            Assert.AreEqual(2, list[0]);
        }


        [TestMethod()]
        public void OutOfBoundsSetterCase1()
        {
            SortedList<int> list = new SortedList<int>() { 1, 2, 3 };
            bool exceptionThrown = false;
            try { list[-1] = 0; }
            catch (ArgumentException) { exceptionThrown = true; }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod()]
        public void OutOfBoundsSetterCase2()
        {
            SortedList<int> list = new SortedList<int>() { 1, 2, 3 };
            bool exceptionThrown = false;
            try { list[5] = 0; }
            catch (ArgumentException) { exceptionThrown = true; }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod()]
        public void OutOfBoundsGetterCase1()
        {
            SortedList<int> list = new SortedList<int>() { 1, 2, 3 };
            bool exceptionThrown = false;
            try { int i = list[-1]; }
            catch (ArgumentException) { exceptionThrown = true; }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod()]
        public void OutOfBoundsGetterCase2()
        {
            SortedList<int> list = new SortedList<int>() { 1, 2, 3 };
            bool exceptionThrown = false;
            try { int i = list[5]; }
            catch (ArgumentException) { exceptionThrown = true; }

            Assert.IsTrue(exceptionThrown);
        }
    }
}
