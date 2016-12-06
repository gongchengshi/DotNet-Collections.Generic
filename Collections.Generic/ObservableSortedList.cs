using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Gongchengshi.Collections.Generic
{
    /// <summary>
    /// SortedList which implements INotifyCollectionChanged interface.
    /// 
    /// Adapted from http://softcollections.codeplex.com/
    /// 
    /// All methods have the same API as List(T)
    /// </summary>
    public class ObservableSortedList<T> : SortedList<T>, IObservableCollection<T>
    {
        public ObservableSortedList()
        { }

        public ObservableSortedList(IComparer<T> comparer) : base(comparer) { }

        #region INotifyPropertyChanged and IObservableCollection implementation and helpers
        public event NotifyCollectionChangedEventHandler CollectionChanged = delegate {};
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

        private void RaiseCollectionChanged(NotifyCollectionChangedAction action, T item, int index)
        {
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
        }

        private void RaiseCollectionChanged(NotifyCollectionChangedAction action, T oldItem, T newItem, int index)
        {
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
        }

        public void RaiseCollectionReset()
        {
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        protected virtual void RaisePropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged(this, e);
        }

        private void RaisePropertyChanged(string propertyName)
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

        protected override void Insert(int index, T value)
        {
            base.Insert(index, value);

            RaiseCollectionChanged(NotifyCollectionChangedAction.Add, value, index);
        }

        public override void RemoveAt(int index)
        {
            var item = this[index];
            base.RemoveAt(index);

            RaiseCollectionChanged(NotifyCollectionChangedAction.Remove, item, index);
        }

        public override T this[int index]
        {
            get { return base[index]; }
            set
            {
                var oldItem = base[index];
                base[index] = value;

                RaiseCollectionChanged(NotifyCollectionChangedAction.Replace, oldItem, value, index);
            }
        }

        public override void Clear()
        {
            base.Clear();
            RaiseCollectionReset();
        }
    }
}
