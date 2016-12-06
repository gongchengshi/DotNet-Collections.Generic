using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Gongchengshi.Collections.Generic
{
   public class Trie<T>
   {
      private class Node
      {
         private readonly Dictionary<T, Node> _children = new Dictionary<T, Node>();

         public Node AddChild(T key)
         {
            Node node;
            if (!_children.TryGetValue(key, out node))
            {
               node = new Node();
               _children[key] = node;
            }

            return node;
         }

         public bool IsTerminal { get; set; }

         public Node GetChild(T key)
         {
            Node node;
            return _children.TryGetValue(key, out node) ? node : null;
         }

         public IEnumerable<IList<T>> List()
         {
            return _List();
         }

         private IEnumerable<IList<T>> _List()
         {
            var curNode = this;

            foreach (var keyValue in curNode._children)
            {
               if (keyValue.Value.IsTerminal)
               {                  
                  yield return new List<T>{keyValue.Key};
               }

               foreach (var postFix in keyValue.Value._List())
               {
                  postFix.Insert(0, keyValue.Key);
                  yield return postFix;
               }
            }
         }
      }

      private readonly Node _root = new Node();

      public void AddSearchString(IEnumerable<T> searchString)
      {
         Node node = _root;
         var en = searchString.GetEnumerator();
         while (en.MoveNext())
         {
            node = node.AddChild(en.Current);
         }
         node.IsTerminal = true;
      }

      public class TrieMatches<TIndex> : Dictionary<IEnumerable<T>, ICollection<TIndex>>
      { }

      public class TrieMatches : TrieMatches<int>
      { }

      public class TrieMatchesLong : TrieMatches<long>
      { }

      /// <summary>
      /// FindMatches() is thread safe so long as AddSearchString is not being called at the same time.
      /// </summary>
      public TrieMatches FindMatches(List<T> input)
      {
         var matchOffsets = new TrieMatches();

         for (int startPosition = 0; startPosition < input.Count; ++startPosition) // Change this to long for production
         {
            int currentPosition = startPosition; // Change this to long for production
            Node node = _root;

            Node tempNode;
            while ((tempNode = node.GetChild(input[currentPosition++])) != null)
            {
               node = tempNode;
            }

            if (node.IsTerminal)
            {
               var foundString = input.GetRange(startPosition, currentPosition - 1 - startPosition);
               AddMatch(matchOffsets, foundString, startPosition);
            }
         }
         return matchOffsets;
      }

      public TrieMatchesLong FindMatches(ILongArray<T> input)
      {
         var matchOffsets = new TrieMatchesLong();

         for (long startPosition = 0; startPosition < input.Length; ++startPosition)
         {
            long currentPosition = startPosition;
            Node node = _root;

            Node tempNode;
            while ((tempNode = node.GetChild(input[currentPosition++])) != null)
            {
               node = tempNode;
            }

            if (node.IsTerminal)
            {
               var foundString = input.GetRange(startPosition, Convert.ToInt32(currentPosition - 1 - startPosition));
               AddMatch(matchOffsets, foundString, startPosition);
            }
         }
         return matchOffsets;
      }

      public IEnumerable<IEnumerable<T>> List()
      {
         return _root.List();
      }

      private static void AddMatch<TIndex>(TrieMatches<TIndex> matchOffsets, IEnumerable<T> foundString, TIndex offset)
      {
         ICollection<TIndex> offsetList;
         if (matchOffsets.TryGetValue(foundString, out offsetList))
         {
            offsetList.Add(offset);
         }
         else
         {
            matchOffsets[foundString] = new List<TIndex> { offset };
         }
      }
   }
}
