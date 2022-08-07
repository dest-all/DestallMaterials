using System;

namespace DestallMaterials.Blazor.Components.Universal.Filters
{
    public abstract partial class LeftRightLimitPropertyFilter<TValue>
        where TValue : IComparable<TValue>
    {
        protected ValueWithFlag<LeftRightValuePair<TValue>> FilterValue 
            => new ValueWithFlag<LeftRightValuePair<TValue>>() { Filters = Filters, Value = Value };
    }
}