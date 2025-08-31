using System;

namespace DestallMaterials.WheelProtection.Queues;

public class RateControlLocker : IDisposable
{
    private readonly Action _onDisposed;

    public RateControlLocker(Action onDisposed)
    {
        _onDisposed = onDisposed;
    }

    public void Release()
    {
        _onDisposed();
    }

    void IDisposable.Dispose() => Release();
}
