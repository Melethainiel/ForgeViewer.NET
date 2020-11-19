using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace ForgeViewer.NET
{
    public class BubbleNode
    {
        public static BubbleNode Create(IJSObjectReference jsObjectReference)
        {
            return new(jsObjectReference);
        }

        private BubbleNode(IJSObjectReference jsObjectReference)
        {
            JsBubbleNode = jsObjectReference;
        }

        public IJSObjectReference JsBubbleNode { get; }

        public async Task<BubbleNode> GetDefaultGeometry(bool searchMasterView = false, bool loadLargestView = false)
        {
            var reference = await JsBubbleNode.InvokeAsync<IJSObjectReference>("getDefaultGeometry", searchMasterView, loadLargestView);
            return new BubbleNode(reference);
        }
    }
}