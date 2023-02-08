using MagicVilla_VillaAPI.Models;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
    public interface IVillaRepository:IBaseRepository<Villa>
    {
        Task Updateasync(Villa Entity);
    }
}

