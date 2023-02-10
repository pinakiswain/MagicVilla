using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Repository.IRepository;

namespace MagicVilla_VillaAPI.Repository
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        private readonly MagicVillaDbContext _Db;
        public RoomRepository(MagicVillaDbContext db):base(db)
        {
            _Db= db;
        }
        public Task<Room> GetsingleRoom(Room Entity)
        {
            throw new NotImplementedException();
        }
    }
}
