// This code is distributed under MIT license. Copyright (c) 2013 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Gongchengshi.Collections.Generic.Trie.Trie
{
   public class ConcurrentTrieNode<TValue> : TrieNodeBase<TValue>
   {
      private readonly ConcurrentDictionary<char, ConcurrentTrieNode<TValue>> _children;
      private readonly ConcurrentQueue<TValue> _values;

      public ConcurrentTrieNode()
      {
         _children = new ConcurrentDictionary<char, ConcurrentTrieNode<TValue>>();
         _values = new ConcurrentQueue<TValue>();
      }

      protected override int KeyLength
      {
         get { return 1; }
      }

      protected override IEnumerable<TValue> Values()
      {
         return _values;
      }

      protected override IEnumerable<TrieNodeBase<TValue>> Children()
      {
         return _children.Values;
      }

      protected override void AddValue(TValue value)
      {
         _values.Enqueue(value);
      }

      protected override TrieNodeBase<TValue> GetOrCreateChild(char key)
      {
         return _children.GetOrAdd(key, new ConcurrentTrieNode<TValue>());
      }

      protected override TrieNodeBase<TValue> GetChildOrNull(string query, int position)
      {
         if (query == null) throw new ArgumentNullException("query");
         ConcurrentTrieNode<TValue> childNode;
         return
             _children.TryGetValue(query[position], out childNode)
                 ? childNode
                 : null;
      }
   }
}