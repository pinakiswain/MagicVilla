using MagicVilla_VillaAPI.Models;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
    public interface IRoomRepository:IBaseRepository<Room>
    {
        Task<Room> GetsingleRoom(Room Entity);
    }
}
