using MagicVilla_VillaAPI.Models;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
    public interface IRepositoryWrapper
    {
       
       
        IRoomRepository Room { get; }
        IVillaRepository Villa { get; }

        IVillaNumberRepository VillaNumber { get; }

    }
}
