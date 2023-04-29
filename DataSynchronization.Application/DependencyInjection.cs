using DataSynchronization.Common.Serialization;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using System.Reflection;

namespace DataSynchronization.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSingleton<ISerializationService, SerializationService>();
            services.AddSingleton<IRestClient, RestClient>();
            return services;
        }
    }
}
