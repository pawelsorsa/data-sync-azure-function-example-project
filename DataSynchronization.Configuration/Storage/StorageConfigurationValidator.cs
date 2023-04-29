using FluentValidation;

namespace DataSynchronization.Configuration.Storage
{
    public class StorageConfigurationValidation : AbstractValidator<StorageConfiguration>
    {
        public StorageConfigurationValidation()
        {
            RuleFor(x => x.ConnectionString).NotEmpty();
        }
    }
}
