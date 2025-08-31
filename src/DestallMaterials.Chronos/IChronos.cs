using System;
using System.Threading.Tasks;

namespace DestallMaterials.Chronos
{
    public interface IChronos
    {
        DateTimeOffset Now { get; }

        Task WhenPasses(TimeSpan time);

        Task WhenComes(DateTimeOffset targetTime);

        Task WhenComes(DateTime targetTimeUtc);
    }

    public interface IChronosControll
    {
        void SetTime(DateTimeOffset newNow);

        void MoveTime(TimeSpan moveForward);

        void SetSpeed(decimal newRelativeSpeed);
    }
}
