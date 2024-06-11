using System.Text.Encodings.Web;
using AutoMapper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.DTOs;

public class RestaurantsProfile : Profile
{
    public RestaurantsProfile()
    {
        CreateMap<Restaurant, RestaurantDTO>()
            .ForMember(d => d.City, option => option.MapFrom(src => src.Address == null ? null : src.Address.City))
            .ForMember(d => d.PostalCode, option => option.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
            .ForMember(d => d.Street, option => option.MapFrom(src => src.Address == null ? null : src.Address.Street))
            .ForMember(d => d.Dishes, option => option.MapFrom(src => src.Dishes))
            .ReverseMap();

        CreateMap<CreateRestaurantCommand, Restaurant>()
            .ForMember(d => d.Address, opt => opt.MapFrom(src => new Address()
            {
                City = src.City,
                PostalCode = src.PostalCode,
                Street = src.Street
            })).ReverseMap();

        CreateMap<UpdateRestaurantCommand, Restaurant>();
    }
}