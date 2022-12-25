using Microservice.Smart.Services.GoogleMapInfo.Models.Responses;

namespace Microservice.Smart.Api.Models
{
	public class MapInfoResponse
	{
		public GoogleDistanceData Data { get; set; }
		public List<string> Errors { get; set; }
	}
}
