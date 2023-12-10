using System.Runtime.CompilerServices;

namespace DestallMaterials.WheelProtection.Extensions.Time
{
    public static class TimeExtensions
    {
        public static TaskAwaiter GetAwaiter(this TimeSpan time)
           => Task.Delay(time).GetAwaiter();

        public static TaskAwaiter GetAwaiter(this DateTime dateTime)
            => Task.Delay(dateTime - DateTime.UtcNow).GetAwaiter();
    }
}
