using AutoFixture.Xunit2;
using FluentAssertions;
using Microservice.Smart.Services.GoogleMapInfo.Contracts;
using NSubstitute;
using Microservice.Smart.Api.Contracts;
using Microservice.Smart.Api.Helpers;
using Microservice.Smart.Services.GoogleMapInfo.Models.Responses;
using Microservice.Smart.Api.Models;

namespace Microservice.Smart.Api.Tests.Helpers
{
	public class MapInfoHelperTests
	{
		[Theory]
		[AutoData]
		public async Task GetMapDistance_ReturnsExpected_WhenCalled(string originCity, string destinationCity, GoogleDistanceData expectedGoogleDistanceData)
		{
			// arrange
			var dependencies = InitializeDependencies();
			dependencies.googleDistanceService.GetMapDistance(originCity, destinationCity)
				.Returns(expectedGoogleDistanceData);

			var expectedResponse = new MapInfoResponse()
			{
				Data = expectedGoogleDistanceData,
				Errors = null
			};
			// act
			var actualResponse = await dependencies.mapInfoHelper.GetDistance(originCity, destinationCity);

			// assert
			actualResponse.Should().BeEquivalentTo(expectedResponse);
		}

		[Theory]
		[AutoData]
		public async Task GetMapDistance_ReturnsExpectedError_WhenCalled(string originCity, string destinationCity, Exception exception)
		{
			// arrange
			var dependencies = InitializeDependencies();
			dependencies.googleDistanceService.GetMapDistance(originCity, destinationCity)
				.Returns<GoogleDistanceData>(_ => throw exception);

			var expectedResponse = new MapInfoResponse()
			{
				Data = null,
				Errors = new List<string>()
				{
					exception.Message,
				}
			};
			// act
			var actualResponse = await dependencies.mapInfoHelper.GetDistance(originCity, destinationCity);

			// assert
			actualResponse.Should().BeEquivalentTo(expectedResponse);
		}
		private (IGoogleDistanceService googleDistanceService, IMapInfoHelper mapInfoHelper) InitializeDependencies()
		{
			var googleDistanceService = Substitute.For<IGoogleDistanceService>();

			var mapInfoHelper = new MapInfoHelper(googleDistanceService);

			return (googleDistanceService, mapInfoHelper);
		}
	}
}
