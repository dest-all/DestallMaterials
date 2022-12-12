using DestallMaterials.WheelProtection.Queues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestallMaterials.Tests
{
    public class QueusTesting
    {
        ServerEmulator _serverEmulator = new ServerEmulator();
        [Test]
        public async Task Simple()
        {
            var rand = new Random(10);

            await Task.WhenAll(Enumerable.Range(0, 10).Select(i => Task.Run(async () =>
            {
                var payload = TimeSpan.FromMilliseconds(rand.Next(100) * 10);
                var response = await _serverEmulator.ProcessRequestAsync(payload);
                Console.WriteLine($"{DateTime.Now} ===> Request processed with response {response}.");
            })).ToArray());
        }
    }
}
