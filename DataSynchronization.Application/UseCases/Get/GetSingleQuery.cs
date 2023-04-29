using MediatR;

namespace DataSynchronization.Application.UseCases.Get
{
    public sealed class GetSingleQuery : IRequest<GetSingleResponse>
    {
        public string RowKey { get; set; } = default!;
    }
}
