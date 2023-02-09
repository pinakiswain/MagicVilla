using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;

namespace MagicVilla_VillaAPI
{
    public class MappingCofig:Profile
    {
        public MappingCofig()
        {
            CreateMap<Villa, VillaDto>().ReverseMap();//Map
            CreateMap<Villa,VillaDtoCreate>().ReverseMap();//Other way of reverse map
            CreateMap<Villa,VillaDtoUpdate>().ReverseMap();

            CreateMap<VillaNumber,VillaNumberDto>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberCreateDto>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberUpdateDto>().ReverseMap();

            CreateMap<VillaNumberDto, VillaNumberCreateDto>().ReverseMap();
        }
    }
}
