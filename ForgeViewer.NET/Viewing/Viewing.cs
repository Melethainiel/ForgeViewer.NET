using System;
using System.Threading.Tasks;
using ForgeViewer.NET.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace ForgeViewer.NET.Viewing
{
    public class Viewing
    {
        #region Properties

        private Lazy<Task<IJSObjectReference>> ModuleTask { get; }
        private Func<Task<string>>? _getAccessToken;
        private Func<Task>? _callBack;

        #endregion

        #region Ctor

        public static Viewing Create(IServiceProvider serviceProvider)
        {
            return new(serviceProvider.GetRequiredService<IJSRuntime>());
        }

        private Viewing(IJSRuntime jsRuntime)
        {
            ModuleTask = new Lazy<Task<IJSObjectReference>>(() =>
                jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/ForgeViewer.NET/ForgeViewer.js")
                    .AsTask());
        }


        #endregion


        public async Task Initializer(Options? options, Func<Task> callback)
        {
            _callBack = callback;
            options ??= new Options();
            if (options.GetAccessToken is { })
                _getAccessToken = options.GetAccessToken;


            var module = await ModuleTask.Value;
            await module.InvokeVoidAsync("ViewingInitializer", options, DotNetObjectReference.Create(this));
        }

        public async Task Shutdown()
        {
            var module = await ModuleTask.Value;
            await module.InvokeVoidAsync("ViewingShutdown");
        }

        [JSInvokable]
        public async Task InitializerCallback()
        {
            await (_callBack?.Invoke() ?? throw new Exception("No function set"));
        }

        [JSInvokable]
        public async Task<object> GetAccessToken()
        {
            return await (_getAccessToken?.Invoke() ?? throw new Exception("No function set"));
        }
    }
}