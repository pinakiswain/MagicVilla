using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Repository.IRepository;

namespace MagicVilla_VillaAPI.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly MagicVillaDbContext _dbcontext;
        private BaseRepository<Villa> _Villa;
        private BaseRepository<VillaNumber> _VillaNumber;
        private BaseRepository<Room> _Room;

        public RepositoryWrapper(MagicVillaDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            VillaNumber = new VillaNumberRepository(_dbcontext);
            Villa = new VillaRepository(_dbcontext);
            Room=new RoomRepository(_dbcontext);

        }
        public IVillaRepository Villa
        {
            get;
            private set;
        }
        public IRoomRepository Room
        {
            get;
            private set;
        }

        public IVillaNumberRepository VillaNumber
        {
            get;
            private set;
        }

        
    }
}
