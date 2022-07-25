using System.Threading.Tasks;
using Xunit;

namespace BarbequeApi.Tests.IntegrationTests
{
    public class PersonControllerIntegrationTests
    {
        [Fact]
        public async Task AddPersonToExistingBarbequeSuccess()
        {

        }

        [Fact]
        public async Task DeletePersonForGivenBarbequeSuccess()
        {

        }

        [Fact]
        public async Task TryToAddPersonToNonExistingBarbequeFailure()
        {

        }

        [Fact]
        public async Task TryToAddExistingPersonToExistingBarbequeFailure() // pretends to be an update via POST
        {

        }

        [Fact]
        public async Task TryToDeleteNonExistingPersonToExistingBarbequeFailure()
        {

        }

        [Fact]
        public async Task TryToDeleteExistingPersonToExistingBarbequeFailure()
        {

        }
    }
}
