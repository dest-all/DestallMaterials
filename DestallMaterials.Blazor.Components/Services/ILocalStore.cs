using Common.Extensions.Object;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Web.View.Services
{
    public interface ILocalStore
    {
        Task PutAsync<TItem>(string key, TItem value);
        Task<TItem> GetAsync<TItem>(string key);
        Task DeleteAsync<TItem>(string key);
    }

    public class BrowserLocalStorage : ILocalStore
    {
        readonly IJSRuntime jsRuntime;

        static class CommandNames
        {
            public const string AddToStore = "AddToStore";
            public const string GetFromStore = "GetFromStore";
            public const string DeleteFromStore = "DeleteFromStore";
        }

        public BrowserLocalStorage(IJSRuntime jSRuntime)
        {
            this.jsRuntime = jSRuntime;
        }

        public async Task DeleteAsync<TItem>(string key)
        {
            await jsRuntime.InvokeVoidAsync(CommandNames.DeleteFromStore);
        }

        public async Task<TItem> GetAsync<TItem>(string key)
        {
            try
            {
                var stringResult = await jsRuntime.InvokeAsync<string>(CommandNames.GetFromStore, key);
                var result = JsonConvert.DeserializeObject<TItem>(stringResult);
                return result;
            }
            catch (Exception ex)
            {
                return default(TItem);
            }
        }

        public async Task PutAsync<TItem>(string key, TItem value)
        {
            await jsRuntime.InvokeVoidAsync(CommandNames.AddToStore, key, value.ToJson());
        }
    }
}
