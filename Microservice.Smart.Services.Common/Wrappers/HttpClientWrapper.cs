using System.Diagnostics.CodeAnalysis;

namespace Microservice.Smart.Services.Common.Wrappers
{
	using System.Net.Http;
	using System.Threading.Tasks;
	using Contracts;

	[ExcludeFromCodeCoverage]
	public class HttpClientWrapper : IHttpClientWrapper
	{
		private readonly HttpClient _httpClient;

		public HttpClientWrapper(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public Task<string> GetStringAsync(string requestUri)
		{
			return _httpClient.GetStringAsync(requestUri);
		}
	}
}
