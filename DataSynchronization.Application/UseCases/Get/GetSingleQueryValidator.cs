using FluentValidation;

namespace DataSynchronization.Application.UseCases.Get
{
    public sealed class GetSingleQueryValidator : AbstractValidator<GetSingleQuery>
    {
        public GetSingleQueryValidator()
        {
            RuleFor(x => x.RowKey)
                .NotEmpty();
        }
    }
}
