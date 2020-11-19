using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ForgeViewer.NET.Misc;
using ForgeViewer.NET.Models;
using Microsoft.JSInterop;

namespace ForgeViewer.NET
{
    public class Viewing
    {
        private Lazy<Task<IJSObjectReference>> ModuleTask { get; }

        public Viewing(IJSRuntime jsRuntime)
        {
            ModuleTask = new Lazy<Task<IJSObjectReference>>(() =>
                jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/ForgeViewer.NET/ForgeViewer.js")
                    .AsTask());
            Functions = new Dictionary<string, object>();
        }

        private Dictionary<string, object> Functions { get; }

        public async Task Initializer(Options? options)
        {
            options ??= new Options();
            if (options.GetAccessToken is { })
                Functions.Set(options.GetAccessTokenId, options.GetAccessToken);


            var module = await ModuleTask.Value;
            await module.InvokeVoidAsync("ViewingInitializer", options, DotNetObjectReference.Create(this));
        }

        [JSInvokable]
        public async Task<object> Callback(string id)
        {
            return await Functions.Get<Func<Task<string>>>(id).Invoke();
        }
    }
}