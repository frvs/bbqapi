using BarbequeApi.Models.Dtos;
using BarbequeApi.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BarbequeApi.Tests.UnitTests
{
    public class BarbequeValidatorTests
    {
        [Fact]
        public async Task ValidateBarbequeDtoReturnsFalseBeucaseOfBeingNull()
        {
            var barbequeValidator = new BarbequeValidator();
            var (successful, errorMessages) = barbequeValidator.Validate(null);
            Assert.False(successful);
            Assert.Single(errorMessages);
            Assert.Equal("400: BarbequeDto should not be null.", errorMessages.First());
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task ValidateBarbequeDtoReturnsFalseBeucaseOfTitle(string title)
        {
            var barbequeValidator = new BarbequeValidator();
            var (successful, errorMessages) = barbequeValidator.Validate(new BarbequeDto
            {
                Title = title,
                Persons = null,
                Date = new DateTime(2000, 1, 1)
            });
            Assert.False(successful);
            Assert.Single(errorMessages);
            Assert.Equal(
              "400: BarbequeDto.Title should not be null, empty string or white spaces.",
              errorMessages.First());
        }

        [Fact]
        public async Task ValidateBarbequeDtoReturnsFalseBeucaseInvalidPersons()
        {
            var barbequeValidator = new BarbequeValidator();

            var (successful, errorMessages) = barbequeValidator.Validate(new BarbequeDto
            {
                Title = "Barbeque",
                Persons = new List<PersonDto>()
                {
                    new PersonDto()
                    {
                        Name = "Invalid test user",
                        FoodMoneyShare = -10
                    },
                    new PersonDto()
                    {
                        Name = "Valid test user"
                    }
                },
                Date = new DateTime(2000, 1, 1)
            });

            Assert.False(successful);
            Assert.Single(errorMessages);
            Assert.Equal(
              "400: BarbequeDto.Person[0] is not valid: 400: PersonDto.FoodMoneyShare should not be negative.",
              errorMessages.First());
        }

        [Theory]
        [MemberData(nameof(DateTimeRangeForSqlServer))]
        public async Task ValidateBarbequeDtoReturnsFalseBecauseOfSqlServerDateTimeLimitations(DateTime date)
        {
            var barbequeValidator = new BarbequeValidator();

            var (successful, errorMessages) = barbequeValidator.Validate(new BarbequeDto
            {
                Title = "Barbeque",
                Persons = new List<PersonDto>()
                {
                    new PersonDto()
                    {
                        Name = "Valid test user"
                    }
                },
                Date = date
            });

            if (successful)
            {
                Assert.Empty(errorMessages);
            }

            if (!successful)
            {
                Assert.Single(errorMessages);
                Assert.Equal(
                  "400: BarbequeDto.Date should be equal or after January 1st, 1753.",
                  errorMessages.First());
            }
        }

        public static IEnumerable<object[]> DateTimeRangeForSqlServer =>
        new List<object[]>
        {
            new object[] { new DateTime(1751, 11, 30) },
            new object[] { new DateTime(1753, 1, 1) },
            new object[] { new DateTime(2000, 1, 1) }
        };

    }
}
