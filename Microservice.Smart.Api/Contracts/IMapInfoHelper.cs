using Microservice.Smart.Api.Models;

namespace Microservice.Smart.Api.Contracts
{
	public interface IMapInfoHelper
	{
		Task<MapInfoResponse> GetDistance(string originCity, string destinationCity);
	}
}
