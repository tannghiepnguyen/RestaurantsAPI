using Restaurants.API.Middlewares;

namespace Restaurants.API.Extensions;

public static class WebApplicationBuilderExtension
{
	public static void AddPresentation(this WebApplicationBuilder builder)
	{
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(c =>
		{
			c.AddSecurityDefinition("bearerAuth", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
			{
				Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
				Scheme = "Bearer"
			});
			c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
	{
		{
			new Microsoft.OpenApi.Models.OpenApiSecurityScheme
			{
				Reference = new Microsoft.OpenApi.Models.OpenApiReference
				{
					Type =  Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
					Id = "bearerAuth"
				}
			},
			Array.Empty<string>()
		}
	});
		});
		builder.Services.AddScoped<ErrorHandlingMiddleware>();
		builder.Services.AddScoped<RequestTimeLoggingMiddleware>();
		builder.Services.AddAuthentication();
	}
}
