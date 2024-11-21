namespace SB.Challenge.Application;
using AutoMapper;
using SB.Challenge.Domain;

public class ProfileMapping : Profile
{
    public ProfileMapping()
    {
        CreateMap<Person, PersonViewModel>()
               .ForMember(t => t.Id, o => o.MapFrom(t => t.Id))
               .ForMember(t => t.Name, o => o.MapFrom(t => t.Name))
               .ForMember(t => t.LastName, o => o.MapFrom(t => t.LastName))
               .ForMember(t => t.LastName, o => o.MapFrom(t => t.LastName))
               .ForMember(t => t.IsActive, o => o.MapFrom(t => t.IsActive))
               .ReverseMap();

        CreateMap<GovernmentEntity, GovernmentEntityViewModel>()
              .ForMember(t => t.Id, o => o.MapFrom(t => t.Id))
              .ForMember(t => t.Name, o => o.MapFrom(t => t.Name))
              .ForMember(t => t.IsActive, o => o.MapFrom(t => t.IsActive))
              .ReverseMap();
    }
}
