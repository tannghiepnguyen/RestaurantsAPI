﻿using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Users;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionsExtensions
{
	public static void AddApplication(this IServiceCollection services)
	{
		var applicationAssembly = typeof(ServiceCollectionsExtensions).Assembly;
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
		services.AddAutoMapper(applicationAssembly);
		services.AddValidatorsFromAssembly(applicationAssembly).AddFluentValidationAutoValidation();
		services.AddScoped<IUserContext, UserContext>();
		services.AddHttpContextAccessor();
	}
}