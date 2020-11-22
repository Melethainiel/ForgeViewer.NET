using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using ForgeViewer.NET.Ui;
using Microsoft.JSInterop;

namespace ForgeViewer.NET.Viewing
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class Extension
    {
        #region Ctor

        public static Extension Create(IJSObjectReference jsObjectReference)
        {
            return new(jsObjectReference);
        }

        private Extension(IJSObjectReference jsObjectReference)
        {
            JsExtension = jsObjectReference;
        }

        public IJSObjectReference JsExtension { get; }
        

        #endregion

        #region Methods

        public async Task<bool> Load()
        {
            return await JsExtension.InvokeAsync<bool>("load");
        }
        public async Task<bool> Unload()
        {
            return await JsExtension.InvokeAsync<bool>("unload");
        }

        public async Task OnToolbarCreated(ToolBar toolBar)
        {
            await JsExtension.InvokeVoidAsync("onToolbarCreated", toolBar.JsControl);
        }

        #endregion
    }
}