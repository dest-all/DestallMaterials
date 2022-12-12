using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestallMaterials.WheelProtection.DataStructures.Time
{
    public struct DateTimeRange
    {
#if NETSTANDARD2_0
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
#else
        public DateTime Start { get; init; }
        public DateTime End { get; init; }
#endif
    }
}
