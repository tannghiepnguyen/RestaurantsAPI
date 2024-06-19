using MediatR;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQuery : IRequest<RestaurantDTO>
{
	public Guid Id { get; init; }
}