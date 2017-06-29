using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Realms.GroupedCollection
{
    /// <summary>
    /// An observable key-value grouping, similar to System.Linq.IGrouping.
    /// </summary>
    /// <typeparam name="TKey">The type of the key by which the collections will be grouped.</typeparam>
    /// <typeparam name="TValue">The type of the objects, contained in the grouped collections.</typeparam>
    public interface IObservableGrouping<TKey, TValue>
        : IReadOnlyList<TValue>, INotifyPropertyChanged, INotifyCollectionChanged
    {
        /// <summary>
        /// Gets the key of the grouping.
        /// </summary>
        TKey Key { get; }
    }
}