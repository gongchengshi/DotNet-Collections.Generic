///////////////////////////////////////////////////////////////////////////////
//  COPYRIGHT (c) 2011 Schweitzer Engineering Laboratories, Pullman, WA
//////////////////////////////////////////////////////////////////////////////

using SEL.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections;
using SEL.UnitTest;

namespace SEL.Collections.Generic.UnitTests
{  
    /// <summary>
    ///This is a test class for ObservableListTest and is intended
    ///to contain all ObservableListTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ObservableListTest
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

        IEnumerable<GenericParameterHelper> InputCollection = new List<GenericParameterHelper> 
        {
            new GenericParameterHelper(),
            new GenericParameterHelper(),
            new GenericParameterHelper(),
            new GenericParameterHelper()
        };

        /// <summary>
        ///A test for ObservableList`1 Constructor
        ///</summary>
        public void ObservableListConstructorTestHelper<T>() where T : new()
        {
            ICollection<T> collection = new List<T>() { new T(), new T(), new T()};
            ObservableList<T> target = new ObservableList<T>(collection);
            SELAssert.AreCollectionsEqual(collection, target);
        }

        [TestMethod()]
        public void ObservableListConstructorTest()
        {
            ObservableListConstructorTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for ObservableList`1 Constructor
        ///</summary>
        public void ObservableListConstructorTest1Helper<T>()
        {
            ObservableList<T> target = new ObservableList<T>();
        }

        [TestMethod()]
        public void ObservableListConstructorTest1()
        {
            ObservableListConstructorTest1Helper<GenericParameterHelper>();
        }

        [TestMethod()]
        public void AddTest()
        {
            ObservableList<int> target = new ObservableList<int>();
            int item = 4;

            for (int i = 0; i < 1000; i++ )
            {
                target.Add(item);
                Assert.IsTrue(target[target.Count - 1] == item);
            }

            // Verify that there are 1000 entries in list
            Assert.IsTrue(target.Count == 1000);

            // Now verify that they all have the value "item" after the fact

            for (int i = 0; i < target.Count; i++)
            {
                Assert.IsTrue(target[i] == item);
            }
        }

        /// <summary>
        /// This method tests the PropertyChanged method when an element is added to an observable list.
        /// </summary>
        [TestMethod()]        
        public void ObservableAddTest()
        {
            ObservableList<int> target = new ObservableList<int>() { 1,2,3,4,5 };
            int count = 0;
            target.PropertyChanged += (sender, args) =>
                {
                    Assert.AreEqual(args.PropertyName, "Count");

                    count = (sender as ObservableList<int>).Count;
                };

            target.Add(10);
            target.Add(11);
            target.Add(12);

            Assert.AreEqual(count, target.Count);
        }

        /// <summary>
        ///A test for AddRange
        ///</summary>
        public void AddRangeTestHelper<T>()
        {
            ObservableList<int> target = new ObservableList<int>();
            IEnumerable<int> collection = new int[] { 5, 3, 6, 4, 7, 3, 2 };
            target.AddRange(new int[0]);
            target.AddRange(collection);
           
            // Verify that the contents of target match collection
            IEnumerator<int> enumerator = collection.GetEnumerator();

            SELAssert.AreCollectionsEqual(collection, target);               
        }

        [TestMethod()]
        public void AddRangeTest()
        {
            AddRangeTestHelper<GenericParameterHelper>();
        }

        [TestMethod()]
        public void ClearTest()
        {
            ObservableList<int> target = new ObservableList<int>() { 1, 2, 3, 4, 5, 6, 7 };
            Assert.IsTrue(target.Count > 0);
            target.Clear();
            Assert.IsTrue(target.Count == 0);   
        }

        /// <summary>
        /// Test simple true case
        /// </summary>
        [TestMethod()]
        public void ContainsTestTrue()
        {
            ObservableList<int> target = new ObservableList<int>() { 1, 2, 3, 4, 5 };
            int item = 4;
            bool expected = true;
            bool actual;
            actual = target.Contains(item);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test simple false case
        /// </summary>
        [TestMethod()]
        public void ContainsTestFalse()
        {
            ObservableList<int> target = new ObservableList<int>() { 1, 2, 3, 4, 5 };
            int item = 0;
            bool expected = false;
            bool actual;
            actual = target.Contains(item);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test true case for large list
        /// </summary>
        [TestMethod()]
        public void ContainsTestTrueHard()
        {
            ObservableList<int> target = new ObservableList<int>();

            for (int i = 0; i < 10000; i++)
            {
                target.Add(i);
            }
            int item = 3345;
            bool expected = true;
            bool actual;
            actual = target.Contains(item);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test false case for large list
        /// </summary>
        [TestMethod()]
        public void ContainsTestFalseHard()
        {
            ObservableList<int> target = new ObservableList<int>();

            for (int i = 0; i < 10000; i++)
            {
                target.Add(i);
            }
            int item = 10001;
            bool expected = false;
            bool actual;
            actual = target.Contains(item);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CopyToTest()
        {
            ObservableList<int> target = new ObservableList<int>();

            for (int i = 0; i < 10000; i++)
            {
                target.Add(i);
            }

            int[] array = new int[target.Count]; 
            int arrayIndex = 0; 
            target.CopyTo(array, arrayIndex);

            // Verify that array has same contents as target

            SELAssert.AreCollectionsEqual(target, array);            
        }

        [TestMethod()]
        public void CopyToTestEmptyList()
        {
            ObservableList<int> target = new ObservableList<int>();

            int[] array = new int[target.Count];
            int arrayIndex = 0;
            target.CopyTo(array, arrayIndex);

            // Verify that array has same contents as target
            SELAssert.AreCollectionsEqual(target, array);        
        }

        [TestMethod()]
        public void CopyToTestSingletonList()
        {
            ObservableList<int> target = new ObservableList<int>();
            target.Add(3);
            int[] array = new int[target.Count];
            int arrayIndex = 0;
            target.CopyTo(array, arrayIndex);

            SELAssert.AreCollectionsEqual(target, array);
        }

        [TestMethod()]
        public void GetEnumeratorTest()
        {
            ObservableList<int> target = new ObservableList<int>();

            for (int i = 0; i < 10000; i++)
            {
                target.Add(i * 2);
            }            
            IEnumerator<int> actual = target.GetEnumerator();
            // Compare items returned by the enumerator with the original array

            for (int i = 0; i < target.Count; i++)
            {
                actual.MoveNext();
                Assert.IsTrue(i * 2 == actual.Current);
            }            
        }

        [TestMethod()]
        public void IndexOfTestTrueCase()
        {
            ObservableList<int> target = new ObservableList<int>() { 3, 4, 2, 5, 6, 4, 3, 6, 7 };
            int item = 4;

            // The first instance of the value 4 in the list is at index 1.
            int expected = 1;
            int actual;
            actual = target.IndexOf(item);
            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void IndexOfTestFalseCase()
        {
            ObservableList<int> target = new ObservableList<int>() { 3, 4, 2, 5, 6, 4, 3, 6, 7 };
            int item = 10;

            // The first instance of the value 4 in the list is at index 1.
            int expected = -1;
            int actual;
            actual = target.IndexOf(item);
            Assert.AreEqual(expected, actual);
        }

        // Make sure insertion operator works properly
        [TestMethod()]
        public void InsertTestRegular()
        {
            ObservableList<int> target = new ObservableList<int>() { 0, 4, 2, 6, 4, 8, 6, 6, 7, 5, 9 };
            int index = 7;
            int item = 4;
            target.Insert(index, item);

            Assert.IsTrue(target[index] == item);

            ObservableList<int> expectedResultList = new ObservableList<int>() { 0, 4, 2, 6, 4, 8, 6, 4, 6, 7, 5, 9 };
            SELAssert.AreCollectionsEqual(expectedResultList, target);
        }

        // Make sure insertion operator works properly
        [TestMethod()]
        public void InsertTestZeroCase()
        {
            ObservableList<int> target = new ObservableList<int>() { 0, 4, 2, 6, 4, 8, 6, 6, 7, 5, 9 };
            int index = 0;
            int item = 4;
            target.Insert(index, item);

            Assert.IsTrue(target[index] == item);

            ObservableList<int> expectedResultList = new ObservableList<int>() { 4, 0, 4, 2, 6, 4, 8, 6, 6, 7, 5, 9 };

            SELAssert.AreCollectionsEqual(target, expectedResultList);           
        }

        // Make sure insertion operator works properly
        [TestMethod()]
        public void InsertTestNCase()
        {
            ObservableList<int> target = new ObservableList<int>() { 0, 4, 2, 6, 4, 8, 6, 6, 7, 5, 9 };
            int index = target.Count;
            int item = 4;
            target.Insert(index, item);

            Assert.IsTrue(target[index] == item);

            ObservableList<int> expectedResultList = new ObservableList<int>() { 0, 4, 2, 6, 4, 8, 6, 6, 7, 5, 9, 4 };

            SELAssert.AreCollectionsEqual(expectedResultList, target);
        }
 
        [TestMethod()]
        public void InsertRangeTestZeroCase()
        {
            ObservableList<int> target = new ObservableList<int>() { 1, 2, 3, 4, 5, 6, 7 };
            int index = 0;
            IEnumerable<int> collection = new int[] { 10, 11, 12 };
            target.InsertRange(index, collection);

            ObservableList<int> expectedResultList = new ObservableList<int>() { 10, 11, 12, 1, 2, 3, 4, 5, 6, 7 };

            SELAssert.AreCollectionsEqual(expectedResultList, target);
        }

        [TestMethod()]
        public void InsertRangeTestNCase()
        {
            ObservableList<int> target = new ObservableList<int>() { 1, 2, 3, 4, 5, 6, 7 };
            int index = target.Count;
            IEnumerable<int> collection = new int[] { 10, 11, 12 };
            target.InsertRange(index, collection);

            ObservableList<int> expectedResultList = new ObservableList<int>() { 1, 2, 3, 4, 5, 6, 7, 10, 11, 12 };
            SELAssert.AreCollectionsEqual(expectedResultList, target);
                  
        }

        [TestMethod()]
        [DeploymentItem("SEL.Collections.Generic.dll")]
        public void CollectionChangedEventTest()
        {
            ObservableList<int> target = new ObservableList<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            int eventCounter = 0;
            target.CollectionChanged += delegate(object sender, NotifyCollectionChangedEventArgs args)
            {
                eventCounter++;
            };

            target.Add(6);
            Assert.IsTrue(eventCounter == 1);
            target.Insert(2, 1);
            Assert.IsTrue(eventCounter == 2);
            target.RemoveAt(4);
            Assert.IsTrue(eventCounter == 3);
            target.RemoveRange(4, 3);
            Assert.IsTrue(eventCounter == 4);

            target.SuspendCollectionChangeNotification();
            target.Add(34);
            Assert.IsTrue(eventCounter == 4);

            // The call to this method by design is supposed
            // to trogger the callback
            target.ResumeCollectionChangeNotification();
            Assert.IsTrue(eventCounter == 5);

            target.Add(34);
            Assert.IsTrue(eventCounter == 6);

            target.InsertRange(3, new int[] { 3, 4, 5, 6, 7 });
            Assert.IsTrue(eventCounter == 7);
            
            target.Clear();
            Assert.IsTrue(eventCounter == 8);
        }

        [TestMethod()]
        public void RemoveTest()
        {
            ObservableList<int> target = new ObservableList<int>() { 1, 2, 3, 4, 5, 6 };
            int item = 4;

            bool expected = true;
            bool actual;
            actual = target.Remove(item);

            Assert.AreEqual(expected, actual);

            ObservableList<int> expectedResult = new ObservableList<int>() { 1, 2, 3, 5, 6 };
            Assert.IsTrue(expectedResult.Count == target.Count);

            SELAssert.AreCollectionsEqual(expectedResult, target);

            Assert.IsFalse(target.Remove(-1));
        }


        [TestMethod()]
        public void RemoveAllTestOdds()
        {
            ObservableList<int> target = new ObservableList<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            target.RemoveAll(new Predicate<int>(IdentifyOddNums));

            // The result should only include event numbers
            int[] expectedResult = new int[] { 2, 4, 6, 8, 10 };

            SELAssert.AreCollectionsEqual(expectedResult, target);
        }

        [TestMethod()]
        public void RemoveAllPrimesTest()
        {
            ObservableList<int> target = new ObservableList<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            target.RemoveAll(new Predicate<int>(IsPrime));

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
        public void RemoveAtTest()
        {
            ObservableList<int> target = new ObservableList<int>() { 1, 2, 3, 4, 5, 6 };
            int item = 4;
            target.RemoveAt(item);
            
            ObservableList<int> expectedResult = new ObservableList<int>() { 1, 2, 3, 4, 6 };
            SELAssert.AreCollectionsEqual(expectedResult, target);
        }

        [TestMethod()]
        public void RemoveRangeTestBasic()
        {
            ObservableList<int> target = new ObservableList<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int index = 3;
            int count = 5;
            target.RemoveRange(index, count);

            int[] expectedResult = new int[] { 1, 2, 3, 9, 10 };

            SELAssert.AreCollectionsEqual(expectedResult, target);
        }

        [TestMethod()]
        public void RemoveRangeTestZeroCase()
        {
            ObservableList<int> target = new ObservableList<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int index = 0;
            int count = 5;
            target.RemoveRange(index, count);

            int[] expectedResult = new int[] { 6, 7, 8, 9, 10 };
            SELAssert.AreCollectionsEqual(expectedResult, target);
        }

        [TestMethod()]
        public void RemoveRangeTestNCase()
        {
            ObservableList<int> target = new ObservableList<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int index = 5;
            int count = 5;
            target.RemoveRange(index, count);

            int[] expectedResult = new int[] { 1, 2, 3, 4, 5 };

            SELAssert.AreCollectionsEqual(expectedResult, target);
        }

        [TestMethod()]
        public void RemoveRangeTestNoChangeCase()
        {
            ObservableList<int> target = new ObservableList<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int index = 5;
            int count = 0;
            target.RemoveRange(index, count);

            int[] expectedResult = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };            SELAssert.AreCollectionsEqual(expectedResult, target);

        }

        [TestMethod()]
        public void ResumeCollectionChangeNotificationTest()
        {
            // This has essentially already be tested in this.SuspendCollectionChangeNotificationTest(), so just for fun, let's call it.
            this.SuspendCollectionChangeNotificationTest();
        }

        /// <summary>
        ///A test for Sort
        ///</summary>
        public void SortTestHelper<T>()
        {
            ObservableList<int> target = new ObservableList<int>() { 5, 1, 3, 2, 4 };
            target.Sort();

            int[] expected = new int[] { 1, 2, 3, 4, 5 };

            SELAssert.AreCollectionsEqual(expected, target);
        }

        [TestMethod()]
        public void SortTest()
        {
            SortTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Sort
        ///</summary>
        public void SortTest1Helper<T>()
        {
            ObservableList<int> target = new ObservableList<int>() { 5, 1, 3, 2, 4 }; 
            target.Sort();

            // Expected result. A list with the elements sorted.
            int[] expected = new int[] { 1, 2, 3, 4, 5 };

            SELAssert.AreCollectionsEqual(expected, target);
        }

        private class NewSort : IComparer<int>
        {
            public int Compare(int a, int b)
            {
                if (a < b)
                    return 1;
                if (a > b)
                    return -1;
                else
                    return 0;
            }
        }

        /// <summary>
        ///A test for a generic sort method
        ///</summary>
        ///
        [TestMethod()]
        public void SortTest1Generic()
        {
            ObservableList<int> target = new ObservableList<int>() { 5, 1, 3, 2, 4 };

            // We want to sort in reverse order
            target.Sort(new NewSort());

            // Expected result. A list with the elements sorted.
            int[] expected = new int[] { 5, 4, 3, 2, 1 };

            SELAssert.AreCollectionsEqual(expected, target);            
        }


        [TestMethod()]
        public void SortTest1()
        {
            SortTest1Helper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Sort
        ///</summary>
        public void SortTest2Helper<T>()
        {
            ObservableList<T> target = new ObservableList<T>(); 
            int index = 0; 
            int count = 0; 
            IComparer<T> comparer = null; 
            target.Sort(index, count, comparer);
            
        }

        [TestMethod()]
        public void SortTest2()
        {
            SortTest2Helper<GenericParameterHelper>();
        }

        [TestMethod()]
        public void SuspendCollectionChangeNotificationTest()
        {
            ObservableList<int> target = new ObservableList<int>() { 1, 2, 3, 4, 5 };
            int eventCounter = 0;
            target.CollectionChanged += delegate(object sender, NotifyCollectionChangedEventArgs args)
            {
                eventCounter++;
            };
            
            target.SuspendCollectionChangeNotification();
            Assert.IsTrue(eventCounter == 0);
            // By design, the event should be triggered when the resume method is called.
            target.ResumeCollectionChangeNotification();
            Assert.IsTrue(eventCounter == 1);
        }

        /// <summary>
        /// This test confirms that the enumerator returned iterates over the list
        /// </summary>
        [TestMethod()]
        [DeploymentItem("SEL.Collections.Generic.dll")]
        public void GetEnumeratorTest1()
        {
            IEnumerable target = new ObservableList<int>() { 3, 5, 2, 7, 4, 5, 6 };

            IEnumerator actual;
            actual = target.GetEnumerator();

            int counter = 0;
            // Iterate over enumerator
            while (actual.MoveNext() == true)
            {
                counter++;
            }

            Assert.IsTrue(counter > 0);
        }

        [TestMethod()]
        public void CountTest()
        {

            ObservableList<int> target = new ObservableList<int>(); // TODO: Initialize to an appropriate value
            int size = 3847283;
            for (int i = 0; i < size; i++)
            {
                target.Add(i);
            }

            int actual;
            actual = target.Count;
            Assert.IsTrue(size == actual);
        }

        [TestMethod()]
        public void IsReadOnlyTest()
        {
            ObservableList<int> target = new ObservableList<int>(); 
            bool actual;
            bool expected = false;
            actual = target.IsReadOnly;

            Assert.IsTrue(actual == expected);
        }

        [TestMethod()]
        public void ItemTestEasy()
        {
            ObservableList<int> target = new ObservableList<int>() { 1, 2, 3, 4, 5, 6 }; 
            int index = 0;
            int expected = 1;
            int actual;
            target[index] = expected;
            actual = target[index];
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ItemTestHard()
        {
            ObservableList<int> target = new ObservableList<int>();

            int size = 100000;

            for (int i = size-1; i >= 0; i--)
            {
                target.Add(i);
            }

            int index = size/3;
            int expected = size - size/3;
            int actual;
            target[index] = expected;
            actual = target[index];
            Assert.AreEqual(expected, actual);
        }
    }
}
