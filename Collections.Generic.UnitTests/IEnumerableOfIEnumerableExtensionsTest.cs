using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SEL.Collections.Generic.UnitTests
{
   [TestClass]
   public class IEnumerableOfIEnumerableExtensionsTest
   {
      [TestMethod]
      public void CartesianProductTest()
      {
         var result = new List<List<char>> {new List<char> {'a', 'b'}, new List<char> {'c'}, new List<char> {'d', 'e', 'f'}}.CartesianProduct();

         foreach (var combination in result)
         {
            foreach (var item in combination)
            {
               Trace.Write(item + " ");
            }
            Trace.WriteLine(string.Empty);
         }
      }
   }
}
