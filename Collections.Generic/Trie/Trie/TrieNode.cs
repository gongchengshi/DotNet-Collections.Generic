// This code is distributed under MIT license. Copyright (c) 2013 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using System.Collections.Generic;

namespace Gongchengshi.Collections.Generic.Trie.Trie
{
   public class TrieNode<TValue> : TrieNodeBase<TValue>
   {
      private readonly Dictionary<char, TrieNode<TValue>> _children;
      private readonly Queue<TValue> _values;

      protected TrieNode()
      {
         _children = new Dictionary<char, TrieNode<TValue>>();
         _values = new Queue<TValue>();
      }

      protected override int KeyLength
      {
         get { return 1; }
      }

      protected override IEnumerable<TrieNodeBase<TValue>> Children()
      {
         return _children.Values;
      }

      protected override IEnumerable<TValue> Values()
      {
         return _values;
      }

      protected override TrieNodeBase<TValue> GetOrCreateChild(char key)
      {
         TrieNode<TValue> result;
         if (!_children.TryGetValue(key, out result))
         {
            result = new TrieNode<TValue>();
            _children.Add(key, result);
         }
         return result;
      }

      protected override TrieNodeBase<TValue> GetChildOrNull(string query, int position)
      {
         if (query == null) throw new ArgumentNullException("query");
         TrieNode<TValue> childNode;
         return
             _children.TryGetValue(query[position], out childNode)
                 ? childNode
                 : null;
      }

      protected override void AddValue(TValue value)
      {
         _values.Enqueue(value);
      }
   }
}