using AutoMapper;
using Hinet.Model.Entities;
using Hinet.Service.ConfigRequestService.Dto;

namespace Hinet.Web.Areas.ConfigRequestArea.mapper
{
    public class ConfigMapper : Profile
    {
        public ConfigMapper()
        {
            CreateMap<ConfigRequest, ConfigRequestDto>();
            CreateMap<ConfigRequestDto, ConfigRequest>();
        }
    }
}