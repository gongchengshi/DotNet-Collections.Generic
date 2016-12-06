// This code is distributed under MIT license. Copyright (c) 2013 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Gongchengshi.Collections.Generic.Trie.PatriciaTrie
{
   [DebuggerDisplay(
      "{_origin.Substring(0,_startIndex)} [ {_origin.Substring(_startIndex,_partitionLength)} ] {_origin.Substring(_startIndex + _partitionLength)}"
      )]
   public struct StringPartition : IEnumerable<char>
   {
      private readonly string _origin;
      private readonly int _partitionLength;
      private readonly int _startIndex;

      public StringPartition(string origin)
         : this(origin, 0, origin == null ? 0 : origin.Length)
      {
      }

      public StringPartition(string origin, int startIndex)
         : this(origin, startIndex, origin == null ? 0 : origin.Length - startIndex)
      {
      }

      public StringPartition(string origin, int startIndex, int partitionLength)
      {
         if (origin == null) throw new ArgumentNullException("origin");
         if (startIndex < 0) throw new ArgumentOutOfRangeException("startIndex", "The value must be non negative.");
         if (partitionLength < 0)
            throw new ArgumentOutOfRangeException("partitionLength", "The value must be non negative.");
         _origin = string.Intern(origin);
         _startIndex = startIndex;
         int availableLength = _origin.Length - startIndex;
         _partitionLength = Math.Min(partitionLength, availableLength);
      }

      public char this[int index]
      {
         get { return _origin[_startIndex + index]; }
      }

      public int Length
      {
         get { return _partitionLength; }
      }

      #region IEnumerable<char> Members

      public IEnumerator<char> GetEnumerator()
      {
         for (int i = 0; i < _partitionLength; i++)
         {
            yield return this[i];
         }
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }

      #endregion

      public bool Equals(StringPartition other)
      {
         return string.Equals(_origin, other._origin) && _partitionLength == other._partitionLength &&
                _startIndex == other._startIndex;
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         return obj is StringPartition && Equals((StringPartition)obj);
      }

      public override int GetHashCode()
      {
         unchecked
         {
            int hashCode = (_origin != null ? _origin.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ _partitionLength;
            hashCode = (hashCode * 397) ^ _startIndex;
            return hashCode;
         }
      }

      public bool StartsWith(StringPartition other)
      {
         if (Length < other.Length)
         {
            return false;
         }

         for (int i = 0; i < other.Length; i++)
         {
            if (this[i] != other[i])
            {
               return false;
            }
         }
         return true;
      }

      public SplitResult Split(int splitAt)
      {
         var head = new StringPartition(_origin, _startIndex, splitAt);
         var rest = new StringPartition(_origin, _startIndex + splitAt, Length - splitAt);
         return new SplitResult(head, rest);
      }

      public ZipResult ZipWith(StringPartition other)
      {
         int splitIndex = 0;
         using (IEnumerator<char> thisEnumerator = GetEnumerator())
         using (IEnumerator<char> otherEnumerator = other.GetEnumerator())
         {
            while (thisEnumerator.MoveNext() && otherEnumerator.MoveNext())
            {
               if (thisEnumerator.Current != otherEnumerator.Current)
               {
                  break;
               }
               splitIndex++;
            }
         }

         SplitResult thisSplitted = Split(splitIndex);
         SplitResult otherSplitted = other.Split(splitIndex);

         StringPartition commonHead = thisSplitted.Head;
         StringPartition restThis = thisSplitted.Rest;
         StringPartition restOther = otherSplitted.Rest;
         return new ZipResult(commonHead, restThis, restOther);
      }

      public override string ToString()
      {
         var result = new string(this.ToArray());
         return string.Intern(result);
      }

      public static bool operator ==(StringPartition left, StringPartition right)
      {
         return left.Equals(right);
      }

      public static bool operator !=(StringPartition left, StringPartition right)
      {
         return !(left == right);
      }
   }
}
