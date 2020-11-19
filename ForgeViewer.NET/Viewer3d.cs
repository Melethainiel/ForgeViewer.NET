using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace ForgeViewer.NET
{
    public class Viewer3d
    {
        private IJSObjectReference? _jsViewer;
        protected Lazy<Task<IJSObjectReference>> ModuleTask { get; }

        protected IJSObjectReference JsViewer
        {
            get => _jsViewer ?? throw new ArgumentNullException();
            set => _jsViewer = value;
        }

        public static Viewer3d Create(IServiceProvider serviceProvider)
        {
            return new(serviceProvider.GetRequiredService<IJSRuntime>());
        }

        protected Viewer3d(IJSRuntime jsRuntime)
        {
            ModuleTask = new Lazy<Task<IJSObjectReference>>(() =>
                jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/ForgeViewer.NET/ForgeViewer.js")
                    .AsTask());
        }
        
        public async Task Initializer(string id)
        {
            var module = await ModuleTask.Value;
            JsViewer = await module.InvokeAsync<IJSObjectReference>("Viewer3dInitializer", id);
        }

        public async Task Start()
        {
            await JsViewer.InvokeVoidAsync("start");
        }

        public async Task LoadDocumentNode(Document document, BubbleNode manifestNode)
        {
            await JsViewer.InvokeAsync<string>("loadDocumentNode", document.JsDocument, manifestNode.JsBubbleNode);
        }
    }
}