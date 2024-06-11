using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<Restaurant?> GetByIdAsync(Guid id);
    Task<Guid> Create(Restaurant entity);
    Task Delete(Restaurant entity);
    Task SaveChange();
}