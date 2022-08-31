using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestallMaterials.Blazor.Services.UI
{
    public interface IUiManipulator
    {
        void ScrollItem_XAxis(string id, double deltaX);
        void ScrollItem_YAxis(string id, double deltaY);

        Task SayHi(string name);
    }
}
