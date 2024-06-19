using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDishes;
using Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

namespace Restaurants.API.Controllers
{
	[Route("api/restaurant/{restaurantId}/dishes")]
	[ApiController]
	public class DishesController(IMediator mediator) : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> CreateDish([FromRoute] Guid restaurantId, [FromBody] CreateDishCommand command)
		{
			command.RestaurantId = restaurantId;
			var dishId = await mediator.Send(command);
			return CreatedAtAction(nameof(GetByIdForRestaurant), new { restaurantId, dishId }, null);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<DishDTO>>> GetAllForRestaurant([FromRoute] Guid restaurantId)
		{
			var dishes = await mediator.Send(new GetDishesForRestaurantQuery(restaurantId));
			return Ok(dishes);
		}

		[HttpGet("{dishId}")]
		public async Task<ActionResult<DishDTO>> GetByIdForRestaurant([FromRoute] Guid restaurantId, [FromRoute] Guid dishId)
		{
			var dish = await mediator.Send(new GetDishByIdForRestaurantQuery(restaurantId, dishId));
			return Ok(dish);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteDishesForRestaurant([FromRoute] Guid restaurantId)
		{
			await mediator.Send(new DeleteDishesForRestaurantCommand(restaurantId));
			return NoContent();
		}
	}
}
