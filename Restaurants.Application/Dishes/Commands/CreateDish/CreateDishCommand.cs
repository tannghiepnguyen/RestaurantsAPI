using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommand : IRequest<Guid>
{
	public string Name { get; set; } = default!;
	public string Description { get; set; } = default!;
	public decimal Price { get; set; }
	public int? KiloCalories { get; set; }
	[SwaggerIgnore]
	public Guid RestaurantId { get; set; }
}