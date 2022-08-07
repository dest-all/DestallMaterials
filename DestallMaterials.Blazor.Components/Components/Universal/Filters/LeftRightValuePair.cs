using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DestallMaterials.Blazor.Components.Universal.Filters
{
    public struct LeftRightValuePair<T> where T : IComparable<T>
    {
        public T Left;
        public T Right;

        public bool RightLessThanLeft => Left.CompareTo(Right) > 0;

        public LeftRightValuePair(LeftRightValuePair<T> other)
        {
            Left = other.Left;
            Right = other.Right;
        }
    }
}
