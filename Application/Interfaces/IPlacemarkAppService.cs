using Application.DataTransferObjects.Placemark.Request;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPlacemarkAppService
    {
        Task<List<Placemark>> GetPlacemarksAsync(PlacemarkRequestFilter placemarkRequestFilter);
        Task<object> GetFiltersPlacemarksAsync();

    }
}
