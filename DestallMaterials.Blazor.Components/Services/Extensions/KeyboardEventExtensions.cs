using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestallMaterials.Blazor.Services.Extensions
{
    public enum Key
    {
        Alt, Enter, Shift, ArrowUp, ArrowDown
    }
    public static class KeyboardEventExtensions
    {
        static readonly IReadOnlyDictionary<Key, string> KeysAndNames = new Dictionary<Key, string>()
        {
            [Key.ArrowUp] = "ArrowUp",
            [Key.ArrowDown] = "ArrowDown",
            [Key.Enter] = "Enter"
        };
        public static DisposableCallback OnKeyPressed(this IGlobalClickCatcher globalClickCatcher, Key key, Action<KeyboardEventArgs> action)
        {
            return globalClickCatcher.SubscribeForKeyClick(e =>
            {
                if (e.Key == KeysAndNames[key])
                {
                    action(e);
                }
            });
        }
    }
}
