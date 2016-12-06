using System;
using System.Collections;
using System.Collections.Generic;

namespace Gongchengshi.Collections.Generic
{
   public class LongArray<T> : ILongArray<T>
   {
      private readonly T[][] _arrays;

      public LongArray(long size)
      {
         Length = size;
         long arrayCount = size/int.MaxValue;

         long remainder = size - int.MaxValue*arrayCount;

         _arrays = new T[Convert.ToInt32(arrayCount) + Convert.ToInt32(Convert.ToBoolean(remainder))][];

         for (int i = 0; i < _arrays.Length - 1; ++i)
         {
            _arrays[i] = new T[int.MaxValue];
         }

         _arrays[_arrays.Length - 1] = new T[(remainder > 0) ? remainder : int.MaxValue];
      }

      public class LongArrayEnumerator : IEnumerator<T>
      {
         private T[][] _arrays;

         public LongArrayEnumerator(T[][] arrays)
         {
            _arrays = arrays;
         }

         public void Dispose()
         {
            throw new NotImplementedException();
         }

         public bool MoveNext()
         {
            throw new NotImplementedException();
         }

         public void Reset()
         {
            throw new NotImplementedException();
         }

         public T Current { get; private set; }

         object IEnumerator.Current
         {
            get { return Current; }
         }
      }

      public IEnumerator<T> GetEnumerator()
      {
         return new LongArrayEnumerator(_arrays);
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }

      public long Length { get; private set; }

      public T this[long index]
      {
         get { throw new NotImplementedException(); }
         set { throw new NotImplementedException(); }
      }

      public IEnumerable<T> GetRange(long index, int count)
      {
         throw new NotImplementedException();
      }
   }
}
