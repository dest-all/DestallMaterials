using System.Collections.Concurrent;

namespace EverModern.Threading.Locks;

public partial class LockingDictionary<TKey, TValue> where TKey : notnull
{
    /// <summary>
    /// Represents a key-value pair with an exclusive lock on the dictionary entry.
    /// The lock is released when the entry is disposed or removed.
    /// After disposal, properties and methods throw <see cref="ObjectDisposedException"/>.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class Entry<TKey, TValue>(
        ConcurrentDictionary<TKey, TValue> store,
        TKey key,
        TValue? dictValue,
        LockedScope locker
    ) : IDisposable where TKey : notnull
    {
        readonly Lock _disposedLock = new();
        bool _disposed;

        T ThrowIfDisposed<T>(T returnedValue)
        {
            if (_disposed)
            {
                ObjectDisposedException.ThrowIf(_disposed, this);
            }

            return returnedValue;
        }

        /// <summary>
        /// Gets the key associated with this entry.
        /// </summary>
        public TKey Key => key;

        /// <summary>
        /// Gets or sets the value for this entry.
        /// Setting the value also updates the underlying dictionary.
        /// </summary>
        /// <exception cref="ObjectDisposedException">Thrown when the entry has been disposed.</exception>
        public TValue? Value
        {
            get => ThrowIfDisposed(dictValue);
            set
            {
                dictValue = ThrowIfDisposed(value);
                store[key] = value;
            }
        }

        /// <summary>
        /// Removes the entry from the underlying dictionary and releases the lock.
        /// Safe to call multiple times — subsequent calls are no-ops.
        /// </summary>
        public void Remove()
        {
            if (_disposedLock.TryEnter() == false)
                return;

            _disposed = true;
            store.Remove(key, out _);
            locker.Finish();
        }

        /// <summary>
        /// Releases the lock without removing the entry from the underlying dictionary.
        /// Safe to call multiple times — subsequent calls are no-ops.
        /// </summary>
        public void Dispose()
        {
            if (_disposedLock.TryEnter() == false)
                return;

            _disposed = true;
            locker.Finish();
        }
    }

}
