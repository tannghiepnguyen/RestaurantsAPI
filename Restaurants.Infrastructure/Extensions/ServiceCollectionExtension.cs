﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
	public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("RestaurantsDb");
		services.AddDbContext<RestaurantsDbContext>(options => options.UseSqlServer(connectionString));
		services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
		services.AddScoped<IRestaurantRepository, RestaurantsRepository>();
		services.AddScoped<IDishRepository, DishesRepository>();
		services
			.AddIdentityApiEndpoints<User>()
			.AddRoles<IdentityRole>()
		.AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
			.AddEntityFrameworkStores<RestaurantsDbContext>();

		services.AddAuthorizationBuilder()
			.AddPolicy("HasNationality", builder => builder.RequireClaim(AppClaimTypes.Nationality, "German", "Polish"));
	}
}