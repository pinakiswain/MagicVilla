using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVilaService _vilaService;
        private readonly IMapper _Mapper;
        public VillaController(IVilaService vilaService, IMapper Mapper)
        {
            _vilaService=vilaService;
            _Mapper=Mapper;

        }
        public async Task<IActionResult> IndexVilla()
        {
            List<VillaDto> List = new();
            var response=await _vilaService.GetallAsnyc<APIRespose>();
            if(response!=null && response.IsSuccess==true)
            {
                List=JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Result));
            }
            return View(List);
        }
    }
}
