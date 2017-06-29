using System;

namespace Realms.GroupedCollection
{
    internal class ObservableGrouping<TKey, TValue> : ObservableGroupedCollection<TValue, TValue>, IObservableGrouping<TKey, TValue>
        where TKey : RealmObject
        where TValue : RealmObject
    {
        public TKey Key { get; }

        public ObservableGrouping(TKey key, Func<TKey, IRealmCollection<TValue>> collectionSelector)
            : base(collectionSelector(key), c => c)
        {
            Key = key;
        }
    }
}