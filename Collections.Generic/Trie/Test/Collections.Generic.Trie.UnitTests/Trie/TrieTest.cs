﻿// This code is distributed under MIT license. Copyright (c) 2013 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using System.Linq;
using NUnit.Framework;

namespace SEL.Collections.Generic.Trie.UnitTests.Trie
{
   public class TrieTest : BaseTrieTest
   {
      protected override ITrie<int> CreateTrie()
      {
         return new Generic.Trie.Trie.Trie<int>();
      }

      [Test]
      [ExpectedException(typeof(AggregateException))]
      [Explicit]
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