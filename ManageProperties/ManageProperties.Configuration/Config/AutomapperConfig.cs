using AutoMapper;
using ManageProperties.Domain.Models;
using ManageProperties.Infrastructure.Contracts;
using ManageProperties.Infrastructure.Entities;

namespace ManageProperties.Configuration.Config
{
    internal class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<OwnerEntity, OwnerContract>().ReverseMap();
            CreateMap<PropertyEntity, PropertyContract>().ReverseMap();
            CreateMap<PropertyImageEntity, PropertyImageContract>().ReverseMap();
            CreateMap<PropertyTraceEntity, PropertyTraceContract>().ReverseMap();

            CreateMap<OwnerContract, OwnerModel>().ForMember(x => x.Password, opt => opt.Ignore()).ReverseMap();
        }
    }
}
