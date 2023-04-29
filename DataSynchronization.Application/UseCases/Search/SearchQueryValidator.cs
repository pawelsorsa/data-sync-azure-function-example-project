using DataSynchronization.Application.UseCases.Search;
using FluentValidation;

namespace DataSynchronization.Application.UseCases.Get
{
    public sealed class SearchQueryValidator : AbstractValidator<SearchQuery>
    {
        public SearchQueryValidator()
        {
            RuleFor(x => x.FromUtc).NotEmpty();
            RuleFor(x => x.ToUtc).NotEmpty();
        }
    }
}
