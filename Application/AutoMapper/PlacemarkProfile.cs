using Application.DataTransferObjects.Placemark.Request;
using Application.DataTransferObjects.Placemark.Response;
using AutoMapper;
using Domain.Entities;

namespace Application.AutoMapper
{
    public class PlacemarkProfile : Profile
    {
        public PlacemarkProfile()
        {
            CreateMap<Placemark, PlacemarkResponse>().ReverseMap();
            CreateMap<Placemark, PlacemarkRequestFilter>().ReverseMap();

        }
    }
}
