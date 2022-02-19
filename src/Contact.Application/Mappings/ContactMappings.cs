using AutoMapper;
using Contact.Application.Contacts.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Application.Mappings
{
    public class ContactMappings : Profile
    {
        public ContactMappings()
        {
            CreateMap<Domain.Entities.Contact, GetContactByIdResponse>()
            .ForMember(
                dest => dest.ContactId,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dest => dest.ContactInformations,
                opt => opt.MapFrom(src => src.ContactInformations));

            CreateMap<Domain.Entities.Contact, GetAllContactResponse>()
                .ForMember(
                dest => dest.ContactId,
                opt => opt.MapFrom(src => src.Id));
        }
    }
}
