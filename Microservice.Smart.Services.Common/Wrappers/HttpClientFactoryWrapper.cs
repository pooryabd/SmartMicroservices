using System.Diagnostics.CodeAnalysis;

namespace Microservice.Smart.Services.Common.Wrappers
{
	using Contracts;

	[ExcludeFromCodeCoverage]
	public class HttpClientFactoryWrapper : IHttpClientFactoryWrapper
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public HttpClientFactoryWrapper(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public IHttpClientWrapper CreateClient(string name = "")
		{
			var httpClient = string.IsNullOrEmpty(name) ? _httpClientFactory.CreateClient() : _httpClientFactory.CreateClient(name);

			return new HttpClientWrapper(httpClient);
		}
	}
}
