using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository
{
    public class VillaRepository : BaseRepository<Villa>,IVillaRepository
    {
        private readonly MagicVillaDbContext _Db;
        public VillaRepository(MagicVillaDbContext Db):base(Db)
        {
            _Db = Db;
        }
        
        public async Task Updateasync(Villa villa)
        {
             _Db.Villas.Update(villa);
           await  _Db.SaveChangesAsync();
        }

      
    }
}
