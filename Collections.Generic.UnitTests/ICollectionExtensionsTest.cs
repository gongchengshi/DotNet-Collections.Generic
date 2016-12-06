using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEL.UnitTest;

namespace SEL.Collections.Generic.UnitTests
{
    [TestClass]
    public class ICollectionExtensionsTest
    {
        [TestMethod]
        public void TestUpdateItems()
        {
            var collection = new ObservableList<int>();
            collection.Add(1);
            collection.Add(2);
            int collectionChangedCalls = 0;
            collection.CollectionChanged += (s, e) => collectionChangedCalls++;

            var update1 = new[] {1, 2};
            collection.UpdateItems(update1);
            SELAssert.AreCollectionsEqual(update1, collection);
            Assert.AreEqual(0, collectionChangedCalls);

            var update2 = new[] {1, 2, 3};
            collection.UpdateItems(update2);
            SELAssert.AreCollectionsEqual(update2, collection);
            Assert.AreEqual(1, collectionChangedCalls);

            var update3 = new[] {1, 3};
            collection.UpdateItems(update3);
            SELAssert.AreCollectionsEqual(update3, collection);
            Assert.AreEqual(2, collectionChangedCalls);

            var update4 = new[] {2, 4};
            collection.UpdateItems(update4);
            SELAssert.AreCollectionsEqual(update4, collection);
            Assert.AreEqual(6, collectionChangedCalls);
        }

        [TestMethod]
        public void ContentsEqualTest1()
        {
            var arr1 = new[] {1, 2, 3};
            var arr2 = new[] {1, 2, 3};

            Assert.IsTrue(ICollectionExtensions.ContentsEqual(arr1, arr2));
        }

        [TestMethod]
        public void ContentsEqualTest2()
        {
            var arr1 = new[] {1, 2};
            var arr2 = new[] {1, 2, 3};

            Assert.IsFalse(ICollectionExtensions.ContentsEqual(arr1, arr2));
        }

        [TestMethod]
        public void ContentsEqualTest3()
        {
            var arr1 = new[] {1, 2, 4};
            var arr2 = new[] {1, 2, 3};

            Assert.IsFalse(ICollectionExtensions.ContentsEqual(arr1, arr2));
        }

        [TestMethod]
        public void TestGetDifferences()
        {
            var odds = new List<int> { 1, 3, 5, 7, 9, 11, 13, 15 };
            var primes = new List<int> { 1, 2, 3, 5, 7, 11, 13, 17 };

            var expectedAdded = new List<int> {2, 17};
            var expectedRemoved = new List<int> {9, 15};

            var actual = odds.GetDifferences(primes);

            Assert.IsTrue(expectedAdded.ContentsEqual(actual.Added));
            Assert.IsTrue(expectedRemoved.ContentsEqual(actual.Removed));

            Assert.IsTrue(odds.GetDifferences(odds).NoDifference);
            Assert.IsFalse(odds.GetDifferences(primes).NoDifference);
        }
        
        [TestMethod]
        public void TestGetDifferencesBetweenDictionaries()
        {
            var dict1 = new Dictionary<string, int>
                            {
                                {"1", 1},
                                {"2", 2},
                                {"3", 3},
                                {"5", 5},
                            };

            var dict2 = new Dictionary<string, int>
                            {
                                {"1", 1},
                                {"2", 2},
                                {"3", 2},
                                {"4", 4}
                            };

            var expectedAdded = new Dictionary<string, int>
                                    {
                                        {"3", 2},
                                        {"4", 4}
                                    };

            var expectedRemoved = new Dictionary<string, int>
                                    {
                                        {"3", 3},
                                        {"5", 5}
                                    };

            var actual = dict1.GetDifferences(dict2);

            Assert.IsTrue(expectedAdded.ContentsEqual(actual.Added));
            Assert.IsTrue(expectedRemoved.ContentsEqual(actual.Removed));
        }

        [TestMethod]
        public void TestAddRange()
        {
            ICollection<int> collectionToTest = new Collection<int>();
            var toAdd = new[] {1, 2};
            collectionToTest.AddRange(toAdd);
            CollectionAssert.AreEqual(new List<int>(toAdd), new List<int>(collectionToTest));
        }

        [TestMethod]
        public void TestUnorderedCompare()
        {
            var arr1 = new[] { 1, 2, 3 };
            var arr2 = new[] { 1, 2, 3 };
            var arr3 = new[] { 1, 2, 4 };
            var arr4 = new[] { 1, 2, 3, 4 };

            Assert.IsTrue(arr1.UnorderedCompare(arr2));
            Assert.IsFalse(arr1.UnorderedCompare(arr3));
            Assert.IsFalse(arr1.UnorderedCompare(arr4));
        }
    }
}