using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class RestaurantsRepository : IRestaurantRepository
{
	private readonly RestaurantsDbContext _db;

	public RestaurantsRepository(RestaurantsDbContext db)
	{
		_db = db;
	}
	public async Task<IEnumerable<Restaurant>> GetAllAsync()
	{
		var restaurants = await _db.Restaurants.Include(r => r.Dishes).ToListAsync();
		return restaurants;
	}

	public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber)
	{
		var searchPhraseLower = searchPhrase is null ? "" : searchPhrase.ToLower();

		var baseQuery = _db.Restaurants.Where(r => (r.Name.ToLower().Contains(searchPhraseLower) || r.Description.ToLower().Contains(searchPhraseLower)));

		int totalCount = await baseQuery.CountAsync();

		var restaurants = await baseQuery
			.Skip(pageSize * (pageNumber - 1))
			.Take(pageSize)
			.Include(r => r.Dishes)
			.ToListAsync();
		return (restaurants, totalCount);
	}

	public async Task<Restaurant?> GetByIdAsync(Guid id)
	{
		var restaurant = await _db.Restaurants.Include(r => r.Dishes).FirstOrDefaultAsync(c => c.Id == id);
		return restaurant;
	}

	public async Task<Guid> Create(Restaurant entity)
	{
		await _db.Restaurants.AddAsync(entity);
		await _db.SaveChangesAsync();
		return entity.Id;
	}

	public async Task Delete(Restaurant entity)
	{
		_db.Restaurants.Remove(entity);
		await _db.SaveChangesAsync();
	}

	public async Task SaveChange() => await _db.SaveChangesAsync();
}