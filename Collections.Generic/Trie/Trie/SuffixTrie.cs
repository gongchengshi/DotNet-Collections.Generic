// This code is distributed under MIT license. Copyright (c) 2013 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System.Collections.Generic;
using System.Linq;
using Gongchengshi.Collections.Generic.Trie.PatriciaTrie;

namespace Gongchengshi.Collections.Generic.Trie.Trie
{
   public class SuffixTrie<T> : ITrie<T>
   {
      private readonly Trie<T> _innerTrie;
      private readonly int _minSuffixLength;

      public SuffixTrie(int minSuffixLength)
         : this(new Trie<T>(), minSuffixLength)
      {
      }

      private SuffixTrie(Trie<T> innerTrie, int minSuffixLength)
      {
         _innerTrie = innerTrie;
         _minSuffixLength = minSuffixLength;
      }

      public IEnumerable<T> Retrieve(string query)
      {
         return
             _innerTrie
                 .Retrieve(query)
                 .Distinct();
      }

      public void Add(string key, T value)
      {
         foreach (string suffix in GetAllSuffixes(_minSuffixLength, key))
         {
            _innerTrie.Add(suffix, value);
         }
      }

      private static IEnumerable<string> GetAllSuffixes(int minSuffixLength, string word)
      {
         for (int i = word.Length - minSuffixLength; i >= 0; i--)
         {
            var partition = new StringPartition(word, i);
            yield return partition.ToString();
         }
      }
   }
}