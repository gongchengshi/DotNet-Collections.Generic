using System.Collections.Generic;

namespace Gongchengshi.Collections.Generic
{
   public interface ILongArray<T> : IEnumerable<T>
   {
      long Length { get; }
      T this[long index] { get; set; }
      IEnumerable<T> GetRange(long index, int count);
   }
}
