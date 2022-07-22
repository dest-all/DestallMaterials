using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestallMaterials.WheelProtection.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsOneOf<T>(this T item, IEnumerable<T> items)
        {
            return items.Contains(item);
        }

        public static bool IsOneOf<T>(this T item, params T[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Equals(item))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
