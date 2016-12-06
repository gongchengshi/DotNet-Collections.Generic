using System;
using System.Collections.Generic;
using System.Linq;

namespace Gongchengshi.Collections.Generic
{
   public class PrefixTree<TPrefix, TValue> where TValue : class
   {
      public Node Root = new Node();
      public int Count = 0;
      public void Add(IList<TPrefix> parts, TValue value)
      {
         Root.Add(parts, value);
         Count += 1;
      }

      public class Node
      {
         public Dictionary<TPrefix, InternalNode> Children = new Dictionary<TPrefix, InternalNode>();

         public void Add(IList<TPrefix> parts, TValue value)
         {
            var prefix = parts[0];

            InternalNode node;
            try
            {
               node = Children[prefix];
               if (parts.Count == 1)
               {
                  if (node.HasValue)
                  {
                     throw new ArgumentException("Prefix tree already contains a node with this key.");
                  }
                  // Reached terminus and the node exists so just set full and value fields.
                  node.Value = value;
                  return;
               }
            }
            catch (KeyNotFoundException)
            {
               if (parts.Count == 1)
               {
                  // Reached terminus and this is a new node.
                  node = new InternalNode(prefix, value);
                  Children.Add(prefix, node);
                  return;
               }

               node = new InternalNode(prefix);
               Children.Add(prefix, node);
            }

            node.Add(parts.Skip(1).ToList(), value);
         }
      }

      public class InternalNode : Node
      {
         public InternalNode(TPrefix prefix)
         {
            Prefix = prefix;
         }

         public InternalNode(TPrefix prefix, TValue value)
         {
            Prefix = prefix;
            Value = value;
         }

         public bool HasValue { get { return Value != null; } }

         public TPrefix Prefix;
         public TValue Value;
      }
   }
}
