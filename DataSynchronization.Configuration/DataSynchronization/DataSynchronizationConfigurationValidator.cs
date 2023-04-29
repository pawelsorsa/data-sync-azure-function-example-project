using FluentValidation;

namespace DataSynchronization.Configuration.DataSynchronization
{
    public class DataSynchronizationConfigurationValidation : AbstractValidator<DataSynchronizationConfiguration>
    {
        public DataSynchronizationConfigurationValidation()
        {
            RuleFor(x => x.ContainerName).NotEmpty();
            RuleFor(x => x.TableName).NotEmpty();
        }
    }
}
