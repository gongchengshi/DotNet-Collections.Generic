///////////////////////////////////////////////////////////////////////////////
//  COPYRIGHT (c) 2011 Schweitzer Engineering Laboratories, Pullman, WA
//////////////////////////////////////////////////////////////////////////////

using SEL.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace SEL.Collections.Generic.UnitTests
{


    /// <summary>
    ///This is a test class for ObservableSortedListTest and is intended
    ///to contain all ObservableSortedListTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ObservableSortedListTest
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
        public void ObservableSortedListConstructorTest()
        {
            IComparer<int> comparer = null;
            ObservableSortedList<int> target = new ObservableSortedList<int>(comparer);
        }


        [TestMethod()]
        public void ObservableSortedListConstructorTest1()
        {
            ObservableSortedList<int> target = new ObservableSortedList<int>();
        }

        [TestMethod()]
        public void ClearTest()
        {
            ObservableSortedList<int> target = new ObservableSortedList<int>() { 4, 6, 2, 7, 8, 9, 1, 2, 3 }; // TODO: Initialize to an appropriate value
            target.Clear();

            Assert.IsTrue(target.Count == 0);

            bool exceptionThrown = false;
            try
            {
                int val = target[0];
            }
            catch
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod()]
        [DeploymentItem("SEL.Collections.Generic.dll")]
        public void InsertTest()
        {
            ObservableSortedList_Accessor<int> target = new ObservableSortedList_Accessor<int>() { 5, 1, 6, 2, 3, 4, };
            int index = 4;
            int value = 0;

            target.Insert(index, value);

            // The 0 will be at the start of the list, so assert that the collection at 0 is 0.
            Assert.IsTrue(target[index] == 0);
        }
        
        [TestMethod()]
        public void RemoveAtTest()
        {
            int valueToRemove = 3;

            ObservableSortedList<int> target = new ObservableSortedList<int>() { 1, 2, valueToRemove, 4, 5 }; 
            int index = 2; 
            target.RemoveAt(index);

            // Make sure that value at index is not valueToRemove
            Assert.IsTrue(target[index] != valueToRemove);
        }

        [TestMethod()]
        public void ItemTest()
        {
            ObservableSortedList<int> target = new ObservableSortedList<int>() { 4, 2, 4, 8, 9, 0 };
            int index = 3;
            int expected = 2063;
            int actual;
            target[index] = expected;
            actual = target[index];

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SuspendCollectionChangeNotificationTest()
        {
            ObservableSortedList<int> target = new ObservableSortedList<int>() { 1, 2, 3, 4, 5 };
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

            target.Add(10);

        }

    }
}
