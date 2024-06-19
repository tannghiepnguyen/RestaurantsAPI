using MediatR;
using Restaurants.Application.Common;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQuery : IRequest<PageResult<RestaurantDTO>>
{
	public string? SearchPhrase { get; set; }
	public int PageNumber { get; set; }
	public int PageSize { get; set; }

}