using DestallMaterials.WheelProtection.Queues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DestallMaterials.Tests
{

    class ServerEmulator
    {
        Recycler<RequestProcessor> _recycler = new RequestsProcessorRecycler();
        public async Task<long> ProcessRequestAsync(TimeSpan operationExecutionLength)
        {
            using var processorLocker = await _recycler.AwaitAnother();
            await processorLocker.Item.ProcessRequestAsync(operationExecutionLength);
            return operationExecutionLength.Ticks;
        }

        class RequestProcessor : IDisposable
        {
            bool _isBusy = false;

            public void Dispose()
            {

            }

            public async Task ProcessRequestAsync(TimeSpan operationExecutionLength)
            {
                if (_isBusy)
                {
                    throw new InvalidOperationException();
                }
                _isBusy = true;
                await Task.Delay(operationExecutionLength);
                _isBusy = false;
            }
        }

        class RequestsProcessorRecycler : Recycler<RequestProcessor>
        {
            public RequestsProcessorRecycler() : base(1)
            {
            }

            protected override RequestProcessor CreateNew()
                => new RequestProcessor();

            protected override void Discard(RequestProcessor item)
            => item.Dispose();

            protected override bool IsWell(RequestProcessor item)
            => true;
        }
    }
}
