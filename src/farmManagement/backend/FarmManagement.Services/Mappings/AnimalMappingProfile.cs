using AutoMapper;
using FarmManagement.Dal.Entities;
using FarmManagement.Models.Dto;

namespace FarmManagement.Services.Mappings;

public sealed class AnimalMappingProfile : Profile
{
    public AnimalMappingProfile()
    {
        AnimalProfile();
    }

    private void AnimalProfile()
    {
        CreateMap<AnimalEntity, AnimalDto>();
        CreateMap<AnimalDto, AnimalEntity>()
            .ForMember(m => m.Id, opt => opt.Ignore());
    }
}