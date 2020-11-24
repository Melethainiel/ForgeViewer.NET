using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace ForgeViewer.NET.Viewing
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class Model
    {
        #region Ctor

        public static Model Create(IJSObjectReference jsObjectReference)
        {
            return new(jsObjectReference);
        }

        protected Model(IJSObjectReference jsObjectReference)
        {
            JsModel = jsObjectReference;
        }

        public IJSObjectReference JsModel { get; }        

        #endregion

        #region Methods

        public async Task<bool> Is2d()
        {
            return await JsModel.InvokeAsync<bool>("is2d");
        }
        public async Task<bool> Is3d()
        {
            return await JsModel.InvokeAsync<bool>("is3d");
        }
        public async Task<bool> IsOtg()
        {
            return await JsModel.InvokeAsync<bool>("isOTG");
        }
        public async Task<bool> IsSvf2()
        {
            return await JsModel.InvokeAsync<bool>("isSVF2");
        }
        public async Task<bool> IsPdf(bool onlyPdfSource = true)
        {
            return await JsModel.InvokeAsync<bool>("isPdf",onlyPdfSource);
        }
        public async Task<bool> IsLeaflet()
        {
            return await JsModel.InvokeAsync<bool>("isLeaflet");
        }
        public async Task<bool> IsAec()
        {
            return await JsModel.InvokeAsync<bool>("isAEC");
        }
        public async Task<bool> IsPageCoordinates()
        {
            return await JsModel.InvokeAsync<bool>("isPageCoordinates");
        }
        public async Task<bool> IsSceneBuilder()
        {
            return await JsModel.InvokeAsync<bool>("isSceneBuilder");
        }
        public async Task GetData()
        {
            await JsModel.InvokeVoidAsync("getData");
        }
        public async Task<BubbleNode> GetDocumentNode()
        {
            var reference =  await JsModel.InvokeAsync<IJSObjectReference>("getDocumentNode");
            return BubbleNode.Create(reference);
        }
        public async Task<IJSObjectReference> GetRoot() //TODO Get kind of item
        {
            return await JsModel.InvokeAsync<IJSObjectReference>("getRoot");
        }
        public async Task<double> GetRootId()
        {
            return await JsModel.InvokeAsync<double>("getRootId");
        }

        #endregion
    }
}