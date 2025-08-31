using System;
using System.Threading.Tasks;

namespace DestallMaterials.Chronos
{
    public class RealTimeChronos : IChronos
    {
        public static RealTimeChronos Instance = new RealTimeChronos();

        RealTimeChronos()
        { 
        }

        public DateTimeOffset Now => DateTimeOffset.Now;

        public Task WhenComes(DateTimeOffset targetTime)
            => Task.Delay(targetTime - Now);

        public Task WhenComes(DateTime targetTimeUtc)
            => WhenComes(new DateTimeOffset(targetTimeUtc, default(TimeSpan)));

        public Task WhenPasses(TimeSpan time)
            => Task.Delay(time);
    }
}
