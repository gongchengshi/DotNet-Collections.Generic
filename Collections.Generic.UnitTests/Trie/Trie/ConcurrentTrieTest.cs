﻿// This code is distributed under MIT license. Copyright (c) 2013 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System.Linq;
using SEL.Collections.Generic.Trie;
using SEL.Collections.Generic.Trie.Trie;

namespace SEL.Collections.Generic.UnitTests.Trie.Trie
{
    public class ConcurrentTrieTest : BaseTrieTest
    {
        protected override ITrie<int> CreateTrie()
        {
            return new ConcurrentTrie<int>();
        }

//        [TestCase(1)]
//        [TestCase(4)]
//        [TestCase(13)]
        public void ExhaustiveParallelAdd(int degreeofParallelism)
        {
            ITrie<string> trie = new ConcurrentTrie<string>();
            LongPhrases40
                .AsParallel()
                .WithDegreeOfParallelism(degreeofParallelism)
                .ForAll(phrase => trie.Add(phrase, phrase));
        }
    }
}