using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace DestallMaterials.WheelProtection.Caching
{

    public abstract class Cacher
    {
        public abstract void InvalidateCache();


        public static Cacher<TParameter, TResult> Create<TParameter, TResult>(Func<TParameter, TResult> source, Func<TParameter, CachingSettings> getCachingSettings, Func<TParameter, int> computeCheckSum)
        {
            var result = new Cacher<TParameter, TResult>(source, getCachingSettings, computeCheckSum);
            return result;
        }

        public static Cacher<TResult> Create<TResult>(Func<TResult> source, Func<TimeSpan> getCacheLifetime)
        {
            var result = new Cacher<TResult>(source, getCacheLifetime);
            return result;
        }
    }

    public class Cacher<TParameter, TResult>
    {
        readonly Func<TParameter, TResult> _source;
        readonly Func<TParameter, CachingSettings> _getCachingSettings;

        readonly IDictionary<TParameter, CachedValue<TResult>> _caches;

        public Cacher(Func<TParameter, TResult> source, Func<TParameter, CachingSettings> getCachingSettings, Func<TParameter, int> computeCheckSum)
        {
            _getCachingSettings = getCachingSettings;
            _source = source;

            _caches = new ConcurrentDictionary<TParameter, CachedValue<TResult>>(new ByChecksumEqualityComparer(computeCheckSum));
        }

        public void InvalidateCache()
        {
            _caches.Clear();
        }

        public TResult Run(TParameter parameter)
        {
            if (_caches.TryGetValue(parameter, out var result) && result.ValidUntil < DateTime.UtcNow)
            {
                return result.Value;
            }
            return RunDirectly(parameter);
        }
        public TResult RunDirectly(TParameter parameter)
        {
            var result = _source.Invoke(parameter);
            _caches[parameter] = new(result, DateTime.UtcNow + _getCachingSettings(parameter).Validity);
            return result;
        }

        private class ByChecksumEqualityComparer : IEqualityComparer<TParameter>
        {
            readonly Func<TParameter, int> _getChecksum;

            public ByChecksumEqualityComparer(Func<TParameter, int> getChecksum)
            {
                _getChecksum = getChecksum;
            }

            public bool Equals(TParameter? x, TParameter? y)
                => _getChecksum(x) == _getChecksum(y);

            public int GetHashCode([DisallowNull] TParameter obj)
                => _getChecksum(obj);
        }
    }

    public class Cacher<TResult>
    {
        readonly Func<TimeSpan> _getCachingSettings;
        readonly Func<TResult> _source;
        CachedValue<TResult>? _lastCallResult;

        public Cacher(Func<TResult> source, Func<TimeSpan> getCachingSettings)
        {
            _getCachingSettings = getCachingSettings;
            _source = source;
        }

        public void InvalidateCache()
        {
            _lastCallResult = null;
        }

        public TResult Run()
        {
            if (_lastCallResult != null && _lastCallResult.Value.ValidUntil >= DateTime.UtcNow)
            {
                return _lastCallResult.Value.Value;
            }
            return RunDirectly();
        }
        public TResult RunDirectly()
        {
            var result = _source();
            _lastCallResult = new(result, DateTime.UtcNow + _getCachingSettings());
            return result;
        }
    }

}