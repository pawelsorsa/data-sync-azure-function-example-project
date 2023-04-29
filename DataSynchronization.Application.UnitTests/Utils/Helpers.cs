using AutoMapper;
using DataSynchronization.Infrastructure.Mapping;

namespace DataSynchronization.Application.UnitTests.Utils
{
    public static class Helpers
    {
        public static IMapper CreateMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mapperConfig.CreateMapper();
            return mapper;
        }
    }
}