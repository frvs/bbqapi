using BarbequeApi.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BarbequeApi.Tests.UnitTests.Dtos
{
    public class BarbequeDtoTests
    {
        [Fact]
        public async Task BarbequeDtoDateFormatted()
        {
            var bbqDto = new BarbequeDto()
            {
                Date = new DateTime(2007, 02, 01)
            };

            Assert.Equal("01/02", bbqDto.DateFormatted);
        }

        [Fact]
        public async Task BarbequeDtoMoneyInvested()
        {
            var bbqDto = new BarbequeDto()
            {
                Persons = new List<PersonDto>
                {
                    new PersonDto()
                    {
                        Name = "x",
                        BeverageMoneyShare = 10,
                        FoodMoneyShare = 15.25M
                    },
                    new PersonDto()
                    {
                        Name = "x",
                        BeverageMoneyShare = 10,
                        FoodMoneyShare = -15.25M
                    }
                }
            };

            Assert.Equal(20, bbqDto.MoneyInvested);
        }
    }
}
