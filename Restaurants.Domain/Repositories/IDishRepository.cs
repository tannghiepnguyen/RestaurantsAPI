using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IDishRepository
{
    Task<Guid> Create(Dish entity);
    Task Delete(IEnumerable<Dish> entities);
}