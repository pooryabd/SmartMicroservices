using Microservice.Smart.Services.GoogleMapInfo.Contracts;
using Microservice.Smart.Services.GoogleMapInfo.Models.Responses;
using System.Text.Json;
using Microservice.Smart.Services.Common.Contracts;
using Microservice.Smart.Services.Common.Constants;

namespace Microservice.Smart.Services.GoogleMapInfo.Services
{
	public class GoogleDistanceService : IGoogleDistanceService
	{
		private readonly IHttpClientFactoryWrapper _httpClientFactoryWrapper;
		private readonly IGoogleApiConfiguration _googleApiConfiguration;

		public GoogleDistanceService(IGoogleApiConfiguration googleApiConfiguration, IHttpClientFactoryWrapper httpClientFactoryWrapper)
		{
			_googleApiConfiguration = googleApiConfiguration;
			_httpClientFactoryWrapper = httpClientFactoryWrapper;
		}
		public async Task<GoogleDistanceData> GetMapDistance(string originCity, string destinationCity)
		{
			var apiKey = _googleApiConfiguration.GoogleApiKey;
			var googleDistanceApiUrl = _googleApiConfiguration.GoogleApiUrl;

			googleDistanceApiUrl += string.Format(GoogleApiConstants.GoogleApiUrlTemplate, originCity, destinationCity, apiKey);

			var httpClientWrapper = _httpClientFactoryWrapper.CreateClient(DependencyNameConstants.GoogleApiClientName);

			var responseStr = await httpClientWrapper.GetStringAsync(googleDistanceApiUrl);

			var distanceInfo = JsonSerializer.Deserialize<GoogleDistanceData>(responseStr);
			return distanceInfo;
		}
	}
}
