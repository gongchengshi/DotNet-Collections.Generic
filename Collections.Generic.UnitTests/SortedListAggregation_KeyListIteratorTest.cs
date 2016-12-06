using SEL.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SEL.UnitTest;

namespace SEL.Collections.Generic.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for SortedListAggregation_KeyListIteratorTest and is intended
    ///to contain all SortedListAggregation_KeyListIteratorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SortedListAggregation_KeyListIteratorTest
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
        public void CurrentNullTest()
        {
            SortedList<string> nullList = new SortedList<string>() { "one", "two", "three" };

            SortedListAggregation<string, string>.KeyListIterator iterator = new SortedListAggregation<ISortedList<string>, string, ISortedList<string>, string>.KeyListIterator(nullList);

            for (int i = 0; i < nullList.Count; i++)
            {
                iterator.MoveNext();
            }

            ExceptionAssert.Throws<System.InvalidOperationException>(() =>
            {
                string current = iterator.Current;
            });
        }


        [TestMethod()]
        public void KeyListIteratorConstructorNullArgTest()
        {
            SortedList<string> nullList = null;
            ExceptionAssert.Throws<System.ArgumentNullException>(() =>
            {
                SortedListAggregation<string, string>.KeyListIterator iterator = new SortedListAggregation<ISortedList<string>, string, ISortedList<string>, string>.KeyListIterator(nullList);
            });
        }
 
        [TestMethod()]
        [DeploymentItem("SEL.Collections.Generic.dll")]
        public void op_IncrementTest()
        {
            SortedList<string> nullList = new SortedList<string>() { "1", "2", "3", "4", "5", "6" };

            SortedListAggregation<string, string>.KeyListIterator iterator = new SortedListAggregation<ISortedList<string>, string, ISortedList<string>, string>.KeyListIterator(nullList);

            for (int i = 0; i < nullList.Count; i++)
            {                
                Assert.IsTrue(nullList[i] == "" + (i+1));
                iterator++;
            }
        }     

  
    }
}
