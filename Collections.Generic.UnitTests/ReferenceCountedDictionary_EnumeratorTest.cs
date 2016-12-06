///////////////////////////////////////////////////////////////////////////////
//  COPYRIGHT (c) 2011 Schweitzer Engineering Laboratories, Pullman, WA
//////////////////////////////////////////////////////////////////////////////

using SEL.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections;

namespace SEL.Collections.Generic.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for ReferenceCountedDictionary_EnumeratorTest and is intended
    ///to contain all ReferenceCountedDictionary_EnumeratorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ReferenceCountedDictionary_EnumeratorTest
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

#if UNUSED_CODE

        [TestMethod()]
        public void DisposeTest()
        {
            Dictionary<int, ReferenceCountWrapper<string>> internalDictionary = new Dictionary<int, ReferenceCountWrapper<string>>(); // TODO: Initialize to an appropriate value
            internalDictionary.Add(1, new ReferenceCountWrapper<string>("value1", 0));

            ReferenceCountedDictionary<int, string>.Enumerator target = new ReferenceCountedDictionary<int, string>.Enumerator(internalDictionary); // TODO: Initialize to an appropriate value
            target.Dispose();

            target.MoveNext();
        }
#endif




        /// <summary>
        /// This functionality has been tested in ReferenceCountWrapperTest.cs
        ///</summary>
        #if UNUSED_CODE
        public void MoveNextTestHelper<TKey, TValue>()
        {
            Dictionary<TKey, ReferenceCountWrapper<TValue>> internalDictionary = null; // TODO: Initialize to an appropriate value
            ReferenceCountedDictionary<TKey, TValue>.Enumerator target = new ReferenceCountedDictionary<TKey, TValue>.Enumerator(internalDictionary); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.MoveNext();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void MoveNextTest()
        {
            MoveNextTestHelper<GenericParameterHelper, GenericParameterHelper>();
        }   
#endif
        [TestMethod()]
        public void ResetTest()
        {
            Dictionary<int, ReferenceCountWrapper<string>> internalDictionary = new Dictionary<int, ReferenceCountWrapper<string>>();

            internalDictionary.Add(1, new ReferenceCountWrapper<string>("value1", 0));
            ReferenceCountedDictionary<int, string>.Enumerator target = new ReferenceCountedDictionary<int, string>.Enumerator(internalDictionary);

            Assert.IsTrue(target.MoveNext() == true);
            Assert.IsTrue(target.MoveNext() == false);
            target.Reset();
            Assert.IsTrue(target.MoveNext() == true);
        }

        [TestMethod()]
        public void ResetTest2()
        {
            Dictionary<int, ReferenceCountWrapper<string>> internalDictionary = new Dictionary<int, ReferenceCountWrapper<string>>();
            internalDictionary.Add(0, new ReferenceCountWrapper<string>("value1", 1));
            internalDictionary.Add(1, new ReferenceCountWrapper<string>("value2", 1));
            internalDictionary.Add(2, new ReferenceCountWrapper<string>("value3", 1));

            ReferenceCountedDictionary<int, string>.Enumerator target = new ReferenceCountedDictionary<int, string>.Enumerator(internalDictionary);

            Assert.IsTrue(target.MoveNext() == true);
            Assert.IsTrue(target.Current.Key == 0 && target.Current.Value == "value1");
            Assert.IsTrue(target.MoveNext() == true);
            Assert.IsTrue(target.Current.Key == 1 && target.Current.Value == "value2");
            Assert.IsTrue(target.MoveNext() == true);
            Assert.IsTrue(target.Current.Key == 2 && target.Current.Value == "value3");
            Assert.IsTrue(target.MoveNext() == false);
            target.Reset();

            Assert.IsTrue(target.MoveNext() == true);
            Assert.IsTrue(target.Current.Key == 0 && target.Current.Value == "value1");
        }


        [TestMethod()]
        [DeploymentItem("SEL.Collections.Generic.dll")]
        public void CurrentTest1()
        {
           
        }
    }
}
