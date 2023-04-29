using AutoMapper;
using DataSynchronization.Application.Repositories;
using DataSynchronization.Application.UnitTests.Utils;
using DataSynchronization.Application.UseCases.Search;
using DataSynchronization.Domain;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DataSynchronization.Application.UnitTests.UseCases.Search
{
    public class SearchQueryHandlerTests
    {
        private static IMapper _mapper => Helpers.CreateMapper();

        [Fact]
        public async Task Should_Return_Empty_Result_When_There_Are_No_Items_For_Given_Date()
        {
            var now = DateTime.UtcNow;
            var fakeRowKey = Guid.NewGuid().ToString();

            var query = new SearchQuery()
            {
                FromUtc = DateTime.UtcNow.AddDays(1),
                ToUtc = DateTime.UtcNow.AddDays(1)
            };

            var repositoryMock = new Mock<IDataSynchronizationRepository>();
            repositoryMock.Setup(r => r.GetItemsAsync(query.FromUtc, query.ToUtc))
                .ReturnsAsync(Enumerable.Empty<DataSynchronizationResult>());

            var result = await new SearchQueryHandler(_mapper, repositoryMock.Object).Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Count().Should().Be(0);
            repositoryMock.Verify(v => v.GetItemsAsync(query.FromUtc, query.ToUtc), Times.Once);
        }

        [Fact]
        public async Task Should_Return_Items_For_Given_Date()
        {
            var now = DateTime.UtcNow;
            var fakeRowKey = Guid.NewGuid().ToString();

            var fakeResult = new List<DataSynchronizationResult>()
            {
                new DataSynchronizationResult() { RowKey = "1", Timestamp = now.AddMinutes(1) },
                new DataSynchronizationResult() { RowKey = "2", Timestamp = now.AddMinutes(2) },
            };

            var query = new SearchQuery()
            {
                FromUtc = now.AddDays(1),
                ToUtc = now.AddDays(1)
            };

            var repositoryMock = new Mock<IDataSynchronizationRepository>();
            repositoryMock.Setup(r => r.GetItemsAsync(query.FromUtc, query.ToUtc)).ReturnsAsync(fakeResult);

            var result = await new SearchQueryHandler(_mapper, repositoryMock.Object).Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Count().Should().Be(2);
            repositoryMock.Verify(v => v.GetItemsAsync(query.FromUtc, query.ToUtc), Times.Once);
        }
    }
}