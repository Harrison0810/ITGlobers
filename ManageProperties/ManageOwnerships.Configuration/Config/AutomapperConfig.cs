using AutoMapper;
using ManageOwnerships.Domain.Models;
using ManageOwnerships.Infrastructure.Contracts;
using ManageOwnerships.Infrastructure.Entities;

namespace ManageOwnerships.Configuration.Config
{
    internal class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<OwnerEntity, OwnerContract>().ReverseMap();
            CreateMap<OwnershipEntity, OwnershipContract>().ReverseMap();
            CreateMap<OwnershipImageEntity, OwnershipImageContract>().ReverseMap();
            CreateMap<OwnershipTraceEntity, OwnershipTraceContract>().ReverseMap();

            CreateMap<OwnerContract, OwnerModel>().ForMember(x => x.Password, opt => opt.Ignore()).ReverseMap();
            CreateMap<OwnershipContract, OwnershipModel>().ReverseMap();
            CreateMap<OwnershipImageContract, OwnershipImageModel>().ReverseMap();
        }
    }
}
