using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository
{
    public class BaseRepository<ModelName> : IBaseRepository<ModelName> where ModelName : class
    {
        private readonly MagicVillaDbContext _Db;
        internal DbSet<ModelName> Dbset;
        public BaseRepository(MagicVillaDbContext Db)
        {
            _Db = Db;
            this.Dbset=_Db.Set<ModelName>();
        }
        public async Task Createasync(ModelName Entity)
        {
            await Dbset.AddAsync(Entity);
            await Saveasync();
        }
        public async Task<ModelName> Getasync(Expression<Func<ModelName, bool>>? Filter = null, bool Tracked = true)
        {
            IQueryable<ModelName> query = Dbset;
            if (!Tracked)
            {
                query = query.AsNoTracking();
            }
            if (Filter != null)
            {
                query.Where(Filter);
            }
            return await query.FirstOrDefaultAsync();

        }
        public async Task<ModelName> Getfirstordefaultasync(Expression<Func<ModelName, bool>>? Filter = null, bool Tracked = true)
        {
            IQueryable<ModelName> query = Dbset;

            return await query.FirstOrDefaultAsync(Filter);

        }

        public async Task<List<ModelName>> GetAllasync(Expression<Func<ModelName, bool>>? Filter = null)
        {
            IQueryable<ModelName> query = Dbset;
            if (Filter != null)
            {
                query.Where(Filter);
            }
            return await query.ToListAsync();
        }

        public async Task Removeasync(ModelName Entity)
        {
            Dbset.Remove(Entity);
            await Saveasync();
        }

        public async Task Saveasync()
        {
            await _Db.SaveChangesAsync();
        }
    }
}
