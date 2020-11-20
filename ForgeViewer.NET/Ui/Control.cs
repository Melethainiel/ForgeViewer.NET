using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using ForgeViewer.NET.Models;
using Microsoft.JSInterop;

namespace ForgeViewer.NET.Ui
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class Control
    {
        #region Ctor

        public static Control Create(IJSObjectReference jsObjectReference)
        {
            return new(jsObjectReference);
        }

        protected Control(IJSObjectReference jsObjectReference)
        {
            JsControl = jsObjectReference;
        }

        public IJSObjectReference JsControl { get; }        

        #endregion
        
        #region Methods

        public async Task<Dimension> GetDimensions()
        {
            return await JsControl.InvokeAsync<Dimension>("getDimensions");
        }
        public async Task<string> GetId()
        {
            return await JsControl.InvokeAsync<string>("getId");
        }
        public async Task<Position> GetPosition()
        {
            return await JsControl.InvokeAsync<Position>("getPosition");
        }
        public async Task<string> GetToolTip()
        {
            return await JsControl.InvokeAsync<string>("getToolTip");
        }
        public async Task<bool> IsCollapsed()
        {
            return await JsControl.InvokeAsync<bool>("isCollapsed");
        }
        public async Task<bool> IsCollapsible()
        {
            return await JsControl.InvokeAsync<bool>("isCollapsible");
        }
        public async Task<bool> IsVisible()
        {
            return await JsControl.InvokeAsync<bool>("isVisible");
        }
        public async Task RemoveClass()
        {
            await JsControl.InvokeVoidAsync("removeClass");
        }
        public async Task<bool> SetCollapsed()
        {
            return await JsControl.InvokeAsync<bool>("setCollapsed");
        }
        public async Task<bool> SetToolTip()
        {
            return await JsControl.InvokeAsync<bool>("setToolTip");
        }
        public async Task<bool> SetVisible()
        {
            return await JsControl.InvokeAsync<bool>("setVisible");
        }

        #endregion
    }
}