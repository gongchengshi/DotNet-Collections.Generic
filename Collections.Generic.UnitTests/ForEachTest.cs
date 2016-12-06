using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SEL.Collections.Generic.UnitTests
{
    [TestClass]
    public class ForEachTest
    {
        private int[] arr1 = new[] { 1, 2, 3 };
        private int[] arr2 = new[] { 2, 3, 4 };
        private int[] arr3 = new[] { 3, 4, 5 };
        private int[] arr4 = new[] { 4, 5, 6 };
        private int[] arr5 = new[] { 5, 6, 7 };
        private int[] arr6 = new[] { 6, 7, 8 };


        [TestMethod]
        public void TestMethod1()
        {
            ForEach.Do(arr1, arr2, (v1,v2) => Assert.AreEqual(v1+1,v2));
            ForEach.Do(arr1, arr2, arr3, (v1, v2, v3) => { Assert.AreEqual(v1 + 1, v2); Assert.AreEqual(v1 + 2, v3); });
            ForEach.Do(arr1, arr2, arr3, arr4, (v1, v2, v3, v4) =>
            {
                Assert.AreEqual(v1 + 1, v2);
                Assert.AreEqual(v1 + 2, v3);
                Assert.AreEqual(v1 + 3, v4);
            });
            ForEach.Do(arr1, arr2, arr3, arr4, arr5, arr6, (v1, v2, v3, v4, v5, v6) =>
            {
                Assert.AreEqual(v1 + 1, v2);
                Assert.AreEqual(v1 + 2, v3);
                Assert.AreEqual(v1 + 3, v4);
                Assert.AreEqual(v1 + 4, v5);
                Assert.AreEqual(v1 + 5, v6);
            });

        }
    }
}
