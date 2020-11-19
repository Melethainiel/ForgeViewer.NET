using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace ForgeViewer.NET
{
    public static class InstallServices
    {
        public static IServiceCollection AddForgeViewer(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }
    }
}