using DataSynchronization.Configuration.Attributes;
using DataSynchronization.Configuration.DataSynchronization;
using DataSynchronization.Configuration.Storage;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DataSynchronization.Configuration.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddStorageConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddConfiguration<StorageConfiguration, StorageConfigurationValidation>(configuration);
            services.AddConfiguration<DataSynchronizationConfiguration, DataSynchronizationConfigurationValidation>(configuration);
            return services;
        }

        private static IServiceCollection AddConfiguration<Model, Validator>(this IServiceCollection services, IConfiguration configuration)
               where Model : class, new()
               where Validator : AbstractValidator<Model>, new()
        {
            var storageConfig = configuration.GetSectionConfiguration<Model, Validator>();
            services.AddSingleton(storageConfig);

            return services;
        }

        private static Model GetSectionConfiguration<Model, Validator>(this IConfiguration configuration)
            where Model : new()
            where Validator : AbstractValidator<Model>, new()
        {
            var sectionName = GetSectionNameFromAttribute(typeof(Model));

            if (!configuration.GetSection(sectionName).Exists()) throw new Exception($"Section {sectionName} not found");

            var sectionConfig = new Model();

            configuration.Bind(sectionName, sectionConfig);

            var validationResult = new Validator().Validate(sectionConfig);

            if (!validationResult.IsValid)
            {
                throw new Exception(
                    string.Join(
                        ",",
                        $"Config section: {sectionName}",
                        $"Errors: {string.Join(",", validationResult.Errors)}"));
            }

            return sectionConfig;
        }

        private static string GetSectionNameFromAttribute(Type configType)
        {
            var configSectionAttribute = configType.GetCustomAttribute<ConfigSectionAttribute>();

            if (configSectionAttribute is null) throw new Exception("ConfigSection attribute is missing");

            if (string.IsNullOrWhiteSpace(configSectionAttribute.Name))
                throw new Exception($"ConfigSection attribute {nameof(configSectionAttribute.Name)} property required");

            return configSectionAttribute.Name;
        }
    }
}
