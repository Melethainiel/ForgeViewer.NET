using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace ForgeViewer.NET.Ui
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class GroupControl : Control
    {
        
        #region Ctor

        public new static GroupControl Create(IJSObjectReference jsObjectReference)
        {
            return new(jsObjectReference);
        }

        protected GroupControl(IJSObjectReference jsObjectReference) : base(jsObjectReference) { }

        #endregion

        #region Methods
        
        public async Task<bool> AddControl(Control control, object options)
        {
            return await JsControl.InvokeAsync<bool>("addControl", control.JsControl, options);
        }
        public async Task<T> GetControl<T>(string controlId) where T : Control
        {
            var reference = await JsControl.InvokeAsync<IJSObjectReference>("getControl", controlId);
            return (T)(typeof(T).GetMethod("Create", BindingFlags.Static | BindingFlags.Public)?.Invoke(null, new object?[] {reference}) ?? throw new Exception("can't parse"));
        }
        public async Task<string> GetControlId(int number)
        {
            return await JsControl.InvokeAsync<string>("getControlId", number);
        }
        public async Task<int> GetNumberOfControls()
        {
            return await JsControl.InvokeAsync<int>("getNumberOfControls");
        }
        public async Task<int> IndexOf(string control)
        {
            return await JsControl.InvokeAsync<int>("indexOf", control);
        }
        public async Task<int> IndexOf(Control control)
        {
            return await JsControl.InvokeAsync<int>("indexOf", control.JsControl);
        }
        public async Task<bool> RemoveControl(string control)
        {
            return await JsControl.InvokeAsync<bool>("removeControl", control);
        }
        public async Task<bool> RemoveControl(Control control)
        {
            return await JsControl.InvokeAsync<bool>("removeControl", control.JsControl);
        }
        public async Task<bool> SetCollapsed(bool collapsed)
        {
            return await JsControl.InvokeAsync<bool>("setCollapsed", collapsed);
        }

        #endregion
    }
}