using System;
using System.Collections;
using System.Collections.Generic;

namespace Gongchengshi.Collections.Generic
{
    /// <summary>
    /// Collections that holds elements in the specified order. The complexity and efficiency
    /// of the algorithm is comparable to the SortedList from .NET collections. In contrast 
    /// to the SortedSet, SortedList accepts redundant elements. If no comparer is 
    /// specified the list will use the default comparer for given type.
    /// 
    /// Adapted from http://softcollections.codeplex.com/
    /// 
    /// All methods have the same API as List(T)
    /// </summary>
    public class SortedList<T> : ISortedList<T>
    {
        // Allows for only comparing reference types with null and avoids boxing value types.
        private static readonly bool _isClass = typeof (T).IsClass;

        private const int DefaultCapacity = 4;

        // Slight optimization: avoids allocation of an empty array per instance
        private static readonly T[] _emptyValues = new T[0];

        private readonly IComparer<T> _comparer;
        private T[] _values;
        private int _size;

        // Helps ensure the collection isn't changed during enumeration
        private int _version;

        public SortedList()
        {
            _values = _emptyValues;
            _comparer = System.Collections.Generic.Comparer<T>.Default;
        }

        public SortedList(int capacity) : this()
        {
            Capacity = capacity;
        }

        public SortedList(IComparer<T> comparer)
        {
            _values = _emptyValues;
            _comparer = comparer;
        }

        /// <summary>
        /// If the capacity of the list is less than min, this increases it to min.
        /// </summary>
        private void ResizeIfLessThan(int min)
        {
            // double the capacity
            int num = _values.Length == 0 ? DefaultCapacity : _values.Length * 2;
            if (min > num)
            {
                num = min;
            }
            Capacity = num;
        }

        /// <summary>
        /// Behaves the same as IList(T).Insert(), only it is for internal use only
        /// </summary>
        protected virtual void Insert(int index, T value)
        {
            if (_isClass && (value == null))
            {
                throw new ArgumentException("Value can't be null.");
            }

            if (index < 0 || index > _size)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (_size == _values.Length)
            {
                ResizeIfLessThan(_size + 1);
            }

            if (index < _size)
            {
                Array.Copy(_values, index, _values, index + 1, _size - index);
            }

            _values[index] = value;
            _size++;
            _version++;
        }

        /// <summary>
        /// Inserts value at the appropriate location in order to maintain orderedness.
        /// </summary>
        public void Add(T value)
        {
            if (_isClass && (value == null))
            {
                throw new ArgumentException("Value can't be null.");
            }

            // check where the element should be placed
            int index = Array.BinarySearch(_values, 0, _size, value, _comparer);
            if (index < 0)
            {
                // xor
                index = ~index;
            }
            Insert(index, value);
        }

        public virtual void Clear()
        {
            _version++;
            Array.Clear(_values, 0, _size);
            _size = 0;
        }

        public int IndexOf(T value)
        {
            if (_isClass && (value == null))
            {
                throw new ArgumentException("Value can't be null.");
            }
            int index = Array.BinarySearch(_values, 0, _size, value, _comparer);
            if (index >= 0)
            {
                return index;
            }
            return -1;
        }

        public bool Contains(T value)
        {
            return IndexOf(value) >= 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(_values, 0, array, arrayIndex, _size);
        }

        public int Count
        {
            get { return _size; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T value)
        {
            int index = IndexOf(value);
            if (index < 0)
            {
                return false;
            }
            RemoveAt(index);
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public int Capacity
        {
            get { return _values.Length; }
            set
            {
                if (_values.Length != value)
                {
                    if (value < _size)
                        throw new ArgumentException("Too small capacity.");

                    if (value > 0)
                    {
                        var tempValues = new T[value];
                        if (_size > 0)
                        {
                            // copy only when size is greater than zero
                            Array.Copy(_values, 0, tempValues, 0, _size);
                        }
                        _values = tempValues;
                    }
                }
            }
        }

        public virtual void RemoveAt(int index)
        {
            if (index < 0 || index >= _size)
            {
                throw new ArgumentOutOfRangeException();
            }

            _size--;
            _version++;
            Array.Copy(_values, index + 1, _values, index, _size - index);
            // Frees reference to avoid memory leak
            _values[_size] = default(T);
        }

        public virtual T this[int index]
        {
            get
            {
                if (index < 0 || index >= _size)
                {
                    throw new ArgumentOutOfRangeException();
                }

                return _values[index];
            }
            set
            {
                if (index < 0 || index >= _size)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _values[index] = value;
                _version++;
            }
        }

        /// <summary>
        /// Enumerator class for SortedList(T)
        /// </summary>
        private sealed class Enumerator : IEnumerator<T>
        {
            private readonly SortedList<T> _collection;
            private int _index;
            private readonly int _version;

            internal Enumerator(SortedList<T> collection)
            {
                _collection = collection;
                _version = collection._version;
            }

            public void Dispose()
            {
                _index = 0;
                Current = default(T);
            }

            private void ValidateVersion()
            {
                if (_version != _collection._version)
                {
                    throw new InvalidOperationException(
                        "Collection was modified after the enumerator was instantiated.");
                }
            }

            public bool MoveNext()
            {
                ValidateVersion();

                if (_index < _collection.Count)
                {
                    Current = _collection._values[_index];
                    _index++;
                    return true;
                }

                _index = _collection.Count + 1;
                Current = default(T);
                return false;
            }

            void IEnumerator.Reset()
            {
                ValidateVersion();

                _index = 0;
                Current = default(T);
            }

            public T Current { get; private set; }

            object IEnumerator.Current
            {
                get
                {
                    if (_index == 0)
                    {
                        throw new InvalidOperationException("Enumerator not initialized. Call MoveNext first.");
                    }

                    if (_index == _collection.Count + 1)
                    {
                        throw new InvalidOperationException(
                            "Enumerator is past the end of the collection. Call Reset to return to beginning.");
                    }

                    return Current;
                }
            }
        }
    }
}