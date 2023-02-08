using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
    public interface IBaseRepository<ModelName> where ModelName : class
    {
        Task<List<ModelName>> GetAllasync(Expression<Func<ModelName, bool>>? Filter = null);
        Task<ModelName> Getasync(Expression<Func<ModelName, bool>>? Filter = null, bool Tracked = true);
        Task<ModelName> Getfirstordefaultasync(Expression<Func<ModelName, bool>>? Filter = null, bool Tracked = true);
        Task Createasync(ModelName Entity);
       
        Task Removeasync(ModelName Entity);

        Task Saveasync();
    }
}
