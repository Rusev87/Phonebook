using Phonebook.Application.Persons.Dtos;
using Phonebook.Domain.Entities;

namespace Phonebook.Application.Common.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Person, PersonDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.Addresses));

        CreateMap<Address, AddressDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.AddressDetail, opt => opt.MapFrom(src => src.AddressDetail))
            .ForMember(dest => dest.PhoneNumbers, opt => opt.MapFrom(src => src.PhoneNumbers.Select(p => p.Number)));
    }
}
