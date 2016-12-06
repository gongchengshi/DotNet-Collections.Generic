// This code is distributed under MIT license. Copyright (c) 2013 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEL.Collections.Generic.Trie;

namespace SEL.Collections.Generic.UnitTests.Trie.Trie
{
   public class TrieTest : BaseTrieTest
   {
      protected override ITrie<int> CreateTrie()
      {
         return new Generic.Trie.Trie.Trie<int>();
      }

      [TestMethod]
      [ExpectedException(typeof(AggregateException))]
      // [Explicit]
      [Ignore]
      public void ExhaustiveParallelAddFails()
      {
         while (true)
         {
            ITrie<int> trie = CreateTrie();
            LongPhrases40
                .AsParallel()
                .ForAll(phrase => trie.Add(phrase, phrase.GetHashCode()));
         }
      }
   }
}