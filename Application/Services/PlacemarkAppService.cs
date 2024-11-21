using Application.DataTransferObjects.Placemark.Request;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class PlacemarkAppService : IPlacemarkAppService
    {
        private readonly IPlacemarkRepository _placemarkRepository;
        private readonly IMapper _mapper;

        public PlacemarkAppService(IPlacemarkRepository placemarkRepository, IMapper mapper)
        {
            _placemarkRepository = placemarkRepository;
            _mapper = mapper;
        }

        public async Task<object> GetFiltersPlacemarksAsync()
        {
            var placemarks = await GetPlacemarksAsync(new PlacemarkRequestFilter());
            var filtros = new
            {
                Clientes = placemarks
                .Where(p => !string.IsNullOrEmpty(p.Cliente))
                .Select(p => p.Cliente)
                .Distinct()
                .ToList(),

                Situacoes = placemarks
                .Where(p => !string.IsNullOrEmpty(p.Situacao))
                .Select(p => p.Situacao)
                .Distinct()
                .ToList(),

                Bairros = placemarks
                .Where(p => !string.IsNullOrEmpty(p.Bairro)) 
                .Select(p => p.Bairro)
                .Distinct()
                .ToList()
            };
            return filtros;
        }

        public Task<List<Placemark>> GetPlacemarksAsync(PlacemarkRequestFilter placemarkRequestFilter)
        {
            return _placemarkRepository.GetPlacemarkAsync(_mapper.Map<Placemark>(placemarkRequestFilter));
        }

    }
}
