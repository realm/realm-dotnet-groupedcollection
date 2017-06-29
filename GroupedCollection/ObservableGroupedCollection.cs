using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Realms.GroupedCollection
{
    internal abstract class ObservableGroupedCollection<TKey, TValue>
        : IReadOnlyList<TValue>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        private readonly IRealmCollection<TKey> _collection;
        private readonly Func<TKey, TValue> _converter;

        private event NotifyCollectionChangedEventHandler _collectionChanged;

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add
            {
                if (_collectionChanged == null)
                {
                    _collection.CollectionChanged += OnCollectionChanged;
                }

                _collectionChanged += value;
            }
            remove
            {
                _collectionChanged -= value;

                if (_collectionChanged == null)
                {
                    _collection.CollectionChanged -= OnCollectionChanged;
                }
            }
        }

        private event PropertyChangedEventHandler _propertyChanged;

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                if (_propertyChanged == null)
                {
                    _collection.PropertyChanged += OnCollectionPropertyChanged;
                }

                _propertyChanged += value;
            }
            remove
            {
                _collection.PropertyChanged -= value;

                if (_propertyChanged == null)
                {
                    _collection.PropertyChanged -= OnCollectionPropertyChanged;
                }
            }
        }

        public int Count => _collection.Count;

        public TValue this[int index] => _converter(_collection[index]);

        protected ObservableGroupedCollection(IRealmCollection<TKey> collection, Func<TKey, TValue> converter)
        {
            _collection = collection;
            _converter = converter;
        }

        public IEnumerator<TValue> GetEnumerator() => new Enumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void OnCollectionPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e);
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (typeof(TKey) != typeof(TValue))
            {
                // The parent collection has changed - since this could reorder children, raise Reset to avoid
                // inconsistencies.
                RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                return;
            }

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Move:
                    var moves = e.NewItems.Cast<TKey>().Select(_converter).ToArray();
                    var moveArgs = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, moves, e.NewStartingIndex, e.OldStartingIndex);
                    RaiseCollectionChanged(moveArgs);
                    break;
                case NotifyCollectionChangedAction.Add:
                    var adds = e.NewItems.Cast<TKey>().Select(_converter).ToArray();
                    var addArgs = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, adds, e.NewStartingIndex);
                    RaiseCollectionChanged(addArgs);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    var removes = e.OldItems.Cast<TKey>().Select(_ => default(TValue)).ToArray();
                    var removeArgs = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removes, e.OldStartingIndex);
                    RaiseCollectionChanged(removeArgs);
                    break;
                default:
                    RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                    break;
            }
        }

        protected void RaisePropertyChanged(PropertyChangedEventArgs args)
        {
            _propertyChanged?.Invoke(this, args);
        }

        protected void RaiseCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            _collectionChanged?.Invoke(this, args);
        }

        private class Enumerator : IEnumerator<TValue>
        {
            private readonly ObservableGroupedCollection<TKey, TValue> _collection;
            private int _index;

            internal Enumerator(ObservableGroupedCollection<TKey, TValue> collection)
            {
                _index = -1;
                _collection = collection;
            }

            public TValue Current => _collection[_index];

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                var index = _index + 1;
                if (index >= _collection.Count)
                {
                    return false;
                }

                _index = index;
                return true;
            }

            public void Reset()
            {
                _index = -1;
            }

            public void Dispose()
            {
            }
        }
    }
}