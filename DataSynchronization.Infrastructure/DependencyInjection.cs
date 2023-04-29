using AutoMapper;
using DataSynchronization.Application.Repositories;
using DataSynchronization.Infrastructure.Mapping;
using DataSynchronization.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DataSynchronization.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IDataSynchronizationRepository, DataSynchronizationRepository>();
            services.AddScoped<IDataSynchronizationContentRepository, DataSynchronizationContentRepository>();
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            }).CreateMapper());

            return services;
        }
    }
}
