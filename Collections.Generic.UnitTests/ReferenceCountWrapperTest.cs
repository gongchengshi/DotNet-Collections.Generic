///////////////////////////////////////////////////////////////////////////////
//  COPYRIGHT (c) 2011 Schweitzer Engineering Laboratories, Pullman, WA
//////////////////////////////////////////////////////////////////////////////

using SEL.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SEL.Collections.Generic.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for ReferenceCountWrapperTest and is intended
    ///to contain all ReferenceCountWrapperTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ReferenceCountWrapperTest
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
        ///A test for ReferenceCountWrapper`1 Constructor
        ///</summary>
        public void ReferenceCountWrapperConstructorTestHelper<T>()
        {
            T value = default(T); 
            int count = 0;
            ReferenceCountWrapper<T> target = new ReferenceCountWrapper<T>(value, count);
            
        }

        [TestMethod()]
        public void ReferenceCountWrapperConstructorTest()
        {
            ReferenceCountWrapperConstructorTestHelper<GenericParameterHelper>();
        }
    }
}
