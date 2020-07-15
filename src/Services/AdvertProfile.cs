using AdvertApi.Models;
using AutoMapper;

namespace  WebAdvert.Services 
{
    public class AdvertProfile : Profile
    {
        public AdvertProfile()
        {
            CreateMap<AdvertModel, AdvertDBModel>();
        }
    }
}