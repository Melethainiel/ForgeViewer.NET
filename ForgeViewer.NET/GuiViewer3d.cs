using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace ForgeViewer.NET
{
    public class GuiViewer3d : Viewer3d
    {

        public new static GuiViewer3d Create(IServiceProvider serviceProvider)
        {
            return new(serviceProvider.GetRequiredService<IJSRuntime>());
        }
        
        private GuiViewer3d(IJSRuntime jsRuntime) : base(jsRuntime) { }
        
        public new async Task Initializer(string id)
        {
            var module = await ModuleTask.Value;
            JsViewer = await module.InvokeAsync<IJSObjectReference>("GuiViewer3dInitializer", id);
        }
    }
}