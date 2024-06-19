using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger, IRestaurantRepository restaurantRepository, IMapper mapper) : IRequestHandler<GetAllRestaurantsQuery, PageResult<RestaurantDTO>>
{
	public async Task<PageResult<RestaurantDTO>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Getting all restaurants");
		var (restaurants, totalCount) = await restaurantRepository.GetAllMatchingAsync(request.SearchPhrase, request.PageSize, request.PageNumber);
		var restaurantsDto = mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);

		var result = new PageResult<RestaurantDTO>(restaurantsDto, totalCount, request.PageSize, request.PageNumber);
		return result;
	}
}