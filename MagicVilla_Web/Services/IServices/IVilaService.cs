using MagicVilla_Web.Models.Dto;

namespace MagicVilla_Web.Services.IServices
{
    public interface IVilaService
    {
        Task<T> GetallAsnyc<T>();
        Task<T> GetAsnyc<T>(int id);
        Task<T> CreateAsnyc<T>(VillaDtoCreate villaDtoCreate);
        Task<T> UpdateAsnyc<T>(VillaDtoUpdate villaDtoUpdate);
        Task<T> DeleteAsnyc<T>(int id);

    }
}
