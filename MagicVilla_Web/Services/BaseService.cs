using MagicVilla_Librery;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using Newtonsoft.Json;
using System.Text;

namespace MagicVilla_Web.Services
{
    public class BaseService : IBaseService
    {

        public APIRespose responseModel { get; set; }
        public IHttpClientFactory _httpClient { get; set; }
        public BaseService(IHttpClientFactory httpClient)
        {
            this.responseModel = new APIRespose();
            this._httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(APIRequest aPIRequest)
        {
           try
            {
                var Clinet = _httpClient.CreateClient("MagicAPI"); // Create client
                HttpRequestMessage Message = new HttpRequestMessage();//Meesage to pass in Http request
                Message.Headers.Add("Accept", "application/json"); // Accept Header type
                Message.Headers.TryAddWithoutValidation("Content-Type", "application/json");// Accept content type
                Message.RequestUri=new Uri(aPIRequest.Url);// API Url
                if(aPIRequest.Data!=null)
                {
                    Message.Content = new StringContent(JsonConvert.SerializeObject(aPIRequest.Data),Encoding.UTF8, "application/json");// If we send data then we need to Serialize the data
                }
             switch(aPIRequest.ApiType)// What is the type of the Http request 
                {
                    case SD.ApiType.POST:
                            Message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.GET:
                        Message.Method = HttpMethod.Get;
                        break;
                    case SD.ApiType.PUT:
                        Message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        Message.Method = HttpMethod.Delete;
                        break;
                }
                HttpResponseMessage responseMessage  = null;

                responseMessage =await Clinet.SendAsync(Message);// Get the response message from the API

                var ApiContent= await responseMessage.Content.ReadAsStringAsync();// read the content
               var Apiresponse = JsonConvert.DeserializeObject<T>(ApiContent);// Deserialize the content to fit on the method f Apiresponse class
                return Apiresponse;

            }
            catch (Exception ex)
            {
                var Dto = new APIRespose()
                {
                    IsSuccess = false,
                    ErrorMessages = new List<string> { ex.ToString() }
                };
                var res=JsonConvert.SerializeObject(Dto);
                var Apiresponse=JsonConvert.DeserializeObject<T>(res);
                return Apiresponse;

            }
        }
    }
}
