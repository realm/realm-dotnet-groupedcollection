using System;

namespace Realms.GroupedCollection
{
    internal class RealmCollectionGrouping<TKey, TValue> : RealmObservableCollection<TValue, TValue>, IRealmCollectionGrouping<TKey, TValue>
        where TKey : RealmObject
        where TValue : RealmObject
    {
        public TKey Key { get; }

        public RealmCollectionGrouping(TKey key, Func<TKey, IRealmCollection<TValue>> collectionSelector)
            : base(collectionSelector(key), c => c)
        {
            Key = key;
        }
    }
}
