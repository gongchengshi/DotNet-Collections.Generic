using System;
using System.Collections.Generic;

namespace Gongchengshi.Collections.Generic
{
    /// <summary>
    /// A set that keeps track of how many attempts have been made to add a given item and
    /// does not remove the item from the collection until the same number of removes have been attempted.
    /// 
    /// Some of the elementary set operations are not implemented yet.
    /// </summary>
    public class MultiSet<T> : ISet<T>
    {
        private readonly IDictionary<T, int> _counts = new Dictionary<T, int>();

        #region ISet Implementation
        public bool Add(T item)
        {
            if (_counts.ContainsKey(item))
            {
                ++_counts[item];
                return false;
            }
            else
            {
                _counts.Add(item, 1);
                return true;
            }
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void UnionWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        void ICollection<T>.Add(T item)
        {
            this.Add(item);
        }

        public void Clear()
        {
            _counts.Clear();
        }

        public bool Contains(T item)
        {
            return _counts.ContainsKey(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _counts.Keys.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _counts.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            if (!_counts.ContainsKey(item))
                return false;

            _counts[item]--;
            if (_counts[item] == 0)
            {
                _counts.Remove(item);
                return true;
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _counts.Keys.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
