// This code is distributed under MIT license. Copyright (c) 2013 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEL.Collections.Generic.Trie;
using SEL.Collections.Generic.Trie.PatriciaTrie;
using SEL.UnitTest;

namespace SEL.Collections.Generic.UnitTests.Trie.PatriciaTrie
{
   [TestClass]
   public class PatriciaTrieTest : BaseTrieTest
   {
      protected override ITrie<int> CreateTrie()
      {
         return new PatriciaTrie<int>();
      }

      [TestMethod]
      public void TestNotExactMatched()
      {
         ITrie<int> trie = new PatriciaTrie<int>();
         trie.Add("aaabbb", 1);
         trie.Add("aaaccc", 2);

         var actual = trie.Retrieve("aab");

         SELAssert.AreCollectionsEqual(Enumerable.Empty<int>(), actual);
      }
   }
}
