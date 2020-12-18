using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using ForgeViewer.NET.Misc;
using ForgeViewer.NET.Models;
using ForgeViewer.NET.Ui;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace ForgeViewer.NET.Viewing
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedMethodReturnValue.Global")]
    public class Viewer3d
    {
        #region Properties

        protected Lazy<Task<IJSObjectReference>> ModuleTask { get; }
        private IJSObjectReference? _jsViewer;

        protected IJSObjectReference JsViewer
        {
            get => _jsViewer ?? throw new ArgumentNullException();
            set => _jsViewer = value;
        }

        public ToolBar ToolBar
        {
            get => _toolBar ?? throw new Exception($"{nameof(ToolBar)} hasn't been loaded");
            protected set => _toolBar = value;
        }

        private readonly Dictionary<string, Func<object?, Task>> _eventAsyncDictionary;
        private ToolBar? _toolBar;

        #endregion

        #region Ctor

        public static Viewer3d Create(IServiceProvider serviceProvider)
        {
            return new(serviceProvider.GetRequiredService<IJSRuntime>());
        }

        protected Viewer3d(IJSRuntime jsRuntime)
        {
            _eventAsyncDictionary = new Dictionary<string, Func<object?, Task>>();
            ModuleTask = new Lazy<Task<IJSObjectReference>>(() =>
                jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/ForgeViewer.NET/ForgeViewer.js")
                    .AsTask());
        }

        #endregion

        #region Methodes

        public async Task Initializer(string id)
        {
            var module = await ModuleTask.Value;
            JsViewer = await module.InvokeAsync<IJSObjectReference>("Viewer3dInitializer", id);
        }
        public async Task Uninitialize()
        {
            await JsViewer.InvokeVoidAsync("uninitialize");
        }
        public async Task Start()
        {
            var module = await ModuleTask.Value;
            var eventName = ViewerEvent.ToolbarCreatedEvent.DescriptionAttr();
            await module.InvokeAsync<string>("AddViewEventListener", JsViewer, eventName,
                DotNetObjectReference.Create(this));

            await JsViewer.InvokeVoidAsync("start");
        }
        public async Task<Model> LoadDocumentNode(Document document, BubbleNode manifestNode, object? options = null)
        {
            var reference = await JsViewer.InvokeAsync<IJSObjectReference>("loadDocumentNode", document.JsDocument,
                manifestNode.JsBubbleNode, options);
            return Model.Create(reference);
        }
        public async Task<Extension> GetExtensions(string extensionId, Func<Extension, Task> callback)
        {
            var reference = await JsViewer.InvokeAsync<IJSObjectReference>("getExtension", extensionId);
            var extension = Extension.Create(reference);
            await callback.Invoke(extension);
            return extension;
        }
        public async Task LoadExtension(string extensionId) //TODO Need to check promise
        {
            await JsViewer.InvokeVoidAsync("loadExtension", extensionId);
        }
        public async Task UnloadExtension(string extensionId)
        {
            await JsViewer.InvokeVoidAsync("unloadExtension", extensionId);
        }

        public async Task GetProperties(double id, Func<PropertyResult, Task> onSuccess )
        {
            var module = await ModuleTask.Value;
            var result = await module.InvokeAsync<PropertyResult>("CallFunction", JsViewer, "getProperties", id);
            await onSuccess(result);
        }
        
        public async Task AddEventListener(ViewerEvent viewerEvent, Func<object?, Task> action)
        {
            var eventName = viewerEvent.DescriptionAttr();
            var module = await ModuleTask.Value;
            _eventAsyncDictionary.Add(eventName, action);
            await module.InvokeAsync<string>("AddViewEventListener", JsViewer, eventName,
                DotNetObjectReference.Create(this));
        }
        [JSInvokable]
        public async Task EventListener(string eventName, JsonElement? obj)
        {
            var viewerEvent = eventName.GetValueFromDescription<ViewerEvent>();
            if (viewerEvent == ViewerEvent.ToolbarCreatedEvent)
                await InitToolbarAsync();
            var responseType = viewerEvent.TypeAttr();
            var type = obj.ToObject(responseType);
            await _eventAsyncDictionary[eventName].Invoke(type);
        }

        #endregion

        #region private methods

        private async Task InitToolbarAsync()
        {
            await LoadParameter<ToolBar>();
        }
        private async Task LoadParameter<T>()
        {
            var module = await ModuleTask.Value;
            var parameterName = typeof(T).Name;
            var reference =
                await module.InvokeAsync<IJSObjectReference>("GetProperty", JsViewer, parameterName.ToLower());
            var obj = typeof(T).GetMethod("Create", BindingFlags.Static | BindingFlags.Public)
                ?.Invoke(null, new object?[] {reference});
            GetType().GetProperty(parameterName)?.SetValue(this, obj);
        }

        #endregion
    }
}