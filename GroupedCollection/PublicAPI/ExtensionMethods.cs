using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Realms;
using Realms.GroupedCollection;

/// <summary>
/// A set of extension methods over Realm collections to generate grouped collections.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class ExtensionMethods
{
    internal static IGroupedCollection<TKey, TValue> ToGroupedCollection<TKey, TValue>(this IRealmCollection<TKey> collection, Func<TKey, IRealmCollection<TValue>> valueSelector)
        where TKey : RealmObject
        where TValue : RealmObject
    {
        return new GroupedCollection<TKey, TValue>(collection, valueSelector);
    }

    internal static IGroupedCollection<TKey, TValue> ToGroupedCollection<TKey, TValue>(this IQueryable<TKey> collection, Func<TKey, IRealmCollection<TValue>> valueSelector)
        where TKey : RealmObject
        where TValue : RealmObject
    {
        return collection.AsRealmCollection().ToGroupedCollection(valueSelector);
    }

    internal static IGroupedCollection<TKey, TValue> ToGroupedCollection<TKey, TValue>(this IList<TKey> collection, Func<TKey, IRealmCollection<TValue>> valueSelector)
        where TKey : RealmObject
        where TValue : RealmObject
    {
        return collection.AsRealmCollection().ToGroupedCollection(valueSelector);
    }

    /// <summary>
    /// Creates an observable grouped collection from the Realm query.
    /// </summary>
    /// <returns>The grouped collection.</returns>
    /// <param name="collection">The query to group.</param>
    /// <param name="valueSelector">A projection function to obtain the child collections.</param>
    /// <typeparam name="TKey">The type of the key by which the collections will be grouped.</typeparam>
    /// <typeparam name="TValue">The type of the objects, contained in the grouped collections.</typeparam>
    public static IGroupedCollection<TKey, TValue> ToGroupedCollection<TKey, TValue>(this IQueryable<TKey> collection, Func<TKey, IQueryable<TValue>> valueSelector)
        where TKey : RealmObject
        where TValue : RealmObject
    {
        return collection.AsRealmCollection().ToGroupedCollection(key => valueSelector(key).AsRealmCollection());
    }

    /// <summary>
    /// Creates an observable grouped collection from the Realm query.
    /// </summary>
    /// <returns>The grouped collection.</returns>
    /// <param name="collection">The query to group.</param>
    /// <param name="valueSelector">A projection function to obtain the child collections.</param>
    /// <typeparam name="TKey">The type of the key by which the collections will be grouped.</typeparam>
    /// <typeparam name="TValue">The type of the objects, contained in the grouped collections.</typeparam>
    public static IGroupedCollection<TKey, TValue> ToGroupedCollection<TKey, TValue>(this IQueryable<TKey> collection, Func<TKey, IList<TValue>> valueSelector)
        where TKey : RealmObject
        where TValue : RealmObject
    {
        return collection.AsRealmCollection().ToGroupedCollection(key => valueSelector(key).AsRealmCollection());
    }

    /// <summary>
    /// Creates an observable grouped collection from the Realm list.
    /// </summary>
    /// <returns>The grouped collection.</returns>
    /// <param name="collection">The list to group.</param>
    /// <param name="valueSelector">A projection function to obtain the child collections.</param>
    /// <typeparam name="TKey">The type of the key by which the collections will be grouped.</typeparam>
    /// <typeparam name="TValue">The type of the objects, contained in the grouped collections.</typeparam>
    public static IGroupedCollection<TKey, TValue> ToGroupedCollection<TKey, TValue>(this IList<TKey> collection, Func<TKey, IQueryable<TValue>> valueSelector)
        where TKey : RealmObject
        where TValue : RealmObject
    {
        return collection.AsRealmCollection().ToGroupedCollection(key => valueSelector(key).AsRealmCollection());
    }

    /// <summary>
    /// Creates an observable grouped collection from the Realm list.
    /// </summary>
    /// <returns>The grouped collection.</returns>
    /// <param name="collection">The list to group.</param>
    /// <param name="valueSelector">A projection function to obtain the child collections.</param>
    /// <typeparam name="TKey">The type of the key by which the collections will be grouped.</typeparam>
    /// <typeparam name="TValue">The type of the objects, contained in the grouped collections.</typeparam>
    public static IGroupedCollection<TKey, TValue> ToGroupedCollection<TKey, TValue>(this IList<TKey> collection, Func<TKey, IList<TValue>> valueSelector)
        where TKey : RealmObject
        where TValue : RealmObject
    {
        return collection.AsRealmCollection().ToGroupedCollection(key => valueSelector(key).AsRealmCollection());
    }
}