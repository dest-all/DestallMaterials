using Client.Web.View.Components;
using Common.Extensions.String;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Web.View.Services
{
    public static partial class StandardRenderer
    {
        public static class PageComponentTypes
        {
            public const string List = "List";
            public const string Item = "Item";
        }
        public static RenderFragment RenderPage(string entityType, string pageComponentTypeString)
        {
            var componentType = PickComponentForPage(entityType, pageComponentTypeString);

            return builder =>
            {
                builder.OpenComponent(1, componentType);
            };
        }

        public static Type PickComponentForPage(string entityType, string pageComponentTypeString)
        {
            entityType = entityType.ToLower().Capitalize();
            if (!StandardComponentsByEntities.ContainsKey(entityType))
            {
                throw new ArgumentException($"Entity type {entityType} does not have its own page.");
            }
            pageComponentTypeString = pageComponentTypeString.ToLower().Capitalize();
            var components = StandardComponentsByEntities[entityType];

            var componentType = pageComponentTypeString switch
            {
                PageComponentTypes.List => components.ListPage
                ,
                PageComponentTypes.Item => components.EditPage
                ,
                _ => throw new NotImplementedException()
            };

            return componentType;
        }
    }
}
