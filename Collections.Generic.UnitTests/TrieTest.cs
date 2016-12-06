using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SEL.Collections.Generic.UnitTests
{
   [TestClass]
   public class TrieTest
   {
      [TestMethod]
      public void TestTrie()
      {
         var searchStrings = new[] { "Bob", "Sue", "Frank", "Joe", "Sally" };

         var trie = new Trie<char>();
         foreach (var searchString in searchStrings)
         {
            trie.AddSearchString(searchString);
         }

         var tests = new Dictionary<string, Dictionary<string, int[]>>
         {
            {
               "Bob took Sally and Sue to see Frank at Sue's house.",
               new Dictionary<string, int[]>
               {
                  {"Bob", new[] {0}},
                  {"Sally", new[] {9}},
                  {"Sue", new[] {19, 39}},
                  {"Frank", new[] {30}},
                  {"Joe", new int[]{}}
               }
            },

            {
               "0123456SueSallyBob8901SallyFrankJoeJoe",
               new Dictionary<string, int[]>
               {
                  {"Bob", new[] {15}},
                  {"Sally", new[] {10, 22}},
                  {"Sue", new[] {7}},
                  {"Frank", new[] {27}},
                  {"Joe", new[]{32, 35}}
               }
            }
         };

         foreach (var test in tests)
         {
            var matches = trie.FindMatches(test.Key.ToList());

            Assert.IsTrue(matches.Count == test.Value.Count);
            foreach (var expectedResult in test.Value)
            {
               ICollection<int> offsets;
               Assert.IsTrue(matches.TryGetValue(expectedResult.Key, out offsets));
               Assert.IsTrue(offsets.Count == expectedResult.Value.Length);

               foreach (var offset in expectedResult.Value)
               {
                  Assert.IsTrue(offsets.Contains(offset));
               }
            }
         }
      }

      [TestMethod]
      public void TestTrieToTree()
      {
         var searchStrings = new[] { "Bob", "Sue", "Frank", "Joe", "Sally" };

         var trie = new Trie<char>();
         foreach (var searchString in searchStrings)
         {
            trie.AddSearchString(searchString);
         }
      }
   }
}
