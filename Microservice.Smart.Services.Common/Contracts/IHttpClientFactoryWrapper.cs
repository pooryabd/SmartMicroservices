namespace Microservice.Smart.Services.Common.Contracts
{
	public interface IHttpClientFactoryWrapper
	{
		IHttpClientWrapper CreateClient(string name = "");
	}
}
