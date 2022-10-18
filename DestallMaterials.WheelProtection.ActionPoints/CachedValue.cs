﻿namespace DestallMaterials.WheelProtection.ActionPoints
{
    internal struct CachedValue<TValue>
    {
        public readonly TValue Value;
        public readonly DateTime ValidUntil;

        public CachedValue(TValue value, DateTime dateCreated)
        {
            Value = value;
            ValidUntil = dateCreated;
        }
    }

}