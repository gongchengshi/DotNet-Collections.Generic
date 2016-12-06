using Gongchengshi;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Gongchengshi.Collections.Generic
{
    public class DifferenceInfo<T>
    {
        public readonly ICollection<T> Added = new List<T>();
        public readonly ICollection<T> Removed = new List<T>();

        public bool NoDifference { get { return Added.Count == 0 && Removed.Count == 0; } }
    }

   public static class MiscExtensions
   {
      public static void ToCsvFile<T>(this IEnumerable<IDictionary<string, T>> @this, string outputPath)
      {
         if (@this.IsEmpty())
            return;

         var keys = @this.First().Keys;

         var csvWriter = new CSVWriter(outputPath, FileMode.Create);
         csvWriter.WriteLine(keys);

         object[] values;

         foreach (var dict in @this)
         {
            values = new object[keys.Count];
            int i = 0;
            foreach (var key in keys)
            {
               values[i++] = dict[key];
            }
            csvWriter.WriteLine(values);
         }
      }
   }
}