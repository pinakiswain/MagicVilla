using MagicVilla_VillaAPI.Models;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
    public interface IRepositoryWrapper
    {
        IBaseRepository<Villa> Villa { get; }
        IBaseRepository<VillaNumber> VillaNumber { get; }
       
    }
}
