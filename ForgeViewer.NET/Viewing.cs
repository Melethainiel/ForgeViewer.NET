using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ForgeViewer.NET.Misc;
using ForgeViewer.NET.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace ForgeViewer.NET
{
    public class Viewing
    {
        private Lazy<Task<IJSObjectReference>> ModuleTask { get; }
        private Func<Task<string>>? _getAccessToken;
        public static Viewing Create(IServiceProvider serviceProvider)
        {
            return new(serviceProvider.GetRequiredService<IJSRuntime>());
            
        }
        private Viewing(IJSRuntime jsRuntime)
        {
            ModuleTask = new Lazy<Task<IJSObjectReference>>(() =>
                jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/ForgeViewer.NET/ForgeViewer.js")
                    .AsTask());
        }


        public async Task Initializer(Options? options)
        {
            options ??= new Options();
            if (options.GetAccessToken is { })
                _getAccessToken= options.GetAccessToken;


            var module = await ModuleTask.Value;
            await module.InvokeVoidAsync("ViewingInitializer", options, DotNetObjectReference.Create(this));
        }

        [JSInvokable]
        public async Task<object> GetAccessToken()
        {
            return await (_getAccessToken?.Invoke() ?? throw new Exception("No function set"));
        }
    }
}