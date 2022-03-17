using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentAssert;
using PropertyDataImporter.Models;
using Xunit;

namespace PropertyDataImporter.ScheduleTask.Unit
{
    public class DistanceMatcherTests
    {
        private readonly Property _fakeAgProperty;
        private readonly Property _fakeDbProperty;
        private readonly IFixture _fixture;

        public DistanceMatcherTests()
        {
            _fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            _fakeAgProperty = _fixture.Create<Property>();
            _fakeDbProperty = _fixture.Create<Property>();
        }

        [Theory(DisplayName = "When property is in less than 200 metter distance we should return correct response")]
        [InlineData(10.285, 11.300, 10.288, 11.500, false)]
        [InlineData(10.285, 11.301, 10.286, 11.300, true)]
        public void WhenIsPropertyDistanceMatcherGetCalledReturnCorrectResponse(decimal agLat, decimal agLong, decimal dbLat,
         decimal dbLong, bool expectedResult)
        {
            //arrange
            _fakeAgProperty.Latitude = agLat;
            _fakeAgProperty.Longitude = agLong;

            _fakeDbProperty.Latitude = dbLat;
            _fakeDbProperty.Longitude = dbLong;

            var sut = _fixture.Create<DistanceMatcher>();

            //act
            var result = sut.IsMatch(_fakeAgProperty, _fakeDbProperty);

            //assert
            result.ShouldBeEqualTo(expectedResult);
        }
    }
}