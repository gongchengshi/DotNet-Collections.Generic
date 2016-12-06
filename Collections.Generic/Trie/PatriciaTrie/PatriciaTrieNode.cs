// This code is distributed under MIT license. Copyright (c) 2013 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Gongchengshi.Collections.Generic.Trie.Trie;

namespace Gongchengshi.Collections.Generic.Trie.PatriciaTrie
{
   [DebuggerDisplay("'{_key}'")]
   public class PatriciaTrieNode<TValue> : TrieNodeBase<TValue>
   {
      private Dictionary<char, PatriciaTrieNode<TValue>> _children;
      private StringPartition _key;
      private Queue<TValue> _values;

      protected PatriciaTrieNode(StringPartition key, TValue value)
         : this(key, new Queue<TValue>(new[] { value }), new Dictionary<char, PatriciaTrieNode<TValue>>())
      {
      }

      protected PatriciaTrieNode(StringPartition key, Queue<TValue> values,
          Dictionary<char, PatriciaTrieNode<TValue>> children)
      {
         _values = values;
         _key = key;
         _children = children;
      }

      protected override int KeyLength
      {
         get { return _key.Length; }
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

      internal virtual void Add(StringPartition keyRest, TValue value)
      {
         ZipResult zipResult = _key.ZipWith(keyRest);

         switch (zipResult.MatchKind)
         {
            case MatchKind.ExactMatch:
               AddValue(value);
               break;

            case MatchKind.IsContained:
               GetOrCreateChild(zipResult.OtherRest, value);
               break;

            case MatchKind.Contains:
               SplitOne(zipResult, value);
               break;

            case MatchKind.Partial:
               SplitTwo(zipResult, value);
               break;
         }
      }


      private void SplitOne(ZipResult zipResult, TValue value)
      {
         var leftChild = new PatriciaTrieNode<TValue>(zipResult.ThisRest, _values, _children);

         _children = new Dictionary<char, PatriciaTrieNode<TValue>>();
         _values = new Queue<TValue>();
         AddValue(value);
         _key = zipResult.CommonHead;

         _children.Add(zipResult.ThisRest[0], leftChild);
      }

      private void SplitTwo(ZipResult zipResult, TValue value)
      {
         var leftChild = new PatriciaTrieNode<TValue>(zipResult.ThisRest, _values, _children);
         var rightChild = new PatriciaTrieNode<TValue>(zipResult.OtherRest, value);

         _children = new Dictionary<char, PatriciaTrieNode<TValue>>();
         _values = new Queue<TValue>();
         _key = zipResult.CommonHead;

         char leftKey = zipResult.ThisRest[0];
         _children.Add(leftKey, leftChild);
         char rightKey = zipResult.OtherRest[0];
         _children.Add(rightKey, rightChild);
      }

      protected void GetOrCreateChild(StringPartition key, TValue value)
      {
         PatriciaTrieNode<TValue> child;
         if (!_children.TryGetValue(key[0], out child))
         {
            child = new PatriciaTrieNode<TValue>(key, value);
            _children.Add(key[0], child);
         }
         else
         {
            child.Add(key, value);
         }
      }

      protected override TrieNodeBase<TValue> GetOrCreateChild(char key)
      {
         throw new NotSupportedException("Use alternative signature instead.");
      }

      protected override TrieNodeBase<TValue> GetChildOrNull(string query, int position)
      {
         if (query == null) throw new ArgumentNullException("query");
         PatriciaTrieNode<TValue> child;
         if (_children.TryGetValue(query[position], out child))
         {
            var queryPartition = new StringPartition(query, position, child._key.Length);
            if (child._key.StartsWith(queryPartition))
            {
               return child;
            }
         }
         return null;
      }

      public string Traversal()
      {
         var result = new StringBuilder();
         result.Append(_key);

         string subtreeResult = string.Join(" ; ", _children.Values.Select(node => node.Traversal()).ToArray());
         if (subtreeResult.Length != 0)
         {
            result.Append("[");
            result.Append(subtreeResult);
            result.Append("]");
         }

         return result.ToString();
      }

      public override string ToString()
      {
         return
             string.Format(
                 "Key: {0}, Values: {1} Children:{2}, ",
                 _key,
                 Values().Count(),
                 String.Join(";", _children.Keys));
      }
   }
}