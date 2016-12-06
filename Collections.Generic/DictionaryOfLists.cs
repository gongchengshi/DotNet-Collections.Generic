using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Gongchengshi.Collections.Generic
{
    /// <summary>
    /// A very common data structure needed is a Dictionary of Lists.  This class
    /// is built upon a generic Dictionary of generic Lists, and encapsulates all
    /// the logic to do this.
    /// </summary>
    /// <typeparam name="TKey">The type of the dictionary key.</typeparam>
    /// <typeparam name="TValues">The type of the items stored in the lists.</typeparam>
    public class DictionaryOfLists<TKey, TValues> : IEnumerable<KeyValuePair<TKey, List<TValues>>>
    {
        readonly Dictionary<TKey, List<TValues>> _dictionary = new Dictionary<TKey, List<TValues>>();

        /// <summary>
        /// Add the given item to the list under the dictionary entry with the given key.
        /// Creates the list if this is the first item in it.
        /// </summary>
        /// <param name="key">Key to place under.</param>
        /// <param name="item">Item to add to list.</param>
        public void Add(TKey key, TValues item)
        {
            GetList(key).Add(item);
        }

        private List<TValues> GetList(TKey key)
        {
            List<TValues> list;
            if (!_dictionary.TryGetValue(key, out list))
            {
                list = new List<TValues>();
                _dictionary.Add(key, list);
            }
            return list;
        }

        /// <summary>
        /// Include the given item in the list under the dictionary entry with the given key.
        /// If the item is already in the list, no action is taken.
        /// Creates the list if this is the first item in it.
        /// </summary>
        /// <param name="key">Key to place under.</param>
        /// <param name="item">Item to include in list.</param>
        public void Include(TKey key, TValues item)
        {
            var list = GetList(key);
            if (!list.Contains(item))
                list.Add(item);
        }

        /// <summary>
        /// Remove the given value from list with the given key.
        /// Will throw if key is not in dictionary or item not in list.
        /// </summary>
        /// <param name="key">Key to remove item from.</param>
        /// <param name="value">Item to remove.</param>
        internal void Remove(TKey key, TValues value)
        {
            var list = _dictionary[key];
            list.Remove(value);
            if (list.Count == 0)
                _dictionary.Remove(key);
        }

        /// <summary>
        /// Enumerates the keys within the dictionary.
        /// </summary>
        public IEnumerable<TKey> Keys
        {
            get { return _dictionary.Keys; }
        }

        /// <summary>
        /// Enumerates the lists within the dictionary values.
        /// </summary>
        public IEnumerable<List<TValues>> ValueLists
        {
            get { return _dictionary.Values; }
        }

        /// <summary>
        /// Enumerate the items under the given key.
        /// </summary>
        /// <param name="key">Key to return items under</param>
        /// <returns>Items.  Returns empty enumerable if no items exist under key.</returns>
        public IEnumerable<TValues> this[TKey key]
        {
            get
            {
                List<TValues> list;
                return _dictionary.TryGetValue(key, out list) ? list : Enumerable.Empty<TValues>();
            }
        }

        /// <summary>
        /// Clear the dictionary.
        /// </summary>
        public void Clear() { _dictionary.Clear(); }

        /// <summary>
        /// Returns the count of keys in the dictionary.
        /// </summary>
        public int CountKeys
        {
            get { return _dictionary.Count; }            
        }

        /// <summary>
        /// Returns the count of items in the value lists in the dictionary.
        /// </summary>
        public int CountItems
        {
            get { return _dictionary.Values.Sum(list => list.Count); }
        }

        public IEnumerable<TValues> AllValues
        {
            get { return _dictionary.Values.SelectMany(list => list); }
        }

       public IEnumerator<KeyValuePair<TKey, List<TValues>>> GetEnumerator()
       {
          return _dictionary.GetEnumerator();
       }

       IEnumerator IEnumerable.GetEnumerator()
       {
          return GetEnumerator();
       }
    }
}
