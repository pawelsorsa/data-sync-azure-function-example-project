using AutoMapper;
using DataSynchronization.Application.UseCases.Get;
using DataSynchronization.Application.UseCases.Search;
using DataSynchronization.Domain;
using RestSharp;

namespace DataSynchronization.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RestResponse, BaseEntity>()
                .ForMember(dest => dest.RowKey, opt => opt.MapFrom(x => new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds()))
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(x => DateTime.UtcNow))
                .IncludeAllDerived();

            CreateMap<RestResponse, DataSynchronizationResult>()
                .ForMember(dest => dest.PartitionKey, opt => opt.MapFrom(x => nameof(DataSynchronizationResult)))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.StatusCode))
                .ForMember(dest => dest.StatusMessage, opt => opt.MapFrom(src => src.ErrorMessage));

            CreateMap<DataSynchronizationResult, SearchQueryResponse>()
                .ForMember(dest => dest.RowKey, opt => opt.MapFrom(src => src.RowKey))
                .IncludeAllDerived();

            CreateMap<DataSynchronizationResult, GetSingleResponse>();            
        }
    }
}
