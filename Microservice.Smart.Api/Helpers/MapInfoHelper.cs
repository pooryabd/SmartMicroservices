using Microservice.Smart.Api.Contracts;
using Microservice.Smart.Api.Models;
using Microservice.Smart.Services.RequestReceiver;

namespace Microservice.Smart.Api.Helpers
{
	/// <summary>
	/// Class MapInfoHelper
	/// </summary>
	public class MapInfoHelper : IMapInfoHelper
	{
		private readonly IRequestReceiverWrapper _requestReceiverWrapper;

		/// <summary>
		/// Create a new instance of MapInfoHelper
		/// </summary>
		/// <param name="requestReceiverWrapper">The requestReceiverWrapper</param>
		public MapInfoHelper(IRequestReceiverWrapper requestReceiverWrapper)
		{
			_requestReceiverWrapper = requestReceiverWrapper;
		}

		/// <summary>
		/// Gets distance between two cities.
		/// </summary>
		/// <param name="originCity">The originCity</param>
		/// <param name="destinationCity">The destinationCity</param>
		/// <returns></returns>
		public async Task<MapInfoResponse> GetDistance(string originCity, string destinationCity)
		{
			var response = new MapInfoResponse();

			try
			{
				var request = new Cities()
				{
					DestinationCity = destinationCity,
					OriginCity = originCity
				};

				// calling google distance microservice
				response.Miles = (await _requestReceiverWrapper.GetDistance(request)).Miles;
			}
			catch (Exception e)
			{
				// in case of any error, the data will be null and errors will be returned as array of string.
				response.Errors = new List<string>()
				{
					e.Message
				};
			}

			return response;
		}

		public async Task<ISSNCodeResponse> CheckISSNCode(string code)
		{
			var response = new ISSNCodeResponse();

			try
			{
				var request = new ISSNCodeInput()
				{
					Code = code
				};

				// calling directly to ISSN code checker microservice
				response.Message = (await _requestReceiverWrapper.CheckISSNCode(request)).Message;
			}
			catch (Exception e)
			{
				// in case of any error, the data will be null and errors will be returned as array of string.
				response.Errors = new List<string>()
				{
					e.Message
				};
			}

			return response;
		}
	}
}
