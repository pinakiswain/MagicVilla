using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class VillaService : BaseService, IVilaService
    {
        private readonly IHttpClientFactory _httpClient;
       private string ApiUrl;
        public VillaService(IHttpClientFactory httpClient,IConfiguration configuration):base(httpClient)
        {
            _httpClient=httpClient;
            ApiUrl = configuration.GetValue<string>("ServiceUrls:VIllaAPI");//get the api URl from appsettings.json
        }
        public Task<T> CreateAsnyc<T>(VillaDtoCreate villaDtoCreate)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType =MagicVilla_Librery.SD.ApiType.POST,
                Data = villaDtoCreate,
                Url=ApiUrl+ "/api/Villa"
            });
        }

        public Task<T> DeleteAsnyc<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = MagicVilla_Librery.SD.ApiType.DELETE,
                Url = ApiUrl + "/api/Villa/" + id
            });
        }

        public Task<T> GetallAsnyc<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = MagicVilla_Librery.SD.ApiType.GET,
                Url = ApiUrl + "/api/Villa/Route1"
            });
        }

        public Task<T> GetAsnyc<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = MagicVilla_Librery.SD.ApiType.GET,
                Url = ApiUrl + "api/Villa/"+id
            });
        }

        public Task<T> UpdateAsnyc<T>(VillaDtoUpdate villaDtoUpdate)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = MagicVilla_Librery.SD.ApiType.PUT,
                Data = villaDtoUpdate,
                Url = ApiUrl + "/api/Villa/" + villaDtoUpdate.Id
            });
        }
    }
}
