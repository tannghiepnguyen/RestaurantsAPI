using MediatR;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;

public class GetDishByIdForRestaurantQuery(Guid restaurantId, Guid dishId) : IRequest<DishDTO>
{
    public Guid RestaurantId { get; set; } = restaurantId;
    public Guid DishId { get; set; } = dishId;
}