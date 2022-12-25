using System.Diagnostics.CodeAnalysis;
using Microservice.Smart.Api.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Smart.Api.Controllers
{
	[Route("[controller]/[action]")]
	[ApiController]
	[ExcludeFromCodeCoverage]
	public class MapInfoController : Controller
	{
		private readonly IMapInfoHelper _mapInfoHelper;

		public MapInfoController(IMapInfoHelper mapInfoHelper)
		{
			_mapInfoHelper = mapInfoHelper;
		}

		[HttpGet]
		public async Task<IActionResult> GetDistance(string originCity, string destinationCity)
		{
			return Ok(await _mapInfoHelper.GetDistance(originCity, destinationCity));
		}
	}
}
