using Microsoft.Extensions.DependencyInjection;

namespace ForgeViewer.NET
{
    public static class InstallServices
    {
        public static IServiceCollection AddForgeViewer(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<Viewing>();

            return serviceCollection;
        }
    }
}