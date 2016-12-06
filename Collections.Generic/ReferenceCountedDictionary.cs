using System;
using System.Collections.Generic;

namespace Gongchengshi.Collections.Generic
{
    public class ReferenceCountWrapper<T>
    {
        public ReferenceCountWrapper(T value, int count)
        {
            Value = value;
            Count = count;
        }

        public T Value;
        public int Count;
    }

    /// <summary>
    /// A dictionary that keeps track of how many attempts have been made to add a given key and
    /// does not remove the item from the collection until the same number of removes have been attempted.
    /// 
    /// An exception is thrown if values of additional adds for a given key do not match the original value.
    /// </summary>
    public class ReferenceCountedDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        public ReferenceCountedDictionary()
        {
            _dictionary = new Dictionary<TKey, ReferenceCountWrapper<TValue>>();
        }

        private Dictionary<TKey, ReferenceCountWrapper<TValue>> _dictionary;

        #region IDictionary Implementation
        public void Add(TKey key, TValue value)
        {
            AddWithResult(key, value);
        }

        public bool Contains(object key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            if (!(key is TKey))
            {
                throw new ArgumentException("key");
            }

            return ContainsKey((TKey)key);
        }

        public ICollection<TKey> Keys
        {
            get { return _dictionary.Keys; }
        }

        /// <summary>
        /// Removes the value with the specified key from the Dictionary 
        /// if the resultant count after removing would be 0.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>true if the element is removed from the collection, 
        /// false if the reference count is still greater than 0 or the key was not found.</returns>
        public virtual bool Remove(TKey key)
        {
            ReferenceCountWrapper<TValue> wrapper;
            if (!_dictionary.TryGetValue(key, out wrapper))
            {
                return false;
            }

            --wrapper.Count;

            if (wrapper.Count == 0)
            {
                _dictionary.Remove(key);
                return true;
            }

            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            ReferenceCountWrapper<TValue> wrapper;
            if (_dictionary.TryGetValue(key, out wrapper))
            {
                value = wrapper.Value;
                return true;
            }
            else
            {
                value = default(TValue);
                return false;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                var values = new List<TValue>();

                foreach (var item in _dictionary.Values)
                {
                    values.Add(item.Value);
                }

                return values;
            }
        }

        public TValue this[TKey key]
        {
            get { return _dictionary[key].Value; }
            set { Add(key, value); }
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public int Count
        {
            get { return _dictionary.Count; }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new Enumerator(_dictionary);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get { return false; }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Same as IDictionary.Add but takes a KeyValuePair.
        /// </summary>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        /// <returns>True if the key was newly added, false if the reference count was incremented</returns>
        public bool AddWithResult(TKey key, TValue value)
        {
            ReferenceCountWrapper<TValue> valueWrapper;

            if (_dictionary.TryGetValue(key, out valueWrapper))
            {
                if (!valueWrapper.Value.Equals(value))
                {
                    throw new InvalidOperationException("The new value does not match the existing one");
                }

                ++valueWrapper.Count;
                return false;
            }
            else
            {
                valueWrapper = new ReferenceCountWrapper<TValue>(value, 1);
                _dictionary.Add(key, valueWrapper);
                return true;
            }
        }

        /// <summary>
        /// More explicit contains function.
        /// </summary>
        /// <returns>True if the key is in the collection</returns>
        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        public class Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            private Dictionary<TKey, ReferenceCountWrapper<TValue>> _internalDictionary;
            private Dictionary<TKey, ReferenceCountWrapper<TValue>>.Enumerator _enumerator;

            public Enumerator(Dictionary<TKey, ReferenceCountWrapper<TValue>> internalDictionary)
            {
                _internalDictionary = internalDictionary;
                _enumerator = _internalDictionary.GetEnumerator();
            }

            public KeyValuePair<TKey, TValue> Current
            {
                get
                {
                    return new KeyValuePair<TKey, TValue>(
                        _enumerator.Current.Key, _enumerator.Current.Value.Value);
                }
            }

            public void Dispose()            
            {
                // No dispose guard needed here because Dictionary.Enumerator handles this.
                _enumerator.Dispose();
            }

            object System.Collections.IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                return _enumerator.MoveNext();
            }

            public void Reset()
            {
                _enumerator = _internalDictionary.GetEnumerator();
            }
        }
    }
}
