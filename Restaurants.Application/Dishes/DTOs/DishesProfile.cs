using AutoMapper;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes;

public class DishesProfile : Profile
{
    public DishesProfile()
    {
        CreateMap<Dish, DishDTO>().ReverseMap();
        CreateMap<CreateDishCommand, Dish>().ReverseMap();
    }
}