using DestallMaterials.WheelProtection.Queues;


var constraints = new Dictionary<TimeSpan, int>
{
    {
        TimeSpan.FromSeconds(0.5), 3
    }
};

var rateController = new RateController(constraints);

var rand = new Random();
for (int i = 0; i < 100; i++)
{
    using (await rateController.WaitNext())
    {
        Console.WriteLine(DateTime.Now);
    }
}
