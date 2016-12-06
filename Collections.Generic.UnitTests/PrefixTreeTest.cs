using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SEL.Collections.Generic.UnitTests
{
   [TestClass]
   public class PrefixTreeTest
   {
      [TestMethod]
      public void TestAdd()
      {
         var inputs = new List<string>
         {
            "a.b.c",
            "a.b.c.d",
            "a.b",
            "a.b.c.d.e",
            "a"
         };

         var target = new PrefixTree<string, string>();
         foreach (var input in inputs)
         {
            target.Add(input.Split('.'), input);
         }

         Debugger.Break();
      }
   }
}
