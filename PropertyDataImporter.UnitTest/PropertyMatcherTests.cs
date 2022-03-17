using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentAssert;
using NSubstitute;
using PropertyDataImporter.Models;
using Serilog;
using System;
using Xunit;

namespace PropertyDataImporter.ScheduleTask.Unit
{
    public class PropertyMatcherTests
    {
        public PropertyMatcherTests()
        {
            _fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            _fakeAgProperty = _fixture.Create<Property>();
            _fakeDbProperty = _fixture.Create<Property>();
        }

        private readonly IFixture _fixture;
        private readonly Property _fakeDbProperty;
        private readonly Property _fakeAgProperty;

        [Theory(DisplayName = "When the agency code pass to the create matcher it should return expceted type")]
        [InlineData("OTBRE", typeof(AddressMatcher))]
        [InlineData("LRE", typeof(DistanceMatcher))]
        [InlineData("CRE", typeof(ReverseNameMatcher))]
        public void WhenAgencyPropertCreateMatcherReturnSameType(string agCode, Type matcherType)
        {
            //arrange
            _fakeAgProperty.AgencyCode = agCode;
            var sut = _fixture.Create<PropertyMatcher>();

            //act
            var result = sut.CreateMatcher(_fakeAgProperty);
            //assert
            result.GetType().ShouldBeEqualTo(matcherType);
        }

        [Fact(DisplayName = "If there is no match should log the details")]
        public void WhenIsMatchGettingCalledItShouldLogInCaseOfNoMatch()
        {
            //arrange
            var logger = _fixture.Freeze<ILogger>();
            var sut = _fixture.Create<PropertyMatcher>();

            //act
            Record.Exception(() => sut.IsMatch(_fakeAgProperty, _fakeDbProperty));

            //asert
            logger.Received().Error("There is no match for agency code {:@agencyCode}", _fakeAgProperty.AgencyCode);
        }

        [Fact(DisplayName = "If there is no match throw exception")]
        public void WhenIsMatchGettingCalledItShouldThrowExceptionInCaseOfNoMatch()
        {
            //arrange
            var logger = _fixture.Freeze<ILogger>();
            var sut = _fixture.Create<PropertyMatcher>();

            //act
            Action action = () => sut.IsMatch(_fakeAgProperty, _fakeDbProperty);

            //asert
            AssertExtensions.ShouldThrow<Exception>(action);
            logger.Received().Error("There is no match for agency code {:@agencyCode}", _fakeAgProperty.AgencyCode);
        }
    }
}