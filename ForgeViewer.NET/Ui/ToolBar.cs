using Microsoft.JSInterop;

namespace ForgeViewer.NET.Ui
{
    public class ToolBar
    {
        public static ToolBar Create(IJSObjectReference jsObjectReference)
        {
            return new(jsObjectReference);
        }

        private ToolBar(IJSObjectReference jsObjectReference)
        {
            JsToolBar = jsObjectReference;
        }

        public IJSObjectReference JsToolBar { get; }        
    }
}