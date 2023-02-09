using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Repository.IRepository;

namespace MagicVilla_VillaAPI.Repository
{
    public class VillaNumberRepository : BaseRepository<VillaNumber>, IVillaNumberRepository
    {
        private readonly MagicVillaDbContext _db;
        public VillaNumberRepository(MagicVillaDbContext db):base(db)
        {
            _db=db;
        }
        public async Task Updateasync(VillaNumber Entity)
        {
            Entity.UpdatedDate = DateTime.Now;
            _db.Update(Entity);
            await _db.SaveChangesAsync();
        }
    }
}
