using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class DishesRepository(RestaurantsDbContext db) : IDishRepository
{
    public async Task<Guid> Create(Dish entity)
    {
        await db.Dishes.AddAsync(entity);
        await db.SaveChangesAsync();
        return entity.Id;
    }

    public async Task Delete(IEnumerable<Dish> entities)
    {
        db.Dishes.RemoveRange(entities);
        await db.SaveChangesAsync();
    }
}