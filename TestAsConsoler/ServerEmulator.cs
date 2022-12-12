using DestallMaterials.WheelProtection.Queues;

public class ServerEmulator
{
    readonly IEnumerable<KeyValuePair<TimeSpan, int>> _constraints;
    readonly List<RateController.StartAndFinishTime> _processed = new List<RateController.StartAndFinishTime>();

    object _lock = new object();

    public ServerEmulator(IEnumerable<KeyValuePair<TimeSpan, int>> constraints)
    {
        _constraints = constraints.OptimizeConstraints();
    }

    public async Task<long> ProcessRequestAsync(TimeSpan executionLength)
    {
        await Task.Delay(150);
        var date = DateTime.UtcNow;
        lock (_lock)
        {
            if (_constraints.Any(constraint => _processed.Count(p =>
            {
                bool notFinished = p.Finish == default;
                bool finishedJustYet = date - p.Finish < constraint.Key;
                bool startedJustYet = (date - p.Start < constraint.Key);
                return (notFinished || finishedJustYet || startedJustYet);
            }) >= constraint.Value))
            {
                throw new Exception();
            }
        }
        var times = new RateController.StartAndFinishTime();
        times.Start = date;
        _processed.Add(times);
        await Task.Delay(executionLength);
        times.Finish = DateTime.UtcNow;
        return executionLength.Ticks;
    }
}