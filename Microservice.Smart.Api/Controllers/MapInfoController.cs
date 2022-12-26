using System.Diagnostics.CodeAnalysis;
using Microservice.Smart.Api.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Smart.Api.Controllers
{
	/// <summary>
	/// The MapInfo Controller
	/// </summary>
	[Route("[controller]/[action]")]
	[ApiController]
	[ExcludeFromCodeCoverage]
	public class MapInfoController : Controller
	{
		private readonly IMapInfoHelper _mapInfoHelper;

		/// <summary>
		/// Create a new instance of MapInfoController
		/// </summary>
		/// <param name="mapInfoHelper">The map info helper</param>
		public MapInfoController(IMapInfoHelper mapInfoHelper)
		{
			_mapInfoHelper = mapInfoHelper;
		}

		/// <summary>
		/// Get distance between two cities.
		/// </summary>
		/// <param name="originCity">The originCity</param>
		/// <param name="destinationCity">The destinationCity</param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> GetDistance(string originCity, string destinationCity)
		{
			// using controller helper class to get the distance from google api
			return Ok(await _mapInfoHelper.GetDistance(originCity, destinationCity));
		}

		/// <summary>
		/// Check ISSN code.
		/// </summary>
		/// <param name="code">The issn code</param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> CheckISSNCode(string code)
		{
			// using controller helper class to check issn code
			return Ok(await _mapInfoHelper.CheckISSNCode(code));
		}
	}
}
