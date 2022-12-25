using Microservice.Smart.Api.Contracts;
using Microservice.Smart.Api.Models;
using Microservice.Smart.Services.GoogleMapInfo.Contracts;

namespace Microservice.Smart.Api.Helpers
{
	public class MapInfoHelper : IMapInfoHelper
	{
		private readonly IGoogleDistanceService _googleDistanceService;

		public MapInfoHelper(IGoogleDistanceService googleDistanceService)
		{
			_googleDistanceService = googleDistanceService;
		}

		public async Task<MapInfoResponse> GetDistance(string originCity, string destinationCity)
		{
			var response = new MapInfoResponse();

			try
			{
				response.Data = await _googleDistanceService.GetMapDistance(originCity, destinationCity);
			}
			catch (Exception e)
			{
				response.Errors = new List<string>()
				{
					e.Message
				};
			}

			return response;
		}
	}
}
