using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentAssert;
using PropertyDataImporter.Models;
using Xunit;

namespace PropertyDataImporter.ScheduleTask.Unit
{
    public class AddressMatcherTests
    {
        private readonly Property _fakeAgProperty;
        private readonly Property _fakeDbProperty;
        private readonly IFixture _fixture;

        public AddressMatcherTests()
        {
            _fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            _fakeAgProperty = _fixture.Create<Property>();
            _fakeDbProperty = _fixture.Create<Property>();
        }

        [Theory(DisplayName = "Getting correct match for the requested name")]
        [InlineData("*Super * -High!APARTMENTS(Sydney)", "Super High Apartments, Sydney",
         "32 Sir John-Young Crescent, Sydney, NSW.", "32 Sir John Young Crescent, Sydney NSW", true)]
        [InlineData("*Super * -High!APARTMENTS(Sydney)", "Super High Apartments, Sydney", "32 Sir John-Young Crescent, ",
         "32 Sir John Young Crescent, Sydney NSW", false)]
        public void WhenIsPropertyAddressMatcherGetCalledReturnCorrectResponse(string agName, string dbName, string agAddress,
         string dbAddress, bool expectedResult)
        {
            //arrange
            _fakeDbProperty.Address = dbAddress;
            _fakeDbProperty.Name = dbName;


            _fakeAgProperty.Address = agAddress;
            _fakeAgProperty.Name = agName;

            var sut = _fixture.Create<AddressMatcher>();

            //act
            var result = sut.IsMatch(_fakeAgProperty, _fakeDbProperty);

            //assert
            result.ShouldBeEqualTo(expectedResult);
        }
    }
}