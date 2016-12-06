﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SEL.Collections.Generic.UnitTests
{
    /// <summary>
    /// Summary description for ComparerTest
    /// </summary>
    [TestClass]
    public class ComparerTest
    {
        public ComparerTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void CompareConstructorTest()
        {
            Comparer<int> test = new Comparer<int>();
        }

        [TestMethod]
        public void TestCompare()
        {
            Comparer<int> test = new Comparer<int>();

            Assert.IsTrue(test.Compare(0, 1) < 0);
            Assert.IsTrue(test.Compare(1, 1) == 0);
            Assert.IsTrue(test.Compare(1, 0) > 0);
        }

        [TestMethod]
        public void TestUsingDefaultIComparer()
        {
            IComparer<int> test = Comparer<int>.Default;

            Assert.IsTrue(test.Compare(0, 1) < 0);
            Assert.IsTrue(test.Compare(1, 1) == 0);
            Assert.IsTrue(test.Compare(1, 0) > 0);
        }

    }
}
