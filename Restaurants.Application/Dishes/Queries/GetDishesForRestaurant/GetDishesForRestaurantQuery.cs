using MediatR;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

public class GetDishesForRestaurantQuery(Guid restaurantId) : IRequest<IEnumerable<DishDTO>>
{
    public Guid RestaurantId { get; set; } = restaurantId;
}