﻿using DestallMaterials.Blazor.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestallMaterials.Blazor.Services.UI
{
    public interface IUiManipulator
    {
        Task<uint> Y_Deviation(string itemId, string containerId);
        Task<uint> X_Deviation(string itemId, string containerId);
        Task ScrollItem_X(string id, double XPosition);
        Task ScrollItem_Y(string id, double YPosition);
        Task ScrollToFit_Y(string itemId, string containerId);

        Task SetCssVariableValue(string elementId, string variableName, string value);
        Task<double> GetItemScroll_Y(string elementId);
        Task DisableDefaultEventHandling(string elementId, string eventType);
    }
}
