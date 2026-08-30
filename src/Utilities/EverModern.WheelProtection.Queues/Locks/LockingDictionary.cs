using System.Collections;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace EverModern.Threading.Locks;

/// <summary>
/// Provides thread-safe, key-level locked access to a dictionary.
/// Each key is locked independently, so different keys do not contend.
/// The lock is held for the lifetime of the returned <see cref="Entry{TKey,TValue}"/>.
/// </summary>
/// <typeparam name="TKey">The type of keys in the dictionary. Must not be null.</typeparam>
/// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
public partial class LockingDictionary<TKey, TValue>(IEqualityComparer<TKey> comparer)
    : IDisposable,
        IReadOnlyDictionary<TKey, TValue>
    where TKey : notnull
{
    readonly KeyLocker<TKey> _locker = new(comparer);
    readonly ConcurrentDictionary<TKey, TValue> _store = new(comparer);

    /// <summary>
    /// Initializes a new instance using the default equality comparer.
    /// </summary>
    public LockingDictionary()
        : this(EqualityComparer<TKey>.Default) { }

    public TValue this[TKey key] => ((IReadOnlyDictionary<TKey, TValue>)_store)[key];

    public IEnumerable<TKey> Keys => ((IReadOnlyDictionary<TKey, TValue>)_store).Keys;

    public IEnumerable<TValue> Values => ((IReadOnlyDictionary<TKey, TValue>)_store).Values;

    public int Count => ((IReadOnlyCollection<KeyValuePair<TKey, TValue>>)_store).Count;

    /// <summary>
    /// Acquires an exclusive lock for <paramref name="key"/> and either retrieves
    /// an existing value or creates one using <paramref name="valueFactory"/>.
    /// The lock is released when the returned entry is disposed or removed.
    /// </summary>
    /// <param name="key">The key to lock and look up.</param>
    /// <param name="valueFactory">A factory that produces a value for the key if one does not exist.</param>
    /// <returns>A <see cref="Entry{TKey,TValue}"/> holding the key-level lock.</returns>
    public Entry<TKey, TValue> Acquire(TKey key, Func<TKey, TValue> valueFactory)
    {
        LockedScope? lockedKey = null;
        try
        {
            lockedKey = _locker.Lock(key);
            var value = _store.GetOrAdd(key, valueFactory);
            return new(_store, key, value, lockedKey);
        }
        catch
        {
            lockedKey?.Finish();
            throw;
        }
    }

    public bool ContainsKey(TKey key)
    {
        return ((IReadOnlyDictionary<TKey, TValue>)_store).ContainsKey(key);
    }

    /// <summary>
    /// Disposes the underlying key locker. Does not affect already-acquired entries.
    /// </summary>
    public void Dispose()
    {
        _locker.Dispose();
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return ((IEnumerable<KeyValuePair<TKey, TValue>>)_store).GetEnumerator();
    }

    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
    {
        return ((IReadOnlyDictionary<TKey, TValue>)_store).TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_store).GetEnumerator();
    }
}
