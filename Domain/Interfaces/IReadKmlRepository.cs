using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IReadKmlRepository
    {
        Task<List<Placemark>> GetPlacemarkDataAsync(string kmlFilePath);
    }
}