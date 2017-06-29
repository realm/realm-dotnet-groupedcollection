using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Realms.GroupedCollection
{
    /// <summary>
    /// An observable collection of <see cref="IObservableGrouping{TKey,TValue}"/>.
    /// </summary>
    /// <typeparam name="TKey">The type of the key by which the collections will be grouped.</typeparam>
    /// <typeparam name="TValue">The type of the objects, contained in the grouped collections.</typeparam>
    public interface IGroupedCollection<TKey, TValue>
        : IReadOnlyList<IObservableGrouping<TKey, TValue>>, INotifyPropertyChanged, INotifyCollectionChanged
    {
    }
}