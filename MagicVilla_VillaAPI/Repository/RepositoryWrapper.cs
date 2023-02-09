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
        public RepositoryWrapper(MagicVillaDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public IBaseRepository<Villa> Villa
        {
            get
            {
                return _Villa ??
                    (_Villa = new BaseRepository<Villa>(_dbcontext));
            }
        }

        public IBaseRepository<VillaNumber> VillaNumber
        {
            get
            {
                return _VillaNumber ??
                    (_VillaNumber = new BaseRepository<VillaNumber>(_dbcontext));
            }
        }
    }
}
