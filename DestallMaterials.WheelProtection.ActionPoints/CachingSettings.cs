namespace DestallMaterials.WheelProtection.ActionPoints
{
    public struct CachingSettings
    {
        public TimeSpan Validity { get; init; }
        public readonly int MaxSize { get; init; }
    }

}