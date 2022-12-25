namespace Microservice.Smart.Services.Common.Contracts
{
	using System.Threading.Tasks;

	public interface IHttpClientWrapper
	{
		Task<string> GetStringAsync(string requestUri);
	}
}
