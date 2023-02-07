using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/Villa")]
    [ApiController]
    public class VillaAPIController: ControllerBase
    {
        //private readonly ILogger<VillaAPIController> _logger;
        private readonly ILogger<VillaAPIController> _logger;
        private readonly MagicVillaDbContext _dbContext;
        
        public VillaAPIController(ILogger<VillaAPIController> logger,MagicVillaDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        
        [HttpGet("Route1",Name ="GetallVilla")]//route name =Route1
        public async Task<ActionResult <IEnumerable<VillaDto>>> GetVillas()
        {
            _logger.LogInformation("Get all villas");
            return Ok(await _dbContext.Villas.ToListAsync());//200

        }
        [HttpGet("Route2", Name = "GetallVilla2")]// route name =Route2
        public ActionResult<IEnumerable<VillaDto>> GetVillas2()
        {
            _logger.LogInformation("Get all villas");
            return Ok(VillaStore.villas);//200

        }
        [HttpGet("Getvillabyid",Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult <VillaDto>> villaDtosById(int id)
        {
            if(id==0)
            {
                return BadRequest();//400
            }
            var Villa =await _dbContext.Villas.FirstOrDefaultAsync(u => u.Id == id);
            if(Villa==null)
            {
                return NotFound();//404
            }
            return Ok(Villa);//200
        }
       
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDto>> CreateVilla([FromBody] VillaDtoCreate villaDto)
        {
            //if(ModelState.IsValid==false)
            //{
            //    return BadRequest(ModelState);
            //}
            if(await _dbContext.Villas.FirstOrDefaultAsync(u=>u.Name==villaDto.Name)!=null)
            {
                ModelState.AddModelError("UniqueName", "Vill Name should be unique");
                return BadRequest(ModelState);
            }
            if (villaDto == null)
            {
                //return StatusCode(StatusCodes.Status400BadRequest);// use the custom statuscode you want
                 return BadRequest();//Directly use the BadRequest()
            }
            //if(villaDto.Id>0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);// use the custom statuscode you want
            //}
            //villaDto.Id = VillaStore.villas.Max(u=>u.Id)+1;
            Villa villa = new Villa()
            {
                Name = villaDto.Name,
              
                Amenity = villaDto.Amenity,
                Details = villaDto.Details,
                ImageUrl = villaDto.ImageUrl,
                Occupancy = villaDto.Occupancy,
                Rate = villaDto.Rate,
                SQft = villaDto.SQft
            };
           await _dbContext.Villas.AddAsync(villa);
            await _dbContext.SaveChangesAsync();
            //return Ok(villaDto);
            return CreatedAtRoute("GetVilla", new {id=villa.Id}, villa);
        }
        [HttpDelete("id",Name ="DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task <IActionResult> DeleteVila(int id)
        {
            if(id==0)
            {
                return BadRequest();
            }
            var Villa=await _dbContext.Villas.FirstOrDefaultAsync(u=>u.Id==id);
            if(Villa==null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            _dbContext.Villas.Remove(Villa);
           await _dbContext.SaveChangesAsync(true);
            //return NoContent();
            return CreatedAtRoute("GetallVilla", VillaStore.villas);
        }
        [HttpPut("id",Name ="UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateVilla(int id,[FromBody] VillaDtoUpdate villaDto)
        {
            if(villaDto==null || id!=villaDto.Id)
            {
                return BadRequest();
            }
            Villa villa = new Villa()//Convert villDto to Villa
            {
                Name = villaDto.Name,
                Id = villaDto.Id,
                Amenity = villaDto.Amenity,
                Details = villaDto.Details,
                ImageUrl = villaDto.ImageUrl,
                Occupancy = villaDto.Occupancy,
                Rate = villaDto.Rate,
                SQft = villaDto.SQft
            };
            _dbContext.Villas.Update(villa);
           await _dbContext.SaveChangesAsync();
            // return NoContent();
            return CreatedAtRoute("GetallVilla", VillaStore.villas);
        }
        [HttpPatch("id",Name ="PtachUpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PatchVillUpdate(int id,JsonPatchDocument<VillaDtoUpdate> PatchVilla)
        {
            if (PatchVilla == null || id == 0)
            {
                return BadRequest();
            }
            var villa =await _dbContext.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            VillaDtoUpdate villaDtos = new VillaDtoUpdate()//Convert villa to villaDtos
            {
                Name = villa.Name,
                Id = villa.Id,
                Amenity = villa.Amenity,
                Details = villa.Details,
                ImageUrl = villa.ImageUrl,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                SQft = villa.SQft
            };
            if (villa == null)
            {
                return NotFound();
            }
            PatchVilla.ApplyTo(villaDtos, ModelState);

            Villa villas = new Villa()//Convert villDto to Villa
            {
                Name = villaDtos.Name,
                Id = villaDtos.Id,
                Amenity = villaDtos.Amenity,
                Details = villaDtos.Details,
                ImageUrl = villaDtos.ImageUrl,
                Occupancy = villaDtos.Occupancy,
                Rate = villaDtos.Rate,
                SQft = villaDtos.SQft
            };
            _dbContext.Villas.Update(villas);
           await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
