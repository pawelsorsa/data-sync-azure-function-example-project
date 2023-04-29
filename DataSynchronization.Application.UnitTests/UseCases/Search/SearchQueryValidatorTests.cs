using DataSynchronization.Application.UseCases.Get;
using DataSynchronization.Application.UseCases.Search;
using FluentAssertions;
using System;
using Xunit;

namespace DataSynchronization.Application.UnitTests.UseCases.Search
{
    public class SearchQueryValidatorTests
    {
        private SearchQueryValidator _validator = new();

        [Fact]
        public void Should_Pass_Validation_For_Correct_Request()
        {
            var query = new SearchQuery()
            {
                FromUtc = DateTime.UtcNow,
                ToUtc = DateTime.UtcNow
            };

            var result = _validator.Validate(query);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_Fail_When_FromUtc_Empty()
        {
            var query = new SearchQuery()
            {
                FromUtc = DateTime.MinValue,
                ToUtc = DateTime.UtcNow
            };


            var result = _validator.Validate(query);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Should_Fail_When_ToUtc_Empty()
        {
            var query = new SearchQuery()
            {
                FromUtc = DateTime.UtcNow,
                ToUtc = DateTime.MinValue
            };

            var result = _validator.Validate(query);

            result.IsValid.Should().BeFalse();
        }
    }
}