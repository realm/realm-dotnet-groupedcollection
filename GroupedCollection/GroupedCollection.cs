using System;
using System.Collections.Generic;

namespace Realms.GroupedCollection
{
    internal class GroupedRealmCollection<TKey, TValue>
        : RealmObservableCollection<TKey, RealmCollectionGrouping<TKey, TValue>>, IGroupedCollection<TKey, TValue>
        where TKey : RealmObject
        where TValue : RealmObject
    {
        public GroupedRealmCollection(IRealmCollection<TKey> objects, Func<TKey, IRealmCollection<TValue>> valueSelector)
            : base(objects, o => new RealmCollectionGrouping<TKey, TValue>(o, valueSelector))
        {
        }

        IRealmCollectionGrouping<TKey, TValue> IReadOnlyList<IRealmCollectionGrouping<TKey, TValue>>.this[int index] => this[index];

        IEnumerator<IRealmCollectionGrouping<TKey, TValue>> IEnumerable<IRealmCollectionGrouping<TKey, TValue>>.GetEnumerator() => GetEnumerator();
    }
}
