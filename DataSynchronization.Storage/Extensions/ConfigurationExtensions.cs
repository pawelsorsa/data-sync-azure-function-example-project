using DataSynchronization.Domain;
using DataSynchronization.Storage.Clients;
using DataSynchronization.Storage.Services.BlobService;
using DataSynchronization.Storage.Services.TableService;
using Microsoft.Extensions.DependencyInjection;

namespace DataSynchronization.Storage.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddStorageClients(this IServiceCollection services)
        {
            services.AddSingleton<IBlobContainerClient, AzureBlobContainerClient>();
            services.AddSingleton<ITableContainerClient, AzureTableContainerClient>();
            services.AddSingleton<IBlobStorageService, BlobStorageService>();
            services.AddSingleton(typeof(ITableStorageService<>), typeof(TableStorageService<>));
            return services;
        }
    }
}
