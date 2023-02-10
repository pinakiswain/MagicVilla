using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;

namespace MagicVilla_Web
{
    public class MappingCofig:Profile
    {
        public MappingCofig()
        {
            CreateMap<VillaDto, VillaDtoCreate>().ReverseMap();//Map
            CreateMap<VillaDto, VillaDtoUpdate>().ReverseMap();//Map

            CreateMap<VillaNumberDto, VillaDtoCreate>().ReverseMap();//Map
            CreateMap<VillaNumberDto, VillaDtoUpdate>().ReverseMap();//Map

        }
    }
}
