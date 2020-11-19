using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace ForgeViewer.NET
{
    public class Document
    { 
        public IJSObjectReference JsDocument { get;  }

        private Document(IJSObjectReference jsObjectReference)
        {
            JsDocument = jsObjectReference;
        }
        public static async Task<Document> Load(IServiceProvider serviceProvider, string urn)
        {
            var jsRuntime = serviceProvider.GetRequiredService<IJSRuntime>();
            var module = await jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/ForgeViewer.NET/ForgeViewer.js");
            var reference = await module.InvokeAsync<IJSObjectReference>("loadDocument", urn);
            
            return new Document(reference);
        }

        public async Task<BubbleNode> GetRoot()
        {
            var reference = await JsDocument.InvokeAsync<IJSObjectReference>("getRoot");
            return BubbleNode.Create(reference);
        }
        
    }
}