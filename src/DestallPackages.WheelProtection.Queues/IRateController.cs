using System;
using System.Threading;
using System.Threading.Tasks;

namespace DestallMaterials.WheelProtection.Queues;

public interface IRateController
{
    ValueTask<IDisposable> AwaitNext(CancellationToken cancellationToken);
}
