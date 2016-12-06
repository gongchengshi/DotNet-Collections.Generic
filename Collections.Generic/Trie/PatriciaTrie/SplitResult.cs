// This code is distributed under MIT license. Copyright (c) 2013 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

namespace Gongchengshi.Collections.Generic.Trie.PatriciaTrie
{
   public struct SplitResult
   {
      private readonly StringPartition _head;
      private readonly StringPartition _rest;

      public SplitResult(StringPartition head, StringPartition rest)
      {
         _head = head;
         _rest = rest;
      }

      public StringPartition Rest
      {
         get { return _rest; }
      }

      public StringPartition Head
      {
         get { return _head; }
      }

      public bool Equals(SplitResult other)
      {
         return _head == other._head && _rest == other._rest;
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         return obj is SplitResult && Equals((SplitResult)obj);
      }

      public override int GetHashCode()
      {
         unchecked
         {
            return (_head.GetHashCode() * 397) ^ _rest.GetHashCode();
         }
      }

      public static bool operator ==(SplitResult left, SplitResult right)
      {
         return left.Equals(right);
      }

      public static bool operator !=(SplitResult left, SplitResult right)
      {
         return !(left == right);
      }
   }
}