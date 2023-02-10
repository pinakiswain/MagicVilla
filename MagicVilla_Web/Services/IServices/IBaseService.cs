using MagicVilla_Web.Models;

namespace MagicVilla_Web.Services.IServices
{
    public interface IBaseService
    {
        APIRespose responseModel { get;set;}
        Task<T> SendAsync<T>(APIRequest aPIRequest);
    }
}
