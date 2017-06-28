using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Realms.GroupedCollection
{
    /// <summary>
    /// An observable key-value grouping, similar to System.Linq.IGrouping.
    /// </summary>
    public interface IRealmCollectionGrouping<TKey, TValue>
        : IReadOnlyList<TValue>, INotifyPropertyChanged, INotifyCollectionChanged
    {
        /// <summary>
        /// Gets the key of the grouping.
        /// </summary>
        TKey Key { get; }
    }

    /// <summary>
    /// An observable collection of <see cref="IRealmCollectionGrouping{TKey,TValue}"/>.
    /// </summary>
    public interface IGroupedCollection<TKey, TValue>
        : IReadOnlyList<IRealmCollectionGrouping<TKey, TValue>>, INotifyPropertyChanged, INotifyCollectionChanged
    {
    }
}