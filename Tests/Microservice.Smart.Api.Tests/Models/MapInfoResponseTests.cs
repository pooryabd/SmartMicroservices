using AutoFixture.Xunit2;
using FluentAssertions;
using Microservice.Smart.Api.Models;

namespace Microservice.Smart.Api.Tests.Models
{
	public class MapInfoResponseTests
	{
		[Theory, AutoData]
		public void MapInfoResponse_ReturnsExpectedProperties_WhenCalled(MapInfoResponse expectedMapInfoResponse)
		{
			// arrange and act
			var actualMapInfoResponse = new MapInfoResponse()
			{
				Data = expectedMapInfoResponse.Data,
				Errors = expectedMapInfoResponse.Errors
			};

			// assert
			actualMapInfoResponse.Should().BeEquivalentTo(expectedMapInfoResponse);
		}
	}
}
