using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/Villa")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        //private readonly ILogger<VillaAPIController> _logger;
        protected APIRespose _response;
        private readonly ILogger<VillaAPIController> _logger;
        private readonly IVillaRepository _dbvilla;
        private readonly IMapper _Mapper;

        public VillaAPIController(ILogger<VillaAPIController> logger, IVillaRepository dbvilla, IMapper Mapper)
        {
            _logger = logger;
            _dbvilla = dbvilla;
            _Mapper = Mapper;
            this._response = new();
        }

        [HttpGet("Route1", Name = "GetallVilla")]//route name =Route1
        public async Task<ActionResult<APIRespose>> GetVillas()
        {
            try
            {
                _logger.LogInformation("Get all villas");
                IEnumerable<Villa> Villalist = await _dbvilla.GetAllasync();
                _response.Result = _Mapper.Map<List<VillaDto>>(Villalist);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;


        }
        [HttpGet("Route2", Name = "GetallVilla2")]// route name =Route2
        public ActionResult<IEnumerable<VillaDto>> GetVillas2()
        {
            _logger.LogInformation("Get all villas");
            var m = VillaStore.villas.Where(u => u.Id == 10);
            return Ok(VillaStore.villas);//200

        }
        [HttpGet("Getvillabyid", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIRespose>> villaDtosById(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();//400
                }
                var Villa = await _dbvilla.Getfirstordefaultasync(u => u.Id == id);
                if (Villa == null)
                {
                    return NotFound();//404
                }
                _response.Result = _Mapper.Map<VillaDto>(Villa);
                _response.StatusCode = HttpStatusCode.OK; _response.IsSuccess = true;
                return Ok(_response);//200
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIRespose>> CreateVilla([FromBody] VillaDtoCreate villaCreateDto)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }
                if (await _dbvilla.Getfirstordefaultasync(u => u.Name.ToLower() == villaCreateDto.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("UniqueName", "Vill Name should be unique");
                    return BadRequest(ModelState);
                }
                if (villaCreateDto == null)
                {
                    //return StatusCode(StatusCodes.Status400BadRequest);// use the custom statuscode you want
                    return BadRequest();//Directly use the BadRequest()
                }
                //if(villaDto.Id>0)
                //{
                //    return StatusCode(StatusCodes.Status500InternalServerError);// use the custom statuscode you want
                //}
                //villaDto.Id = VillaStore.villas.Max(u=>u.Id)+1;
                Villa villa = _Mapper.Map<Villa>(villaCreateDto);// map villa to VillaCreateDto
                                                                 //Villa villa = new Villa()
                                                                 //{
                                                                 //    Name = villaCreateDto.Name,

                //    Amenity = villaCreateDto.Amenity,
                //    Details = villaCreateDto.Details,
                //    ImageUrl = villaCreateDto.ImageUrl,
                //    Occupancy = villaCreateDto.Occupancy,
                //    Rate = villaCreateDto.Rate,
                //    SQft = villaCreateDto.SQft
                //};
                await _dbvilla.Createasync(villa);

                //return Ok(villaDto);
                _response.Result = _Mapper.Map<Villa>(villaCreateDto);
                _response.StatusCode = HttpStatusCode.OK; _response.IsSuccess = true;
                return CreatedAtRoute("GetVilla", new { id = villa.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }
        [HttpDelete("id", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIRespose>> DeleteVila(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var Villa = await _dbvilla.Getfirstordefaultasync(u => u.Id == id);
                if (Villa == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                await _dbvilla.Removeasync(Villa);

                //return NoContent();
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }
        [HttpPut("id", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIRespose>> UpdateVilla(int id, [FromBody] VillaDtoUpdate villaUpdateDto)
        {
            try
            {
                if (villaUpdateDto == null || id != villaUpdateDto.Id)
                {
                    return BadRequest();
                }
                Villa villa = _Mapper.Map<Villa>(villaUpdateDto);
                //Villa villa = new Villa()//Convert villDto to Villa
                //{
                //    Name = villaUpdateDto.Name,
                //    Id = villaUpdateDto.Id,
                //    Amenity = villaUpdateDto.Amenity,
                //    Details = villaUpdateDto.Details,
                //    ImageUrl = villaUpdateDto.ImageUrl,
                //    Occupancy = villaUpdateDto.Occupancy,
                //    Rate = villaUpdateDto.Rate,
                //    SQft = villaUpdateDto.SQft
                //};
                await _dbvilla.Updateasync(villa);

                // return NoContent();
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
                //return CreatedAtRoute("GetallVilla", VillaStore.villas);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }
        [HttpPatch("id", Name = "PtachUpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PatchVillUpdate(int id, JsonPatchDocument<VillaDtoUpdate> PatchVilla)
        {
            if (PatchVilla == null || id == 0)
            {
                return BadRequest();
            }
            var villa = await _dbvilla.Getfirstordefaultasync(u => u.Id == id, false);
            VillaDtoUpdate villaDtos = _Mapper.Map<VillaDtoUpdate>(villa);
            //VillaDtoUpdate villaDtos = new VillaDtoUpdate()//Convert villa to villaDtos
            //{
            //    Name = villa.Name,
            //    Id = villa.Id,
            //    Amenity = villa.Amenity,
            //    Details = villa.Details,
            //    ImageUrl = villa.ImageUrl,
            //    Occupancy = villa.Occupancy,
            //    Rate = villa.Rate,
            //    SQft = villa.SQft
            //};
            if (villa == null)
            {
                return NotFound();
            }
            PatchVilla.ApplyTo(villaDtos, ModelState);
            Villa villas = _Mapper.Map<Villa>(villaDtos);
            //Villa villas = new Villa()//Convert villDto to Villa //no need after user mapper.
            //{
            //    Name = villaDtos.Name,
            //    Id = villaDtos.Id,
            //    Amenity = villaDtos.Amenity,
            //    Details = villaDtos.Details,
            //    ImageUrl = villaDtos.ImageUrl,
            //    Occupancy = villaDtos.Occupancy,
            //    Rate = villaDtos.Rate,
            //    SQft = villaDtos.SQft
            //};
            await _dbvilla.Updateasync(villas);


            return NoContent();
        }
    }
}
