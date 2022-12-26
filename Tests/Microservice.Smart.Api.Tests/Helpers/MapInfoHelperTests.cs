using AutoFixture.Xunit2;
using FluentAssertions;
using NSubstitute;
using Microservice.Smart.Api.Contracts;
using Microservice.Smart.Api.Helpers;
using Microservice.Smart.Api.Models;
using Microservice.Smart.Services.RequestReceiver;

namespace Microservice.Smart.Api.Tests.Helpers
{
	public class MapInfoHelperTests
	{
		[Theory]
		[AutoData]
		public async Task GetMapDistance_ReturnsExpected_WhenCalled(string originCity, string destinationCity, DistanceData expectedGoogleDistanceData)
		{
			// arrange
			var dependencies = InitializeDependencies();
			dependencies.requestReceiverWrapper.GetDistance(Arg.Is<Cities>(c => c.OriginCity == originCity && c.DestinationCity == destinationCity))
				.Returns(expectedGoogleDistanceData);

			var expectedResponse = new MapInfoResponse()
			{
				Miles = expectedGoogleDistanceData.Miles,
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
			dependencies.requestReceiverWrapper.GetDistance(Arg.Is<Cities>(c => c.OriginCity == originCity && c.DestinationCity == destinationCity))
				.Returns<DistanceData>(_ => throw exception);

			var expectedResponse = new MapInfoResponse()
			{
				Miles = null,
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

		[Theory]
		[AutoData]
		public async Task CheckISSNCode_ReturnsExpected_WhenCalled(string code, ISSNCodeResult issnCodeResult)
		{
			// arrange
			var dependencies = InitializeDependencies();
			dependencies.requestReceiverWrapper.CheckISSNCode(Arg.Is<ISSNCodeInput>(c => c.Code == code))
				.Returns(issnCodeResult);

			var expectedResponse = new ISSNCodeResponse()
			{
				Message = issnCodeResult.Message,
				Errors = null
			};
			// act
			var actualResponse = await dependencies.mapInfoHelper.CheckISSNCode(code);

			// assert
			actualResponse.Should().BeEquivalentTo(expectedResponse);
		}

		[Theory]
		[AutoData]
		public async Task CheckISSNCode_ReturnsExpectedError_WhenCalled(string code, Exception exception)
		{
			// arrange
			var dependencies = InitializeDependencies();
			dependencies.requestReceiverWrapper.CheckISSNCode(Arg.Is<ISSNCodeInput>(c => c.Code == code))
				.Returns<ISSNCodeResult>(_ => throw exception);

			var expectedResponse = new ISSNCodeResponse()
			{
				Message = null,
				Errors = new List<string>()
				{
					exception.Message
				}
			};
			// act
			var actualResponse = await dependencies.mapInfoHelper.CheckISSNCode(code);

			// assert
			actualResponse.Should().BeEquivalentTo(expectedResponse);
		}
		private (IRequestReceiverWrapper requestReceiverWrapper, IMapInfoHelper mapInfoHelper) InitializeDependencies()
		{
			var requestReceiverWrapper = Substitute.For<IRequestReceiverWrapper>();

			var mapInfoHelper = new MapInfoHelper(requestReceiverWrapper);

			return (requestReceiverWrapper, mapInfoHelper);
		}
	}
}
