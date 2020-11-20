using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
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

        private readonly Dictionary<string, Func<object[]?, Task>> _funcs;
        private ToolBar? _toolBar;

        #endregion

        #region Ctor

        public static Viewer3d Create(IServiceProvider serviceProvider)
        {
            return new(serviceProvider.GetRequiredService<IJSRuntime>());
        }

        protected Viewer3d(IJSRuntime jsRuntime)
        {
            _funcs = new Dictionary<string, Func<object[]?, Task>>();
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

        public async Task AddEventListener(string eventName, Func<object[]?, Task> action)
        {
            var module = await ModuleTask.Value;
            _funcs.Add(eventName, action);
            await module.InvokeAsync<string>("AddViewEventListener", JsViewer, eventName,
                DotNetObjectReference.Create(this));
        }

        [JSInvokable]
        public void EventListener(string eventName, params object[]? obj)
        {
            if (obj is null)
            {
                _funcs[eventName].Invoke(null);
                return;
            }

            var args = new List<object>();

            foreach (var o in obj)
            {
                var el = o is JsonElement element ? element : default;

                switch (el.ValueKind)
                {
                    case JsonValueKind.Undefined:
                        args.Add(el.GetString() ?? string.Empty);
                        break;
                    case JsonValueKind.Object:
                        break;
                    case JsonValueKind.Array:
                        break;
                    case JsonValueKind.String:
                        args.Add(el.GetString() ?? string.Empty);
                        break;
                    case JsonValueKind.Number:
                        args.Add(el.GetDouble());
                        break;
                    case JsonValueKind.True:
                        args.Add(el.GetBoolean());
                        break;
                    case JsonValueKind.False:
                        args.Add(el.GetBoolean());
                        break;
                    case JsonValueKind.Null:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            _funcs[eventName].Invoke(args.ToArray());
        }

        #endregion
    }
}