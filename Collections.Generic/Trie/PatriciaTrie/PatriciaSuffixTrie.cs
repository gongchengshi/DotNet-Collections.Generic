// This code is distributed under MIT license. Copyright (c) 2013 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System.Collections.Generic;
using System.Linq;

namespace Gongchengshi.Collections.Generic.Trie.PatriciaTrie
{
   public class PatriciaSuffixTrie<TValue> : ITrie<TValue>
   {
      private readonly int _minQueryLength;
      private readonly PatriciaTrie<TValue> _innerTrie;

      public PatriciaSuffixTrie(int minQueryLength)
         : this(minQueryLength, new PatriciaTrie<TValue>())
      { }

      internal PatriciaSuffixTrie(int minQueryLength, PatriciaTrie<TValue> innerTrie)
      {
         _minQueryLength = minQueryLength;
         _innerTrie = innerTrie;
      }

      protected int MinQueryLength
      {
         get { return _minQueryLength; }
      }

      public IEnumerable<TValue> Retrieve(string query)
      {
         return
             _innerTrie
                 .Retrieve(query)
                 .Distinct();
      }

      public void Add(string key, TValue value)
      {
         IEnumerable<StringPartition> allSuffixes = GetAllSuffixes(MinQueryLength, key);
         foreach (StringPartition currentSuffix in allSuffixes)
         {
            _innerTrie.Add(currentSuffix, value);
         }
      }

      private static IEnumerable<StringPartition> GetAllSuffixes(int minSuffixLength, string word)
      {
         for (int i = word.Length - minSuffixLength; i >= 0; i--)
         {
            yield return new StringPartition(word, i);
         }
      }
   }
}