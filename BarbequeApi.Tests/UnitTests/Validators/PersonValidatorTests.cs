using BarbequeApi.Models.Dtos;
using BarbequeApi.Validators;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BarbequeApi.Tests.UnitTests
{
    public class PersonValidatorTests
    {
        [Fact]
        public async Task ValidatePersonDtoReturnsFalseBeucaseOfBeingNull()
        {
            var personValidator = new PersonValidator();
            var (successful, errorMessages) = personValidator.Validate(null);
            Assert.False(successful);
            Assert.Single(errorMessages);
            Assert.Equal("400: PersonDto should not be null.", errorMessages.First());
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task ValidatePersonDtoReturnsFalseBeucaseOfName(string name)
        {
            var barbequeValidator = new PersonValidator();
            var (successful, errorMessages) = barbequeValidator.Validate(new PersonDto()
            {
                Name = name,
                BeverageMoneyShare = 10,
                FoodMoneyShare = 10
            });
            Assert.False(successful);
            Assert.Single(errorMessages);
            Assert.Equal(
              "400: PersonDto.Name should not be null, empty string or white spaces.",
              errorMessages.First());
        }

        [Fact]
        public async Task ValidatepersonDtoReturnsFalseBeucaseOfNegativeMoneyShare()
        {
            var barbequeValidator = new PersonValidator();
            var (successful, errorMessages) = barbequeValidator.Validate(new PersonDto()
            {
                Name = "Test person",
                BeverageMoneyShare = -10,
                FoodMoneyShare = -50
            });
            Assert.False(successful);
            Assert.Equal(2, errorMessages.Count);
            Assert.Equal(
              "400: PersonDto.BeverageMoneyShare should not be negative.",
              errorMessages.First());
            Assert.Equal(
              "400: PersonDto.FoodMoneyShare should not be negative.",
              errorMessages.Last());
        }

    }
}
