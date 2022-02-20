using AutoMapper;
using Contact.Application.DocumentLog.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Application.Mappings
{
    public class DocumentLogMappings : Profile
    {
        public DocumentLogMappings()
        {
            CreateMap<Domain.Entities.DocumentLog, GetDocumentLogByIdQueryResponse>()
                .ForMember(
                desc => desc.DocumentLogId,
                opt => opt.MapFrom(src => src.Id))
                .ForMember(
                desc => desc.CompleteDate,
                opt => opt.MapFrom(src => src.ModifiedDate))
                .ForMember(
                desc => desc.RequestDate,
                opt => opt.MapFrom(src => src.CreationDate));

            CreateMap<Domain.Entities.DocumentLog, GetDocumentLogsQueryResponse>()
              .ForMember(
              desc => desc.DocumentLogId,
              opt => opt.MapFrom(src => src.Id))
              .ForMember(
              desc => desc.CompleteDate,
              opt => opt.MapFrom(src => src.ModifiedDate))
              .ForMember(
              desc => desc.RequestDate,
              opt => opt.MapFrom(src => src.CreationDate));
        }
    }
}
