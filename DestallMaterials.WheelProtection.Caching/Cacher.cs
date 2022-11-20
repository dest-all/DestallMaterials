using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace DestallMaterials.WheelProtection.Caching
{

    public abstract class Cacher
    {
        public abstract void InvalidateCache();


        public static Cacher<TIn, TOut> Create<TIn, TOut>(Func<TIn, TOut> source, Func<TIn, CachingSettings> getCachingSettings, Func<TIn, int> computeCheckSum)
        {
            var result = new Cacher<TIn, TOut>(source, computeCheckSum, getCachingSettings);
            return result;
        }

        public static Cacher<TResult> Create<TResult>(Func<TResult> source, Func<TimeSpan> getCacheLifetime)
        {
            var result = new Cacher<TResult>(source, getCacheLifetime);
            return result;
        }
    }

    public class Cacher<TIn, TOut>
    {
        readonly Func<TIn, TOut> _source;
        readonly Func<TIn, CachingSettings> _getCachingSettings;

        readonly IDictionary<TIn, CachedValue<TOut>> _caches;

        public Cacher(Func<TIn, TOut> source, Func<TIn, int> computeCheckSum, Func<TIn, CachingSettings> getCachingSettings)
        {
            _getCachingSettings = getCachingSettings;
            _source = source;

            _caches = new ConcurrentDictionary<TIn, CachedValue<TOut>>(new ByChecksumEqualityComparer(computeCheckSum));
        }

        public void InvalidateCache(TIn param)
        {
            _caches.Remove(param);
        }

        public void InvalidateCache()
        {
            _caches.Clear();
        }

        public TOut Run(TIn parameter)
        {
            if (_caches.TryGetValue(parameter, out var result) && result.ValidUntil > DateTime.UtcNow)
            {
                return result.Value;
            }
            return RunDirectly(parameter);
        }
        public TOut RunDirectly(TIn parameter)
        {
            var result = _source.Invoke(parameter);
            _caches[parameter] = new(result, DateTime.UtcNow + _getCachingSettings(parameter).Validity);
            return result;
        }

        private class ByChecksumEqualityComparer : IEqualityComparer<TIn>
        {
            readonly Func<TIn, int> _getChecksum;

            public ByChecksumEqualityComparer(Func<TIn, int> getChecksum)
            {
                _getChecksum = getChecksum;
            }

            public bool Equals(TIn? x, TIn? y)
                => _getChecksum(x) == _getChecksum(y);

            public int GetHashCode([DisallowNull] TIn obj)
                => _getChecksum(obj);
        }
    }

    public class Cacher<TOut>
    {
        readonly Func<TimeSpan> _getCachingSettings;
        readonly Func<TOut> _source;
        CachedValue<TOut>? _lastCallResult;

        public Cacher(Func<TOut> source, Func<TimeSpan> getCachingSettings)
        {
            _getCachingSettings = getCachingSettings;
            _source = source;
        }

        public void InvalidateCache()
        {
            _lastCallResult = null;
        }

        public TOut Run()
        {
            if (_lastCallResult != null && _lastCallResult.Value.ValidUntil > DateTime.UtcNow)
            {
                return _lastCallResult.Value.Value;
            }
            return RunDirectly();
        }
        public TOut RunDirectly()
        {
            var result = _source();
            _lastCallResult = new(result, DateTime.UtcNow + _getCachingSettings());
            return result;
        }
    }

}