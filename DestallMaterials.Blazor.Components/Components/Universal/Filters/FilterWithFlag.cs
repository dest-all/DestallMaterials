using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestallMaterials.Blazor.Components.Universal.Filters
{
    public struct ValueWithFlag<TValue>
    {
        public TValue Value { get; init; }
        public bool Filters { get; init; }
    }
}
