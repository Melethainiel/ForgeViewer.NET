using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace ForgeViewer.NET.Viewing
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class Document
    {
        public IJSObjectReference JsDocument { get; }

        private Document(IJSObjectReference jsObjectReference)
        {
            JsDocument = jsObjectReference;
        }


        public static async Task Load(IServiceProvider serviceProvider, string urn,
            Func<Document, Task>? onSuccess = null, Func<string, Task>? onFailure = null)
        {
            var jsRuntime = serviceProvider.GetRequiredService<IJSRuntime>();
            var module =
                await jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/ForgeViewer.NET/ForgeViewer.js");
            try
            {
                var reference = await module.InvokeAsync<IJSObjectReference>("loadDocument", urn);
                var doc = new Document(reference);
                if (onSuccess is { })
                    await onSuccess.Invoke(doc);
            }
            catch (JSException e)
            {
                if (onFailure is { })
                    await onFailure.Invoke(e.Message);
            }
        }

        public async Task<BubbleNode> GetRoot()
        {
            var reference = await JsDocument.InvokeAsync<IJSObjectReference>("getRoot");
            return BubbleNode.Create(reference);
        }
    }
}