using DestallMaterials.WheelProtection.Caching;

var cachingSettings = new CachingSettings()
{
    MaxSize = 100,
    Validity = TimeSpan.FromSeconds(3)
};
var cacher = new Cacher<int, Task<int>>(async n =>
{
    Console.WriteLine($"Launched calculation of square {n}.");
    await Task.Delay(1000);
    return n * n;
}, n => n, n => cachingSettings);

for (int i = 0; i < 100; i++)
{
    await Task.Delay(300);
    Console.WriteLine(await cacher.Run(3));
}