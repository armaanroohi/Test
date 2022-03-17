using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentAssert;
using PropertyDataImporter.Models;
using Xunit;

namespace PropertyDataImporter.ScheduleTask.Unit
{
    public class ReverseNameMatcherTests
    {
        private readonly Property _fakeAgProperty;
        private readonly Property _fakeDbProperty;
        private readonly IFixture _fixture;

        public ReverseNameMatcherTests()
        {
            _fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            _fakeAgProperty = _fixture.Create<Property>();
            _fakeDbProperty = _fixture.Create<Property>();
        }

        [Theory(DisplayName = "When property name is existed in reverse order it should return correct response")]
        [InlineData("Apartments Summit The", "The Summit Apartments", true)]
        [InlineData("Apartments Summit The", "Summit The Apartments", false)]
        public void IsPropertyNameMatcherInReverse(string agName, string dbName, bool expectedResult)
        {
            //arrange
            _fakeDbProperty.Name = dbName;
            _fakeAgProperty.Name = agName;
            var sut = _fixture.Create<ReverseNameMatcher>();

            //act
            var result = sut.IsMatch(_fakeAgProperty, _fakeDbProperty);

            //assert
            result.ShouldBeEqualTo(expectedResult);
        }
    }
}