using System;
using System.Collections.Generic;

namespace Realms.GroupedCollection
{
    internal class GroupedCollection<TKey, TValue>
        : ObservableGroupedCollection<TKey, ObservableGrouping<TKey, TValue>>, IGroupedCollection<TKey, TValue>
        where TKey : RealmObject
        where TValue : RealmObject
    {
        public GroupedCollection(IRealmCollection<TKey> objects, Func<TKey, IRealmCollection<TValue>> valueSelector)
            : base(objects, o => new ObservableGrouping<TKey, TValue>(o, valueSelector))
        {
        }

        IObservableGrouping<TKey, TValue> IReadOnlyList<IObservableGrouping<TKey, TValue>>.this[int index] => this[index];

        IEnumerator<IObservableGrouping<TKey, TValue>> IEnumerable<IObservableGrouping<TKey, TValue>>.GetEnumerator() => GetEnumerator();
    }
}