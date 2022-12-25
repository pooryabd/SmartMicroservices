using Grpc.Core;
using Microservice.Smart.Api.Contracts;
using Microservice.Smart.Services.GoogleMapInfo.Contracts;

namespace Microservice.Smart.Api.Services
{
	public class DistanceInfoService : DistanceInfo.DistanceInfoBase
	{
		private readonly IGoogleDistanceService _googleDistanceService;

		public DistanceInfoService(IGoogleDistanceService googleDistanceService)
		{
			_googleDistanceService = googleDistanceService;
		}

		public override async Task<DistanceData> GetDistance(Cities cities, ServerCallContext context)
		{
			var totalMiles = "0";

			var distanceData = await _googleDistanceService.GetMapDistance(cities.OriginCity, cities.DestinationCity);

			foreach (var distanceDataRow in distanceData.Rows)
			{
				foreach (var element in distanceDataRow.Elements)
				{
					totalMiles = element.Distance.Text;
				}
			}

			return new DistanceData { Miles = totalMiles };
		}
	}
}
