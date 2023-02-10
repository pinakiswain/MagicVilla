using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("Api/Room")]
    [ApiController]
    public class RoomAPIController: ControllerBase
    {
        private readonly IRepositoryWrapper _wrapper;
        protected APIRespose _response;
        public RoomAPIController(IRepositoryWrapper wrapper)
        {
            _wrapper=wrapper;
            this._response=new APIRespose();
        }
        [HttpGet("Getallroom")]
        public async Task<ActionResult<APIRespose>> Get()
        {
            IEnumerable<Room> rooms = await _wrapper.Room.GetAllasync();
            //Room room=await _wrapper.Room.GetsingleRoom(room);
            _response.Result= rooms;
            return Ok(_response);
                
        }
    }
}
