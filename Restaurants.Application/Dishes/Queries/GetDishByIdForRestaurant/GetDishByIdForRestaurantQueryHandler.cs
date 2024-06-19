﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;

public class GetDishByIdForRestaurantQueryHandler(ILogger<GetDishByIdForRestaurantQueryHandler> logger, IRestaurantRepository restaurantRepository, IMapper mapper) : IRequestHandler<GetDishByIdForRestaurantQuery, DishDTO>
{
    public async Task<DishDTO> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving dish: {DishId} for restaurant with id: {RestaurantId}", request.DishId, request.RestaurantId);
        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
        if (restaurant is null)
        {
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        }

        var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId);

        if (dish is null)
        {
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        }

        var result = mapper.Map<DishDTO>(dish);
        return result;
    }
}