using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Gongchengshi.Collections.Generic
{
    /// <summary>
    /// Defines the Observable Collection interface.
    /// </summary>
    public interface IObservableCollection<T> : 
        ICollection<T>, 
        INotifyCollectionChanged, 
        INotifyPropertyChanged, 
        ICollectionChangeSuspendable
    {
        void RaiseCollectionReset();
    }

    public interface ICollectionChangeSuspendable
    {
        /// <summary>
        /// Used to perform many operations on the collection without raising the CollectionChanged event.
        /// 
        /// A common pattern is as follows:
        /// 
        /// SuspendCollectionChangeNotification();
        /// [a set of collection changing actions]
        /// ResumeCollectionChangeNotification();
        /// </summary>
        void SuspendCollectionChangeNotification();
        void ResumeCollectionChangeNotification();
    }
}
