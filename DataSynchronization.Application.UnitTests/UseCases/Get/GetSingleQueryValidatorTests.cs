using DataSynchronization.Application.UseCases.Get;
using FluentAssertions;
using System;
using Xunit;

namespace DataSynchronization.Application.UnitTests.UseCases.Get
{
    public class GetSingleQueryValidatorTests
    {
        private GetSingleQueryValidator _validator = new();

        [Fact]
        public void Should_Pass_Validation_For_Correct_Request()
        {
            var query = new GetSingleQuery()
            {
                RowKey = Guid.NewGuid().ToString()
            };

            var result = _validator.Validate(query);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void Should_Fail_Validation_When_RowKey_Is_Empty(string rowKey)
        {
            var query = new GetSingleQuery()
            {
                RowKey = rowKey
            };

            var result = _validator.Validate(query);

            result.IsValid.Should().BeFalse();
        }
    }
}