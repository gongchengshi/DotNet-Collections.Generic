using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SEL.Collections.Generic.UnitTests
{
    [TestClass]
    public class CompareExtensionsTest
    {
        private static int[] TEST_ARR_1 = new int[] { 4, 5, 6, 7 };
        private static int[] TEST_ARR_2 = new int[] { 4, 5, 6, 7 };
        private static int[] TEST_ARR_3 = new int[] { 1, 2, 3, 4 };
        private static int[] TEST_ARR_4 = new int[] { 2, 3, 4, 5, 6, 7, 8 };

        private Dictionary<int, ICollection<string>> TEST_DIC_1 = new Dictionary<int, ICollection<string>>() { {4, new List<string>{"Four","Five","Six" }},
                                                                                                               {3, new List<string>{"Three", "Four"}},
                                                                                                               {7, new List<string>{"Seven", "Eight", "Nine"}},
                                                                                                               {8, new List<string>{ "Eight", "Nine"}}};

        private Dictionary<int, ICollection<string>> TEST_DIC_2 = new Dictionary<int, ICollection<string>>() { {4, new List<string>{"Four","Five","Six" }},
                                                                                                               {3, new List<string>{"Three", "Four"}},
                                                                                                               {7, new List<string>{"Seven", "Eight", "Nine"}},
                                                                                                               {8, new List<string>{ "Eight", "Nine"}}};

        private Dictionary<int, ICollection<string>> TEST_DIC_3 = new Dictionary<int, ICollection<string>>() { {4, new List<string>{"Four","Five","Six" }},
                                                                                                               {3, new List<string>{"Three", "Four"}},
                                                                                                               {7, new List<string>{"Seven", "Eight", "Nine"}}};

        private Dictionary<int, ICollection<string>> TEST_DIC_4 = new Dictionary<int, ICollection<string>>() { {4, new List<string>{"Four","Five","Six" }},
                                                                                                               {3, new List<string>{"Three", "Four", "Five"}},
                                                                                                               {7, new List<string>{"Seven", "Eight", "Nine"}},
                                                                                                               {8, new List<string>{ "Eight", "Nine"}}};
        private Dictionary<int, ICollection<string>> TEST_DIC_5 = new Dictionary<int, ICollection<string>>() { {4, new List<string>{"Four","Five","Six" }},
                                                                                                               {3, new List<string>{"Three", "Four"}},
                                                                                                               {7, new List<string>{"Seven", "Eight", "Nine"}},
                                                                                                               {9, new List<string>{ "Eight", "Nine"}}};

        [TestMethod]
        public void TestUnorderdCompareTrue()
        {
            Assert.IsTrue(TEST_ARR_1.UnorderedCompare(TEST_ARR_2));
            Assert.IsFalse(TEST_ARR_1.UnorderedCompare(TEST_ARR_3));
            Assert.IsFalse(TEST_ARR_1.UnorderedCompare(TEST_ARR_4));
        }

        [TestMethod]
        public void TestUnorderdCompareDictionaryTrue()
        {
            Assert.IsTrue(TEST_DIC_2.UnorderedCompare(TEST_DIC_1));
            Assert.IsFalse(TEST_DIC_1.UnorderedCompare(TEST_DIC_3));
            Assert.IsFalse(TEST_DIC_1.UnorderedCompare(TEST_DIC_4));
            Assert.IsFalse(TEST_DIC_1.UnorderedCompare(TEST_DIC_5));
        }

        private Dictionary<int, string> DictionaryA = new Dictionary<int, string> {{1, "A"}, {2, "B"}};
        private Dictionary<int, string> DictionaryB = new Dictionary<int, string> { { 1, "A" }, { 2, "B" } };
        private Dictionary<int, string> DictionaryC = new Dictionary<int, string> { { 1, "A" }, { 2, "C" } };
        private Dictionary<int, string> DictionaryD = new Dictionary<int, string> { { 1, "A" } };

        
        [TestMethod]
        public void TestDictionaryContentsEqual()
        {
            Assert.IsTrue(DictionaryA.ContentsEqual(DictionaryB));
            Assert.IsFalse(DictionaryA.ContentsEqual(DictionaryC));
            Assert.IsFalse(DictionaryA.ContentsEqual(DictionaryD));
        }
    }
}
