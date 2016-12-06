using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;

namespace Gongchengshi.Collections.Generic
{
   public class MemoryMappedArray<T> : IEnumerable<T>, IDisposable where T: struct
   {
      private readonly MemoryMappedViewAccessor _accessor;
      private readonly MemoryMappedFile _mmf;

      private static readonly bool IsRunningOnMono = (Type.GetType("Mono.Runtime") != null);

      public MemoryMappedArray(string path)
      {
         Length = new FileInfo(path).Length;

         if (IsRunningOnMono)
         {
            _mmf = MemoryMappedFile.CreateFromFile(path, FileMode.Open, Guid.NewGuid().ToString(), Length,
               MemoryMappedFileAccess.Read);
         }
         else
         {
            _mmf = MemoryMappedFile.CreateFromFile(path, FileMode.Open);
         }

         _accessor = _mmf.CreateViewAccessor(0, Length, MemoryMappedFileAccess.Read);  
     }

      public IEnumerator<T> GetEnumerator()
      {
         throw new NotImplementedException();
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }

      public long Length { get; private set; }

      public T this[long index]
      {
         get
         {
            T output;
            _accessor.Read(index, out output);
            return output;
         }
         set { throw new NotImplementedException(); }
      }

      public void Dispose()
      {
         _accessor.Dispose();
         _mmf.Dispose();
      }
   }
}
