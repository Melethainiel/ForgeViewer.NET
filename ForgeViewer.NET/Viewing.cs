using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace ForgeViewer.NET
{
    public class Viewing
    {
        private Lazy<Task<IJSObjectReference>> ModuleTask { get; }

        public static Viewing Create(IServiceProvider serviceProvider)
        {
            return new(serviceProvider.GetRequiredService<IJSRuntime>());
        }
        
        private Viewing(IJSRuntime jsRuntime)
        {
            ModuleTask = new Lazy<Task<IJSObjectReference>>(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/ForgeViewer.NET/exampleJsInterop.js").AsTask());
        }

        
        public async Task Initializer(object options)
        {
            var module = await ModuleTask.Value;
            await module.InvokeVoidAsync("ViewingInitializer", options, DotNetObjectReference.Create(this));
        }
    }
}