using AutoMapper;
using DataSynchronization.Application.Repositories;
using DataSynchronization.Application.UnitTests.Utils;
using DataSynchronization.Application.UseCases.Get;
using DataSynchronization.Domain;
using FluentAssertions;
using Moq;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DataSynchronization.Application.UnitTests.UseCases.Get
{
    public class GetSingleQueryHandlerTests
    {
        private static IMapper _mapper => Helpers.CreateMapper();

        [Fact]
        public async Task Should_Return_Null_When_Item_Does_Not_Exist()
        {
            var fakeRowKey = Guid.NewGuid().ToString();

            var repositoryMock = new Mock<IDataSynchronizationRepository>();
            repositoryMock.Setup(r => r.GetAsync(fakeRowKey)).ReturnsAsync((DataSynchronizationResult)null);

            var contentRepoMock = new Mock<IDataSynchronizationContentRepository>();
            contentRepoMock.Setup(r => r.GetAsync(fakeRowKey)).ReturnsAsync(new byte[0]);

            var handler = new GetSingleQueryHandler(_mapper, repositoryMock.Object, contentRepoMock.Object);

            var result = await handler.Handle(new GetSingleQuery() { RowKey = fakeRowKey }, CancellationToken.None);

            result.Should().BeNull();
            repositoryMock.Verify(v => v.GetAsync(fakeRowKey), Times.Once);
            contentRepoMock.Verify(v => v.GetAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task Should_Return_Item_When_Item_Exists()
        {
            var fakeRowKey = Guid.NewGuid().ToString();
            var item = new DataSynchronizationResult() { RowKey = fakeRowKey, PartitionKey = "12345", Status = 200, Timestamp = DateTime.UtcNow };

            var repositoryMock = new Mock<IDataSynchronizationRepository>();
            repositoryMock.Setup(r => r.GetAsync(fakeRowKey)).ReturnsAsync(item);

            var contentResult = new byte[1];
            var contentRepoMock = new Mock<IDataSynchronizationContentRepository>();
            contentRepoMock.Setup(r => r.GetAsync(fakeRowKey)).ReturnsAsync(contentResult);

            var handler = new GetSingleQueryHandler(_mapper, repositoryMock.Object, contentRepoMock.Object);

            var result = await handler.Handle(new GetSingleQuery() { RowKey = fakeRowKey }, CancellationToken.None);

            result.Should().NotBeNull();
            result.RowKey.Should().Be(item.RowKey);
            result.Timestamp.Should().Be(item.Timestamp);
            result.Status.Should().Be(item.Status);
            result.Content.Should().Be(Encoding.UTF8.GetString(contentResult));
            repositoryMock.Verify(v => v.GetAsync(fakeRowKey), Times.Once);
            contentRepoMock.Verify(v => v.GetAsync(fakeRowKey), Times.Once);
        }
    }
}