using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Gongchengshi.Collections.Generic
{
    /// <summary>
    /// An improvement on ObservableCollection.  This allows the ability to add and remove multiple items 
    /// from the collection and only raise a single CollectionChanged event.
    /// 
    /// This class is not thread safe.
    /// </summary>
    public class ObservableList<T> : IList<T>, IObservableCollection<T>
    {
        protected List<T> _collection = new List<T>();

        public ObservableList()
        { }

        public ObservableList(IEnumerable<T> collection)
        {
            _collection = new List<T>(collection);
        }

        #region INotifyCollectionChanged and INotifyPropertyChanged implementation and helpers

        public event NotifyCollectionChangedEventHandler CollectionChanged = delegate { };
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private bool _collectionChangeNotificationSuspended;

        protected void RaiseCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            if (!_collectionChangeNotificationSuspended)
            {
                CollectionChanged(this, args);
            }

            RaisePropertyChanged("Count");
        }

        protected void RaiseCollectionChanged(NotifyCollectionChangedAction action, T item, int index)
        {
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
        }

        protected void RaiseCollectionChanged(NotifyCollectionChangedAction action, T oldItem, T newItem, int index)
        {
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
        }
# if UNUSED_CODE
        // I (Stan) have removed this code because it appears to be unused and cannot be code coveraged.
        protected void RaiseCollectionChanged(NotifyCollectionChangedAction action, T item, int index, int oldIndex)
        {
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index, oldIndex));
        }
#endif
        public void RaiseCollectionReset()
        {
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        private void RaisePropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged(this, e);
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            RaisePropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        public void SuspendCollectionChangeNotification()
        {
            _collectionChangeNotificationSuspended = true;
        }

        public void ResumeCollectionChangeNotification()
        {
            _collectionChangeNotificationSuspended = false;
            RaiseCollectionReset();
        }

        #endregion

        #region IList Implementation
        public int IndexOf(T item)
        {
            return _collection.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _collection.Insert(index, item);

            RaiseCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
        }

        public void RemoveAt(int index)
        {
            var item = _collection[index];
            _collection.RemoveAt(index);

            RaiseCollectionChanged(NotifyCollectionChangedAction.Remove, item, index);
        }

        public T this[int index]
        {
            get
            {
                return _collection[index];
            }
            set
            {
                var oldItem = _collection[index];
                _collection[index] = value;

                RaiseCollectionChanged(NotifyCollectionChangedAction.Replace, value, oldItem, index);
            }
        }

        public void Add(T item)
        {
            var index = _collection.Count;
            _collection.Add(item);

            RaiseCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
        }

        public void Clear()
        {
            _collection.Clear();

            RaiseCollectionReset();
        }

        public bool Contains(T item)
        {
            return _collection.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _collection.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _collection.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            var index = _collection.IndexOf(item);
            
            if (index == -1)
                return false;

            this.RemoveAt(index);
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }
        #endregion

        #region Methods found in List<T> but not IList<T>
        public void AddRange(IEnumerable<T> collection)
        {
            if (collection.IsEmpty())
                return;
            try
            {
                _collection.AddRange(collection);
            }
            finally
            {
                RaiseCollectionReset();
            }
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            try
            {
                _collection.InsertRange(index, collection);
            }
            finally
            {
                RaiseCollectionReset();
            }
        }

        public void RemoveRange(int index, int count)
        {
            try
            {
                _collection.RemoveRange(index, count);
            }
            finally
            {
                RaiseCollectionReset();
            }
        }

        public int RemoveAll(Predicate<T> match)
        {
            int numRemoved = 0;
            try
            {
                numRemoved = _collection.RemoveAll(match);
            }
            finally
            {
                if (numRemoved > 0)
                {
                    RaiseCollectionReset();
                }
            }

            return numRemoved;
        }

        #region Sorting
        public void Sort()
        {
            _collection.Sort();

            RaiseCollectionReset();
        }

        public void Sort(IComparer<T> comparer)
        {
            _collection.Sort(comparer);

            RaiseCollectionReset();
        }

        public void Sort(int index, int count, IComparer<T> comparer)
        {
            _collection.Sort(index, count, comparer);

            RaiseCollectionReset();
        }
        #endregion

        #endregion
    }
}
