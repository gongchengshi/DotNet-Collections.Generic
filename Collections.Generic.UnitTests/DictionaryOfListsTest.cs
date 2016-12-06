///////////////////////////////////////////////////////////////////////////////
//  COPYRIGHT (c) 2011 Schweitzer Engineering Laboratories, Pullman, WA
//////////////////////////////////////////////////////////////////////////////

using SEL.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using SEL.UnitTest;

namespace SEL.Collections.Generic.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for DictionaryOfListsTest and is intended
    ///to contain all DictionaryOfListsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DictionaryOfListsTest
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
        ///A test for DictionaryOfLists`2 Constructor
        ///</summary>
        public void DictionaryOfListsConstructorTestHelper<Tkey, Tvalues>()
        {
            DictionaryOfLists<Tkey, Tvalues> target = new DictionaryOfLists<Tkey, Tvalues>();
           
        }

        [TestMethod()]
        public void DictionaryOfListsConstructorTest()
        {
            DictionaryOfListsConstructorTestHelper<GenericParameterHelper, GenericParameterHelper>();
        }

        [TestMethod()]
        public void RemoveTestCornerCase()
        {
            DictionaryOfLists_Accessor<string, int> target = new DictionaryOfLists_Accessor<string, int>();
            target.Add("test", 1);
            target.Remove("test", 1);

            Assert.IsTrue(target.CountItems == 0);

            IEnumerable<int> result = target["test"];
            IEnumerator<int> e = result.GetEnumerator();
            Assert.IsTrue(e.MoveNext() == false);            
        }

        [TestMethod()]
        public void AddAndRemoveTest()
        {
            DictionaryOfLists_Accessor<string, int> target = new DictionaryOfLists_Accessor<string, int>();

            string key1 = "String ME";
            string key2 = "String YOU";
            int item1 = 3;
            int item2 = 3;
            int item3 = 3;

            target.Add(key1, item1);

            // Verify new item is in dictionary
            Assert.IsTrue(target.CountItems == 1);
            Assert.IsTrue(target.CountKeys == 1);

            target.Add(key1, item2);
            target.Add(key2, item3);

            // Loop over list corresponding to first key and verify that there are 2 elements in that list
            IEnumerable<int> e1 = target[key1];
            IEnumerator<int> enumerator = e1.GetEnumerator();
            enumerator.MoveNext();
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.MoveNext() == false);

            // Check key2 list to make sure there is only 1 in that lise
            e1 = target[key2];
            enumerator = e1.GetEnumerator();
            enumerator.MoveNext();

            Assert.IsTrue(enumerator.MoveNext() == false);

            // Now remove Item. This will test happy path
            target.Remove(key1, item1);

            // Remove another item, but this should throw exception
            bool exceptionThrown = false;
            try
            {
                target.Remove("dummy", 3);
            }
            catch
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod()]
        public void ClearTest2()
        {
            // ClearTestHelper<GenericParameterHelper, GenericParameterHelper>();

            DictionaryOfLists<string, int> dic = new DictionaryOfLists<string, int>();
            Assert.IsTrue(dic.CountItems == 0);

            dic.Clear();

            Assert.IsTrue(dic.CountItems == 0);
        }

        [TestMethod()]
        public void ClearTest()
        {
            DictionaryOfLists<string, int> dic = new DictionaryOfLists<string, int>();

            dic.Add("String 1", 1);
            dic.Add("String 2", 2);
            dic.Add("String 3", 3);

            Assert.IsTrue(dic.CountItems == 3);

            dic.Clear();

            Assert.IsTrue(dic.CountItems == 0);
        }

        /// <summary>
        ///A test for GetList
        ///</summary>
        public void GetListTestHelper<Tkey, Tvalues>()
        {
            
        }

        [TestMethod()]
        public void IncludeTestSimple()
        {
            DictionaryOfLists<int, string> target = new DictionaryOfLists<int, string>();
            int key = 4;
            string item = "HelloWorld";
            target.Include(key, item);          

        }

        [TestMethod()]
        public void AllValuesTest()
        {
            DictionaryOfLists<string, int> target = new DictionaryOfLists<string, int>();

            target.Add("key1", 1);
            target.Add("key1", 2);
            target.Add("key1", 3);
            target.Add("key1", 4);
            target.Add("key2", 10);
            target.Add("key2", 11);
            target.Add("key2", 12);
            target.Add("key2", 13);
            target.Add("key3", 20);

            int[] expectedValues = new int[] { 1, 2, 3, 4, 10, 11, 12, 13, 20 };

            IEnumerable<int> actual;
            actual = target.AllValues;

            IEnumerator<int> listEnumerator = actual.GetEnumerator();

            SEL.UnitTest.SELAssert.AreCollectionsEqual(actual, expectedValues);

        }

        [TestMethod()]
        public void CountItemsTestEasy()
        {
            DictionaryOfLists<int, string> target = new DictionaryOfLists<int, string>();
            target.Add(1, "one");
            target.Add(2, "two");
            target.Add(3, "three");
            target.Add(4, "four");

            int actual;
            int expected = 4;
            actual = target.CountItems;

            Assert.IsTrue(expected == actual);
        }

        [TestMethod()]
        public void CountItemsTestHard()
        {
            DictionaryOfLists<int, string> target = new DictionaryOfLists<int, string>();

            int size = 100000;
            for (int i = 0; i < size; i++)
            {
                target.Add(i, "item:" + i);

                if (i % 2 == 0)
                {
                    target.Add(i, "Extra Item");
                }
            }

            int actual;
            int expected = size + (size / 2);
            actual = target.CountItems;

            Assert.IsTrue(expected == actual);
        }

        [TestMethod()]
        public void CountKeysTest()
        {
            DictionaryOfLists<int, string> target = new DictionaryOfLists<int, string>(); 
            int actual;
            actual = target.CountKeys;

            int size = 100000;
            for (int i = 0; i < size; i++)
            {
                target.Add(i, "item:" + i);

                if (i % 2 == 0)
                {
                    target.Add(i, "Extra Item");
                }
            }

            // There should be exectly "size" number of keys
            Assert.IsTrue(size == target.CountKeys);
        }

        [TestMethod()]
        public void ItemTest()
        {
            DictionaryOfLists<int, string> target = new DictionaryOfLists<int, string>();

            int size = 100000;
            for (int i = 0; i < size; i++)
            {
                target.Add(i, "item:" + i);

                if (i % 2 == 0)
                {
                    target.Add(i, "Extra Item");
                }
            }
            
            // Invoke the accessor on each even number and verify that the IEnumerable list
            // it returns has exactly 2 elements

            for (int i = 0; i < size; i++)
            {
                if (i % 2 == 0)
                {
                    IEnumerable<string> list = target[i];

                    int counter = 0;

                    IEnumerator<string> enumerator = list.GetEnumerator();

                    while (enumerator.MoveNext())
                    {
                        counter++;
                    }

                    Assert.IsTrue(counter == 2);
                }
            }            
        }

        [TestMethod()]
        public void KeysTest()
        {
            DictionaryOfLists<string, int> target = new DictionaryOfLists<string, int>();

            target.Add("key1", 1);
            target.Add("key1", 2);
            target.Add("key1", 3);
            target.Add("key1", 4);
            target.Add("key2", 10);
            target.Add("key2", 11);
            target.Add("key2", 12);
            target.Add("key2", 13);
            target.Add("key3", 20);

            string[] keyArray = new string[] { "key1", "key2", "key3" };

            IEnumerator<string> keysEnumerator = target.Keys.GetEnumerator();

            SELAssert.AreCollectionsEqual(target.Keys, keyArray);

        }

        [TestMethod()]
        public void ValueListsTest()
        {
            DictionaryOfLists<string, int> target = new DictionaryOfLists<string, int>();

            target.Add("key1", 1);
            target.Add("key1", 2);
            target.Add("key1", 3);
            target.Add("key1", 4);
            target.Add("key2", 10);
            target.Add("key2", 11);
            target.Add("key2", 12);
            target.Add("key2", 13);
            target.Add("key3", 20);

            IEnumerator<List<int>> listEnumerator = target.ValueLists.GetEnumerator();

            int[] values = new int[] { 1, 2, 3, 4, 10, 11, 12, 13, 20 };

            int mainCounter = 0;
            while (listEnumerator.MoveNext() == true)
            {
                List<int> currentList = listEnumerator.Current;
                for (int i = 0; i < currentList.Count; i++)
                {
                    Assert.IsTrue(currentList[i] == values[mainCounter]);
                    mainCounter++;
                }

            }

            Assert.IsTrue(listEnumerator.MoveNext() == false);
        }

        [TestMethod]
        public void TestIEnumerable()
        {
            ;
        }
    }
}
