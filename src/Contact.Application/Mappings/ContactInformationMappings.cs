using AutoMapper;
using Contact.Application.Contacts.Queries;
using Contact.Domain.Entities;

namespace Contact.Application.Mappings
{
    public class ContactInformationMappings : Profile
    {
        public ContactInformationMappings()
        {
            CreateMap<ContactInformation, ContactInformationResponse>()
                     .ForMember(
                dest => dest.ContactInformationId,
                opt => opt.MapFrom(src => src.Id)
                );
        }
    }
}
