using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace ForgeViewer.NET.Viewing
{
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

        public async Task<bool> IsPdf(bool onlyPdfSource = true)
        {
            return await JsModel.InvokeAsync<bool>("isPdf",onlyPdfSource);
        }

        #endregion
    }
}