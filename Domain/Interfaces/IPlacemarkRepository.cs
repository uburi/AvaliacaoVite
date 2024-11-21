using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPlacemarkRepository
    {
        Task<List<Placemark>> GetPlacemarkAsync(Placemark placemarkRequestFilter);
    }
}

