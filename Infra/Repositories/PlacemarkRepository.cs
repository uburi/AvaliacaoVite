using Domain.Entities;
using Domain.Interfaces;

namespace Infra.Repositories
{
    public class PlacemarkRepository : IPlacemarkRepository
    {
        private readonly IReadKmlRepository readKmlRepository;
        private readonly string kmlFilePath;

        public PlacemarkRepository(IReadKmlRepository readKmlRepository)
        {
            this.readKmlRepository = readKmlRepository;
            this.kmlFilePath = Path.Combine(Directory.GetCurrentDirectory(), "DIRECIONADORES1.kml");
        }

        public async Task<List<Placemark>> GetPlacemarkAsync(Placemark placemarkRequestFilter)
        {
            List<Placemark> placemarks = await readKmlRepository.GetPlacemarkDataAsync(kmlFilePath);

            if(placemarkRequestFilter != null)
            {
                if (!string.IsNullOrEmpty(placemarkRequestFilter.Cliente))
                {
                    placemarks = placemarks.Where(p => p.Cliente.Contains(placemarkRequestFilter.Cliente, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                if (!string.IsNullOrEmpty(placemarkRequestFilter.Situacao))
                {
                    placemarks = placemarks.Where(p => p.Situacao.Contains(placemarkRequestFilter.Situacao, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                if (!string.IsNullOrEmpty(placemarkRequestFilter.Bairro))
                {
                    placemarks = placemarks.Where(p => p.Bairro.Contains(placemarkRequestFilter.Bairro, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                if (!string.IsNullOrEmpty(placemarkRequestFilter.Referencia) && placemarkRequestFilter.Referencia.Length >= 3)
                {
                    placemarks = placemarks.Where(p => p.Referencia.Contains(placemarkRequestFilter.Referencia, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                if (!string.IsNullOrEmpty(placemarkRequestFilter.RuaCruzamento) && placemarkRequestFilter.RuaCruzamento.Length >= 3)
                {
                    placemarks = placemarks.Where(p => p.RuaCruzamento.Contains(placemarkRequestFilter.RuaCruzamento, StringComparison.OrdinalIgnoreCase)).ToList();
                }
            }

            return placemarks;
        }
    }
}
