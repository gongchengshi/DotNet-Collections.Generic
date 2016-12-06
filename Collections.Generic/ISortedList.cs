using System.Collections.Generic;

namespace Gongchengshi.Collections.Generic
{
    /// <summary>
    /// Interface for collections that maintain order.  It is essentially IList(T) with certain operations
    /// removed because they don't work with ordered collections.
    /// </summary>
    public interface ISortedList<T> : ICollection<T>
    {
        #region Borrowed from List<T>

        int IndexOf(T value);

        void RemoveAt(int index);

        #endregion

        T this[int index] { get; }
    }
}
