using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Logging;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("apiv/Villa")]
    [ApiController]
    public class VillaAPIController: ControllerBase
    {
        //private readonly ILogger<VillaAPIController> _logger;
        private readonly ILogging _logger;
        public VillaAPIController(ILogging logger)
        {
            _logger = logger;
        }
        [HttpGet(Name ="GetallVilla")]
        public ActionResult <IEnumerable<VillaDto>> GetVillas()
        {
            _logger.Log("Get all villas", "Error");
            return Ok(VillaStore.villas);//200

        }
        [HttpGet("id",Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult <VillaDto> villaDtosById(int id)
        {
            if(id==0)
            {
                return BadRequest();//400
            }
            var Villa = VillaStore.villas.FirstOrDefault(u => u.Id == id);
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
        public ActionResult <VillaDto> CreateVilla([FromBody]VillaDto villaDto)
        {
            //if(ModelState.IsValid==false)
            //{
            //    return BadRequest(ModelState);
            //}
            if(VillaStore.villas.FirstOrDefault(u=>u.Name==villaDto.Name)!=null)
            {
                ModelState.AddModelError("UniqueName", "Vill Name should be unique");
                return BadRequest(ModelState);
            }
            if (villaDto == null)
            {
                //return StatusCode(StatusCodes.Status400BadRequest);// use the custom statuscode you want
                 return BadRequest();//Directly use the BadRequest()
            }
            if(villaDto.Id>0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);// use the custom statuscode you want
            }
            villaDto.Id = VillaStore.villas.Max(u=>u.Id)+1;
            VillaStore.villas.Add(villaDto);
            //return Ok(villaDto);
            return CreatedAtRoute("GetVilla", new {id=villaDto.Id}, villaDto);
        }
        [HttpDelete("id",Name ="DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVila(int id)
        {
            if(id==0)
            {
                return BadRequest();
            }
            var Villa=VillaStore.villas.FirstOrDefault(u=>u.Id==id);
            if(Villa==null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            VillaStore.villas.Remove(Villa);
            //return NoContent();
            return CreatedAtRoute("GetallVilla", VillaStore.villas);
        }
        [HttpPut("id",Name ="UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateVilla(int id,[FromBody] VillaDto villaDto)
        {
            if(villaDto==null || id!=villaDto.Id)
            {
                return BadRequest();
            }
            var villa = VillaStore.villas.FirstOrDefault(u => u.Id == villaDto.Id);
            if(villa==null)
            {
                return NotFound();
            }
           villa.Name= villaDto.Name;
            villa.Occupancy= villaDto.Occupancy;
            villa.SQft= villaDto.SQft;
            // return NoContent();
            return CreatedAtRoute("GetallVilla", VillaStore.villas);
        }
        [HttpPatch("id",Name ="PtachUpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PatchVillUpdate(int id,JsonPatchDocument<VillaDto> PatchVilla)
        {
            if (PatchVilla == null || id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villas.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            PatchVilla.ApplyTo(villa,ModelState);
            return NoContent();
        }
    }
}
