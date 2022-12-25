using System.Diagnostics.CodeAnalysis;
using Microservice.Smart.Api.Contracts;
using Microservice.Smart.Api.Helpers;
using Microservice.Smart.Api.Services;
using Microservice.Smart.Services.Common.Constants;
using Microservice.Smart.Services.Common.Contracts;
using Microservice.Smart.Services.Common.Models;
using Microservice.Smart.Services.Common.Wrappers;
using Microservice.Smart.Services.GoogleMapInfo.Contracts;
using Microservice.Smart.Services.GoogleMapInfo.Services;
using Microsoft.OpenApi.Models;

namespace Microservice.Smart.Api
{
	[ExcludeFromCodeCoverage]
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			var configuration = new ConfigurationBuilder().AddJsonFile(CommonConstants.AppSettingsJson, optional: true, reloadOnChange: true).Build();

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc(CommonConstants.SwaggerVersion, new OpenApiInfo
				{
					Title = CommonConstants.SwaggerDocTitle,
					Version = CommonConstants.SwaggerVersion
				});
			});

			IGoogleApiConfiguration googleApiConfiguration = new GoogleApiConfiguration()
			{
				GoogleApiBaseUrl = configuration[GoogleApiConstants.GoogleApiBaseUrl],
				GoogleApiKey = configuration[GoogleApiConstants.GoogleApiKey],
				GoogleApiUrl = configuration[GoogleApiConstants.GoogleApiUrl],
				GoogleApiUrlTemplate = configuration[GoogleApiConstants.GoogleApiUrlTemplate]
			};

			builder.Services.AddGrpc();
			builder.Services.AddHttpClient(DependencyNameConstants.GoogleApiClientName,
				httpClient => httpClient.BaseAddress = new Uri(googleApiConfiguration.GoogleApiBaseUrl));

			builder.Services.AddSingleton<IHttpClientWrapper, HttpClientWrapper>();
			builder.Services.AddSingleton<IHttpClientFactoryWrapper, HttpClientFactoryWrapper>();
			builder.Services.AddSingleton<IGoogleDistanceService, GoogleDistanceService>();
			builder.Services.AddSingleton<IMapInfoHelper, MapInfoHelper>();
			builder.Services.AddSingleton(googleApiConfiguration);

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint(CommonConstants.SwaggerEndpoint,
						CommonConstants.SwaggerDescription);
				});
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGrpcService<DistanceInfoService>();
				endpoints.MapControllers();
			});

			app.Run();

		}
	}
}