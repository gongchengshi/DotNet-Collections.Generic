// This code is distributed under MIT license. Copyright (c) 2013 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System.Diagnostics;

namespace Gongchengshi.Collections.Generic.Trie.PatriciaTrie
{
   [DebuggerDisplay("Head: '{CommonHead}', This: '{ThisRest}', Other: '{OtherRest}', Kind: {MatchKind}")]
   public struct ZipResult
   {
      private readonly StringPartition _commonHead;
      private readonly StringPartition _otherRest;
      private readonly StringPartition _thisRest;

      public ZipResult(StringPartition commonHead, StringPartition thisRest, StringPartition otherRest)
      {
         _commonHead = commonHead;
         _thisRest = thisRest;
         _otherRest = otherRest;
      }

      public MatchKind MatchKind
      {
         get
         {
            return _thisRest.Length == 0
               ? (_otherRest.Length == 0
                  ? MatchKind.ExactMatch
                  : MatchKind.IsContained)
               : (_otherRest.Length == 0
                  ? MatchKind.Contains
                  : MatchKind.Partial);
         }
      }

      public StringPartition OtherRest
      {
         get { return _otherRest; }
      }

      public StringPartition ThisRest
      {
         get { return _thisRest; }
      }

      public StringPartition CommonHead
      {
         get { return _commonHead; }
      }


      public bool Equals(ZipResult other)
      {
         return
             _commonHead == other._commonHead &&
             _otherRest == other._otherRest &&
             _thisRest == other._thisRest;
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         return obj is ZipResult && Equals((ZipResult)obj);
      }

      public override int GetHashCode()
      {
         unchecked
         {
            int hashCode = _commonHead.GetHashCode();
            hashCode = (hashCode * 397) ^ _otherRest.GetHashCode();
            hashCode = (hashCode * 397) ^ _thisRest.GetHashCode();
            return hashCode;
         }
      }

      public static bool operator ==(ZipResult left, ZipResult right)
      {
         return left.Equals(right);
      }

      public static bool operator !=(ZipResult left, ZipResult right)
      {
         return !(left == right);
      }
   }
}
