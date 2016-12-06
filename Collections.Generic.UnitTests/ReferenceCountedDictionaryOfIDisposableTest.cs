using SEL.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using System.IO;
namespace SEL.Collections.Generic.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for ReferenceCountedDictionaryOfIDisposableTest and is intended
    ///to contain all ReferenceCountedDictionaryOfIDisposableTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ReferenceCountedDictionaryOfIDisposableTest
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
        public void ReferenceCountedDictionaryOfIDisposableConstructorTest()
        {
             ReferenceCountedDictionaryOfIDisposable<int, Stream> dic = new ReferenceCountedDictionaryOfIDisposable<int, Stream>();

        }

        [TestMethod()]
        public void RemoveTest()
        {
            ReferenceCountedDictionaryOfIDisposable<int, Stream> dic = new ReferenceCountedDictionaryOfIDisposable<int, Stream>();

            dic.Add(1, new MemoryStream());
            dic.Remove(1);

            Assert.IsTrue(dic.Count == 0);
        }

        [TestMethod()]
        public void RemoveTest2()
        {
            ReferenceCountedDictionaryOfIDisposable<int, Stream> dic = new ReferenceCountedDictionaryOfIDisposable<int, Stream>();

            dic.Add(1, new MemoryStream());
            dic.Remove(2);

            Assert.IsTrue(dic.Count == 1);
        }
    }
}
