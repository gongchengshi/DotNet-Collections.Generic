///////////////////////////////////////////////////////////////////////////////
//  COPYRIGHT (c) 2011 Schweitzer Engineering Laboratories, Pullman, WA
//////////////////////////////////////////////////////////////////////////////

using SEL.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using SEL.UnitTest;

namespace SEL.Collections.Generic.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for SortedList_EnumeratorTest and is intended
    ///to contain all SortedList_EnumeratorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SortedList_EnumeratorTest
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
        /// The Move next method has been sufficiently tested in the SortedListTest.cs file.
        ///</summary>
        ///
#if UNUSED_CODE

        public void MoveNextTestHelper<T>()
        {
            PrivateObject param0 = null; 
            SortedList_Accessor<T>.Enumerator target = new SortedList_Accessor<T>.Enumerator(param0); 
            bool expected = false;
            bool actual;
            actual = target.MoveNext();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        [DeploymentItem("SEL.Collections.Generic.dll")]
        public void MoveNextTest()
        {
            MoveNextTestHelper<GenericParameterHelper>();
        }
#endif


        /// <summary>
        ///A test for Enumerator Constructor
        ///</summary>
        public void SortedList_EnumeratorConstructorTestHelper<T>()
        {
            SortedList<T> collection = new SortedList<T>(); 
            SortedList_Accessor<T>.Enumerator target = new SortedList_Accessor<T>.Enumerator(collection);
        }

        [TestMethod()]
        [DeploymentItem("SEL.Collections.Generic.dll")]
        public void SortedList_EnumeratorConstructorTest()
        {
            SortedList_EnumeratorConstructorTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Dispose
        ///</summary>        
        [TestMethod()]
        public void DisposeTestHelper()
        {
            SortedList<int> sl = new SortedList<int>() { 0, 2, 4, 6 };
            SortedList_Accessor<int>.Enumerator target = new SortedList_Accessor<int>.Enumerator(sl); 
            target.Dispose();
        }


        /// <summary>
        ///A test for ValidateVersion
        ///</summary>        
        [TestMethod()]
        [DeploymentItem("SEL.Collections.Generic.dll")]
        public void ValidateVersionTestHelperException()
        {
            var sl = new SortedList<int>() { 1, 3, 5, 7 };
            var target = new SortedList_Accessor<int>.Enumerator(sl);
            target.MoveNext();
            sl.Add(9);
            ExceptionAssert.Throws<InvalidOperationException>(() => { target.ValidateVersion(); });
        }

        /// <summary>
        ///A test for ValidateVersion
        ///</summary>        
        [TestMethod()]
        [DeploymentItem("SEL.Collections.Generic.dll")]
        public void ValidateVersionTestHelper()
        {
            SortedList<int> sl = new SortedList<int>() { 1, 3, 5, 7 };
            SortedList_Accessor<int>.Enumerator target = new SortedList_Accessor<int>.Enumerator(sl);
            target.ValidateVersion();
        }  

        [TestMethod()]
        public void ResetTest()
        {
            SortedList<int> sl = new SortedList<int>() { 1, 3, 5, 7 };
            SortedList_Accessor<int>.Enumerator enumerator = new SortedList_Accessor<int>.Enumerator(sl);

            ((IEnumerator)(enumerator)).Reset();            
        }

        /// <summary>
        ///A test for Current
        ///</summary>
        [TestMethod()]
        public void CurrentTest()
        {
            SortedList<int> sl = new SortedList<int>() { 8, 6, 4, 2 };
            int[] expectedEnumeratedList = new int[] { 2, 4, 6, 8 };
            SortedList_Accessor<int>.Enumerator target = new SortedList_Accessor<int>.Enumerator(sl);

            int currentIndex = 0;
            target.MoveNext();
            do
            {               
                Assert.IsTrue(target.Current == expectedEnumeratedList[currentIndex++]);
            } while (target.MoveNext() == true);
        }

        /// <summary>
        ///A test for Current
        ///</summary>
        [TestMethod()]
        public void CurrentExceptionCase1Test()
        { 
            
            SortedList<int> sl = new SortedList<int>() { 8, 6, 4, 2 };
            int[] expectedEnumeratedList = new int[] { 2, 4, 6, 8 };
            SortedList_Accessor<int>.Enumerator target = new SortedList_Accessor<int>.Enumerator(sl);

            int currentIndex = 0;
            target.MoveNext();
            target._index = 0;
            bool exceptionThrown = false;
            try
            {
                do
                {
                    Assert.IsTrue((int)((IEnumerator)(target)).Current == expectedEnumeratedList[currentIndex++]);

                } while (target.MoveNext() == true && exceptionThrown == false);
            }
            catch (InvalidOperationException)
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);
        }

        /// <summary>
        ///A test for Current
        ///</summary>
        [TestMethod()]
        public void CurrentExceptionCase2Test()
        {
            SortedList<int> sl = new SortedList<int>() { 8, 6, 4, 2 };
            int[] expectedEnumeratedList = new int[] { 2, 4, 6, 8 };
            SortedList_Accessor<int>.Enumerator target = new SortedList_Accessor<int>.Enumerator(sl);

            int currentIndex = 0;
            target.MoveNext();
            target._index = sl.Count + 1;
            bool exceptionThrown = false;
            try
            {
                do
                {
                    Assert.IsTrue((int)((IEnumerator)(target)).Current == expectedEnumeratedList[currentIndex++]);

                } while (target.MoveNext() == true && exceptionThrown == false);
            }
            catch (InvalidOperationException)
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);
        }

    }
}
