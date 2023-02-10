using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("Api/Villnumber")]
    [ApiController]
    
    public class VillaNumberAPIController: ControllerBase
    {
        private readonly IVillaNumberRepository _dbVillanumber;
        //private readonly IVillaRepository _dbVilla;
        private readonly IRepositoryWrapper _wapper;

        protected APIRespose _response;
        private readonly IMapper _Mapper;
        public VillaNumberAPIController(IVillaNumberRepository dbVillanumber, IMapper Mapper, IRepositoryWrapper wrapper)
        {
            _dbVillanumber= dbVillanumber;
            this._response = new APIRespose();
            _Mapper=Mapper;
            _wapper = wrapper;
        }
        [HttpGet("GetAllVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIRespose>> GetAllVillaNumber()
        {
            try
            {
                IEnumerable<VillaNumber> villaNumbers = await _wapper.VillaNumber.GetAllasync();
                if(villaNumbers.Count()==0)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "No data found" };
                       
                    return NotFound(_response);
                }
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result=_Mapper.Map<List<VillaNumberDto>>(villaNumbers);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }
        [HttpGet("GetVillaNumberByID")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIRespose>> GetAllVillaNumberByID(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Enter Id" };
                    return BadRequest(_response);
                }
                var VillaNumber = await _wapper.VillaNumber.Getfirstordefaultasync(u => u.VillaNo == id);
                if (VillaNumber == null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "No data found for the given ID" };
                    return NotFound(_response);
                }
                _response.IsSuccess = true;
                _response.StatusCode= HttpStatusCode.OK;
                _response.Result=_Mapper.Map<VillaNumberDto>(VillaNumber);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }
        [HttpGet("GetAllVillaNumberByVillID")]
        public async Task<ActionResult<APIRespose>> GetAllVillaNumberByVillID(int VillaId)
        {
            try
            {
                if(VillaId == 0)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Enter Id" };
                    return BadRequest(_response);
                }
                IEnumerable<VillaNumber> villaNumber=await _wapper.VillaNumber.GetAllasync(u=>u.VillaId== VillaId);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }
        [HttpPost("InsertVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
       
        public async Task<ActionResult<APIRespose>> InsertVillaNumber([FromBody]  VillaNumberCreateDto villaNumberCreateDto)
        {
            try
            {
                if(villaNumberCreateDto.VillaNo==0)
                {
                    _response.IsSuccess=false;
                    _response.ErrorMessages = new List<string> { "Enter valid villaNo" };
                    return BadRequest(_response);
                }
                if (villaNumberCreateDto.VillaNo.ToString().Length < 3)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Villa Number Must be greater than 2 char" };
                    return BadRequest(_response);
                }
               
                var CheckVillaNumber = await _wapper.VillaNumber.Getfirstordefaultasync(u => u.VillaNo == villaNumberCreateDto.VillaNo);
                var CheckVillaIdExist = await _wapper.Villa.Getfirstordefaultasync(u => u.Id == villaNumberCreateDto.VillaId);

                var m = await _wapper.VillaNumber.Getasync(u => u.VillaNo == villaNumberCreateDto.VillaNo);
                if (CheckVillaNumber != null)
                {
                    _response.IsSuccess = false;
                    ModelState.AddModelError("", "VillaNumber shuld be unique");
                    return BadRequest(ModelState);
                }
                if (CheckVillaIdExist == null)
                {
                    _response.IsSuccess = false;
                    ModelState.AddModelError("", "Vill Id not found");
                    return BadRequest(ModelState);
                }
                if (await _wapper.Villa.Getasync(u=>u.Id==villaNumberCreateDto.VillaId)==null)
                {

                }
                if (!ModelState.IsValid)
                {

                }
                VillaNumber villaNumber = _Mapper.Map<VillaNumber>(villaNumberCreateDto);
                await _wapper.VillaNumber.Createasync(villaNumber);
                _response.IsSuccess = true;
                _response.StatusCode= HttpStatusCode.OK;
                _response.Result= _Mapper.Map<VillaNumberDto>(villaNumberCreateDto);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }
        [HttpPut("UpdateVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIRespose>> UpdateVillaNumber([FromBody] VillaNumberUpdateDto villaNumberUpdateDto, int id)
        {
            try
            {
                if (villaNumberUpdateDto == null || id != villaNumberUpdateDto.VillaNo)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string> { "Invalid Villa Number" };
                    return BadRequest(_response);
                }
                VillaNumber villaNumber=_Mapper.Map<VillaNumber>(villaNumberUpdateDto);
                await _wapper.VillaNumber.Updateasync(villaNumber);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result=_Mapper.Map<VillaNumberUpdateDto>(villaNumber);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;

        }
        [HttpDelete("DeleteVillaNumber")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIRespose>> DeleteVillaNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "VillaNumber can not be 0" };
                    return BadRequest(_response);

                }
                VillaNumber villaNumber=await _wapper.VillaNumber.Getfirstordefaultasync(u=>u.VillaNo==id);
                if(villaNumber==null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "No result found" };
                    return StatusCode(StatusCodes.Status404NotFound);
                }
              await _wapper.VillaNumber.Removeasync(villaNumber);
                return NoContent();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }
    }
}
