using System.Diagnostics.CodeAnalysis;
using Microsoft.JSInterop;

namespace ForgeViewer.NET.Ui
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class ToolBar : GroupControl
    {
        #region Ctor

        protected ToolBar(IJSObjectReference jsObjectReference) : base(jsObjectReference)
        {
        }

        public new static ToolBar Create(IJSObjectReference jsObjectReference)
        {
            return new(jsObjectReference);
        }

        


        #endregion

      
    }
}