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
    ///This is a test class for ReferenceCountedDictionaryTest and is intended
    ///to contain all ReferenceCountedDictionaryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ReferenceCountedDictionaryTest
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
        ///A test for ReferenceCountedDictionary`2 Constructor
        ///</summary>
        public void ReferenceCountedDictionaryConstructorTestHelper<TKey, TValue>()
        {
            ReferenceCountedDictionary<TKey, TValue> target = new ReferenceCountedDictionary<TKey, TValue>();            
        }

        [TestMethod()]
        public void ReferenceCountedDictionaryConstructorTest()
        {
            ReferenceCountedDictionaryConstructorTestHelper<GenericParameterHelper, GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        public void AddTestHelper()
        {
            int key = 45;
            string value = "test value";

            ReferenceCountedDictionary<int, string> target = new ReferenceCountedDictionary<int, string>(); // TODO: Initialize to an appropriate value
            KeyValuePair<int, string> item = new KeyValuePair<int, string>(key, value);
            target.Add(item);

            Assert.IsTrue(target[key] == "test value");

            target.Remove(key);
            bool exceptionThrown = false;
            try
            {
                Assert.IsTrue(target[key] == null);
            }
            catch (KeyNotFoundException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod()]
        public void AddTest()
        {
            AddTestHelper();
        }

    
        [TestMethod()]
        public void AddTest1Easy()
        {
            ReferenceCountedDictionary<string, int> target = new ReferenceCountedDictionary<string, int>(); 
            string key = "Key 1"; 
            int value = 12345; 

            target.Add(key, value);

            Assert.IsTrue(target[key] == value);
        }

        [TestMethod()]
        public void AddWithResultTestEasy()
        {
            ReferenceCountedDictionary<string, int> target = new ReferenceCountedDictionary<string, int>(); 
            string key = "test key";
            int value1 = 42;
            bool expected = true;
            bool actual;

            actual = target.AddWithResult(key, value1);
            Assert.AreEqual(expected, actual);

            // Add another value with the same key and verify that the result is false because a duplicate value is inserted
            // for that key.
            actual = target.AddWithResult(key, value1);
            Assert.IsTrue(actual == false);
        }

        [TestMethod()]
        public void AddWithResultTestHard()
        {
            ReferenceCountedDictionary<string, int> target = new ReferenceCountedDictionary<string, int>();        

            for (int i = 0; i < 10000; i++)
            {
                bool result = target.AddWithResult("key" + i, i);
                Assert.IsTrue(result == true);
                Assert.IsTrue(target["key" + i] == i);
            }

            for (int i = 0; i < target.Count; i++)
            {
                Assert.IsTrue(target["key" + i] == i);
            }
        }

        [TestMethod()]
        public void ClearTest()
        {            
            ReferenceCountedDictionary<string, int> target = new ReferenceCountedDictionary<string, int>();

            for (int i = 0; i < 10000; i++)
            {
                target.Add("key" + i, i);
                Assert.IsTrue(target["key" + i] == i);
            }

            for (int i = 0; i < target.Count; i++)
            {
                Assert.IsTrue(target["key" + i] == i);
            }

            Assert.IsTrue(target.Count > 0);
            target.Clear();
            Assert.IsTrue(target.Count == 0);
            
        }

        [TestMethod()]
        public void ContainsTestTrue()
        {
            ReferenceCountedDictionary<string, int> target = new ReferenceCountedDictionary<string, int>();

            for (int i = 0; i < 10000; i++)
            {
                target.Add("key" + i, 1);
            }

            // Choose arbitrary key to look for in the dictionary.
            string key = "key331";
            bool expected = true; 
            bool actual;

            actual = target.Contains(key);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void ContainsTestFalse()
        {
            ReferenceCountedDictionary<string, int> target = new ReferenceCountedDictionary<string, int>();

            for (int i = 0; i < 10000; i++)
            {
                target.Add("key" + i, 1);
            }

            // Choose arbitrary key to look for in the dictionary.
            string key = "key123456";
            bool expected = false;
            bool actual;

            actual = target.Contains(key);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ContainsKeyTestEasy()
        {
            ReferenceCountedDictionary<string, int> target = new ReferenceCountedDictionary<string, int>();
            string key = "My key";

            target.Add(key, 1);
            bool expected = true; 
            bool actual;

            actual = target.ContainsKey(key);
            Assert.AreEqual(expected, actual);            
        }

        [TestMethod()]
        public void ContainsKeyTest2()
        {
            ReferenceCountedDictionary<string, int> target = new ReferenceCountedDictionary<string, int>();
            string key = "My key";
            target.Add(key, 1);

            bool exceptionThrown = false;
            try
            {
                target.Add(key, 2);
            }
            catch (InvalidOperationException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown == true);

            bool expected = true;
            bool actual;

            actual = target.ContainsKey(key);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ContainsTest4()
        {
            ReferenceCountedDictionary<string, int> target = new ReferenceCountedDictionary<string, int>();
            bool exceptionThrown = false;
            try
            {
                target.Contains(null);
            }
            catch (ArgumentException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public void ContainsTest5()
        {
            ReferenceCountedDictionary<string, int> target = new ReferenceCountedDictionary<string, int>();
            bool exceptionThrown = false;
            try
            {
                target.Contains('b');
            }
            catch (ArgumentException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod()]
        public void GetEnumeratorTest()
        {
            ReferenceCountedDictionary<string, int> target = new ReferenceCountedDictionary<string, int>(); // TODO: Initialize to an appropriate value

            int size = 1000;
            string[] keys = new string[size];
            int[] values = new int[size];

            for (int i = 0; i < size; i++)
            {
                keys[i] = "Key" + i;
                values[i] = i;

                target.Add(keys[i], values[i]);
            }

            IEnumerator<KeyValuePair<string, int>> actual;
            actual = target.GetEnumerator();

            for (int i = 0; i < target.Count; i++)
            {
                actual.MoveNext();
                KeyValuePair<string, int> current = actual.Current;

                Assert.IsTrue(current.Key == keys[i]);
                Assert.IsTrue(current.Value == values[i]);
            }
        }


        [TestMethod()]
        public void RemoveTest()
        {
            ReferenceCountedDictionary<int, string> target = new ReferenceCountedDictionary<int, string>(); 

            target.Add(new KeyValuePair<int, string>(1, "value1"));
            target.Add(new KeyValuePair<int, string>(2, "value2"));
            target.Add(new KeyValuePair<int, string>(3, "value3"));
            target.Add(new KeyValuePair<int, string>(4, "value4"));

            bool actual;
            actual = target.Remove(3);

            Assert.IsTrue(target.Count == 3);

            bool exceptionThrown = false;
            try
            {
                Assert.IsTrue(target[3] == null);
            }
            catch(KeyNotFoundException){
                exceptionThrown  = true;
            }
            Assert.IsTrue(exceptionThrown == true);
        }

        [TestMethod()]
        public void RemoveTest2()
        {
            ReferenceCountedDictionary<int, string> target = new ReferenceCountedDictionary<int, string>();

            target.Add(new KeyValuePair<int, string>(0, "value1"));

            bool actual;
            actual = target.Remove(0);
            Assert.IsTrue(actual == true);

            actual = target.Remove(0);
            Assert.IsTrue(actual == false);
        }

        [TestMethod()]
        [DeploymentItem("SEL.Collections.Generic.dll")]
        public void ContainsTest1()
        {
            ICollection<KeyValuePair<string, int>> target = new ReferenceCountedDictionary<string, int>();

            string key = "MyKey";
            int value = 100;
            KeyValuePair<string, int> item = new KeyValuePair<string, int>(key, value);

            target.Add(item);

            bool expected = true;
            bool actual;
            bool exceptionThrown = false;
            try
            {
                actual = target.Contains(item);
                Assert.AreEqual(expected, actual);
            }
            catch (NotImplementedException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown == true);            

        }

  
        [TestMethod()]
        [DeploymentItem("SEL.Collections.Generic.dll")]
        public void CopyToTest()
        {
            ICollection<KeyValuePair<string, int>> target = new ReferenceCountedDictionary<string, int>();
            KeyValuePair<string, int>[] array = new KeyValuePair<string, int>[target.Count];
            int arrayIndex = 0;
            bool exceptionThrown = false;

            try
            {
                target.CopyTo(array, arrayIndex);
            }
            catch (NotImplementedException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown == true);   
        }

        [TestMethod()]
        [DeploymentItem("SEL.Collections.Generic.dll")]
        public void RemoveTest4()
        {
            ReferenceCountedDictionary<int, string> target = new ReferenceCountedDictionary<int, string>();

            target.AddWithResult(1, "one");
            target.AddWithResult(1, "one");

            bool actual = true;
            bool expected = false;
            actual = target.Remove(1);

            Assert.IsTrue(expected == actual);

            // Remove final element, and make sure result of Remove is true
            actual = target.Remove(1);

            Assert.IsTrue(actual);
        }
        [TestMethod()]
        [DeploymentItem("SEL.Collections.Generic.dll")]
        public void RemoveTest1()
        {
            ICollection<KeyValuePair<int, string>> target = new ReferenceCountedDictionary<int, string>(); 
            KeyValuePair<int, string> item = new KeyValuePair<int, string>();            
            bool actual;
            bool exceptionThrown = false;

            try
            {
                actual = target.Remove(item);
            }
            catch (NotImplementedException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown == true);   
        }

        [TestMethod()]
        [DeploymentItem("SEL.Collections.Generic.dll")]
        public void GetEnumeratorTest1()
        {
            IEnumerable target = new ReferenceCountedDictionary<string, int>();
            IEnumerator actual;
            actual = target.GetEnumerator();

            Assert.IsTrue(actual != null); 
        }

        [TestMethod()]
        public void TryGetValueTestTrue()
        {
            ReferenceCountedDictionary<int, string> target = new ReferenceCountedDictionary<int, string>(); // TODO: Initialize to an appropriate value

            target.Add(new KeyValuePair<int, string>(1, "value1"));
            target.Add(new KeyValuePair<int, string>(2, "value2"));
            target.Add(new KeyValuePair<int, string>(3, "value3"));
            target.Add(new KeyValuePair<int, string>(4, "value4"));

            int key = 3;
            string value = "value3";

            bool expected = true;
            bool actual;

            string valueExpected = value;
            actual = target.TryGetValue(key, out value);

            Assert.AreEqual(valueExpected, value);
            Assert.AreEqual(expected, actual);    
        }

        [TestMethod()]
        public void TryGetValueTestFalse()
        {
            ReferenceCountedDictionary<int, string> target = new ReferenceCountedDictionary<int, string>(); // TODO: Initialize to an appropriate value

            target.Add(new KeyValuePair<int, string>(1, "value1"));
            target.Add(new KeyValuePair<int, string>(2, "value2"));
            target.Add(new KeyValuePair<int, string>(3, "value3"));
            target.Add(new KeyValuePair<int, string>(4, "value4"));

            int key = 5;
            string value = "value3";

            bool expected = false;
            bool actual;

            string valueExpected = default(string);
            // If the search fails, the implementation of the method sets value to default(T).
            // In the case of strings, this is null
            actual = target.TryGetValue(key, out value);

            Assert.AreEqual(valueExpected, value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CountTest()
        {
            ReferenceCountedDictionary<string, int> target = new ReferenceCountedDictionary<string, int>(); // TODO: Initialize to an appropriate value

            int size = 102394;
            for (int i = 0; i < size; i++)
            {
                target.Add("key" + i, i);

                // Accounts for reference counts > 1
                if (i % 5 == 0)
                {
                    target.Add("keyB" + i, i);
                }
            }

            int actual;
            actual = target.Count;
            Assert.IsTrue( Math.Round(size*1.2) == target.Count);
        }


        [TestMethod()]
        public void ItemTest()
        {            
            ReferenceCountedDictionary<string, int> target = new ReferenceCountedDictionary<string, int>();
            string key = "Key";
            int expected = 4;
            int actual;

            target[key] = expected;
            actual = target[key];
            Assert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void KeysTest()
        {
            ReferenceCountedDictionary<int, string> target = new ReferenceCountedDictionary<int, string>();
            int size = 1000;

            for (int i = 0; i < size; i++)
            {
                target.Add(new KeyValuePair<int, string>(i, ""+i));
            }

            ICollection<int> actual;
            actual = target.Keys;
            IEnumerator<int> enumerator = actual.GetEnumerator();
            for (int i = 0; i < size; i++)
            {
                Assert.IsTrue(enumerator.MoveNext() == true);
                Assert.IsTrue(enumerator.Current == i);
            }

            Assert.IsTrue(enumerator.MoveNext() == false);
        }

        /// <summary>
        ///A test for System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.IsReadOnly
        ///</summary>
        public void IsReadOnlyTestHelper<TKey, TValue>()
        {
            ICollection<KeyValuePair<TKey, TValue>> target = new ReferenceCountedDictionary<TKey, TValue>();
            bool actual;
            actual = target.IsReadOnly;

            Assert.IsTrue(actual == false);
        }

        [TestMethod()]
        [DeploymentItem("SEL.Collections.Generic.dll")]
        public void IsReadOnlyTest()
        {
            IsReadOnlyTestHelper<GenericParameterHelper, GenericParameterHelper>();
        }

        [TestMethod()]
        public void EnumeratorTest()
        {
            ReferenceCountedDictionary<int, string> dic = new ReferenceCountedDictionary<int, string>();

            List<KeyValuePair<int, string>> arr = new List<KeyValuePair<int, string>>();
            arr.Add(new KeyValuePair<int, string>(1, "one"));
            arr.Add(new KeyValuePair<int, string>(2, "two"));
            arr.Add(new KeyValuePair<int, string>(3, "three"));

            dic.Add(arr[0]);
            dic.Add(arr[1]);
            dic.Add(arr[2]);

            // Get Enumerator
            IEnumerator<KeyValuePair<int, string>> enumerator = dic.GetEnumerator();

            for (int i = 0; i < dic.Count && enumerator.MoveNext(); i++)
            {
                Assert.IsTrue(arr[i].Key == ((KeyValuePair<int, string>)((((IEnumerator)(enumerator)).Current))).Key);
                Assert.IsTrue(arr[i].Value == ((KeyValuePair<int, string>)((((IEnumerator)(enumerator)).Current))).Value);
            }

            enumerator.Dispose();
        }

        [TestMethod()]
        public void ValuesTest()
        {
            ReferenceCountedDictionary<int, string> target = new ReferenceCountedDictionary<int, string>(); 
            ICollection<string> actual;    
            int size = 1000;
            for (int i = 0; i < size; i++)
            {
                target.Add(new KeyValuePair<int, string>(i, "" + i));
            }
            actual = target.Values;

            IEnumerator<string> enumerator = actual.GetEnumerator();
            for (int i = 0; i < size; i++)
            {
                Assert.IsTrue(enumerator.MoveNext() == true);
                Assert.IsTrue(enumerator.Current == "" + i);
            }
        }
    }
}
