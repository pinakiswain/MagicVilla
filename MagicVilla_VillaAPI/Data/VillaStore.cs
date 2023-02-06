using MagicVilla_VillaAPI.Models.Dto;

namespace MagicVilla_VillaAPI.Data
{
    public static class VillaStore
    {
        public static List<VillaDto> villas= new List<VillaDto>()
            {
                new VillaDto{ Id=1,Name="Villa1",SQft=100,Occupancy=1},
                new VillaDto{ Id=2,Name="Villa2",SQft=200,Occupancy=2},
            };
    }
}
