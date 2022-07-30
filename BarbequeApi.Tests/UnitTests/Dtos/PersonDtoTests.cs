using BarbequeApi.Models.Dtos;
using System.Threading.Tasks;
using Xunit;

namespace BarbequeApi.Tests.UnitTests.Dtos
{
    public class PersonDtoTests
    {
        [Theory]
        [InlineData("t", "T")]
        [InlineData("Test", "Test")]
        [InlineData("Test ", "Test")]
        [InlineData("Test X", "Test X.")]
        [InlineData("Test x", "Test X.")]
        [InlineData("test x", "Test X.")]
        [InlineData("test xy", "Test X.")]
        [InlineData("test xyz", "Test X.")]
        [InlineData("", "")]
        [InlineData("  ", "")]
        [InlineData(null, "")]
        public async Task PersonDtoNameFormatted(string name, string nameFormatted)
        {
            var personDto = new PersonDto()
            {
                Name = name
            };

            Assert.Equal(nameFormatted, personDto.NameFormatted);
        }
    }
}
