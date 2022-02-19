using AutoMapper;
using Contact.Application.Contacts.Queries;
using Contact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Application.Mappings
{
   public class ContactInformationMappings :Profile 
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
