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

        public async Task Start()
        {
            await JsViewer.InvokeVoidAsync("start");
        }

        public async Task LoadParameter<T>(string parameterName)
        {
            var module = await ModuleTask.Value;
            var reference =
                await module.InvokeAsync<IJSObjectReference>("GetProperty", JsViewer, parameterName.ToLower());
            var obj = typeof(T).GetMethod("Create", BindingFlags.Static | BindingFlags.Public)
                ?.Invoke(null, new object?[] {reference});
            GetType().GetProperty(parameterName)?.SetValue(this, obj);
        }

        public async Task LoadDocumentNode(Document document, BubbleNode manifestNode)
        {
            await JsViewer.InvokeAsync<string>("loadDocumentNode", document.JsDocument, manifestNode.JsBubbleNode);
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
            var responseType = viewerEvent.TypeAttr();
            
            if (responseType is null)
            {
                await _eventAsyncDictionary[eventName].Invoke(null);
                return;
            }

            var type = obj.ToObject(responseType);
            await _eventAsyncDictionary[eventName].Invoke(type);

        }

        #endregion
    }
}