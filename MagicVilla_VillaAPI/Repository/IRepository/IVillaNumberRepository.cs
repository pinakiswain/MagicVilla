using MagicVilla_VillaAPI.Models;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
    public interface IVillaNumberRepository: IBaseRepository<VillaNumber>
    {
        Task Updateasync(VillaNumber Entity);
    }
}
