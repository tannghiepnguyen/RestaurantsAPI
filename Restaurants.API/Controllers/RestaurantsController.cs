using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Common;
using Restaurants.Application.DTOs;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RestaurantsController(IMediator mediator) : ControllerBase
	{
		[HttpGet]
		public async Task<ActionResult<PageResult<RestaurantDTO>>> GetAll([FromQuery] GetAllRestaurantsQuery query)
		{
			var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
			return Ok(restaurants);
		}

		[HttpGet("{id:Guid}")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[Authorize(Policy = PolicyNames.HasNationality)]
		public async Task<ActionResult<RestaurantDTO>> GetById([FromRoute] Guid id)
		{
			var userId = User.Claims.FirstOrDefault(c => c.Type == "<id claim type>")?.Value;
			var restaurant = await mediator.Send(new GetRestaurantByIdQuery()
			{
				Id = id
			});
			return Ok(restaurant);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand command)
		{
			Guid id = await mediator.Send(command);
			return CreatedAtAction(nameof(GetById), new { id }, null);
		}

		[HttpDelete("{id:Guid}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> DeleteRestaurant([FromRoute] Guid id)
		{
			await mediator.Send(new DeleteRestaurantCommand()
			{
				Id = id
			});
			return NoContent();
		}

		[HttpPatch("{id:Guid}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> UpdateRestaurant([FromForm] UpdateRestaurantCommand command)
		{
			await mediator.Send(command);
			return NoContent();
		}
	}
}
