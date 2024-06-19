﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Entities;
using System.Security.Claims;

namespace Restaurants.Infrastructure.Authorization;

public class RestaurantsUserClaimsPrincipalFactory(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : UserClaimsPrincipalFactory<User, IdentityRole>(userManager, roleManager, options)
{
	public override async Task<ClaimsPrincipal> CreateAsync(User user)
	{
		var id = await GenerateClaimsAsync(user);
		if (user.Nationality is not null)
		{
			id.AddClaim(new Claim(AppClaimTypes.Nationality, user.Nationality));
		}
		if (user.DateOfBirth is not null)
		{
			id.AddClaim(new Claim(AppClaimTypes.DateOfBirth, user.DateOfBirth.ToString()));
		}
		return new ClaimsPrincipal(id);
	}
}

