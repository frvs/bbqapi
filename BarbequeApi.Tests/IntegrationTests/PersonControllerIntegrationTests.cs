using BarbequeApi.Models;
using BarbequeApi.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BarbequeApi.Tests.IntegrationTests
{
    public class PersonControllerIntegrationTests : IClassFixture<CustomWebApp<Program>>
    {
        private readonly CustomWebApp<Program> factory;

        public HttpClient HttpClient { get; }

        public PersonControllerIntegrationTests(CustomWebApp<Program> factory)
        {
            HttpClient = factory.CreateClient();
            this.factory = factory;
        }


        [Fact]
        public async Task AddPersonToExistingBarbequeSuccess()
        {
            // Arrange
            long barbequeId = 1;
            var personName = "Fulano Testavel";
            var dbContext = factory.GetDbContext(factory);
            var personDto = new PersonDto
            {
                Name = personName,
                DrinksMoney = 20,
                FoodsMoney = 20
            };

            // Act
            var httpResponse = await HttpClient.PostAsync(
                $"api/barbeques/{barbequeId}/persons", 
                new StringContent(JToken.FromObject(personDto).ToString(), Encoding.UTF8, "application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, httpResponse.StatusCode);
            var barbeque = dbContext.Barbeques.Include(b => b.Persons).FirstOrDefault(b => b.Id == barbequeId);
            Assert.NotNull(barbeque);
            var desiredPerson = barbeque.Persons.FirstOrDefault(p => p.Name == personName);
            Assert.NotNull(desiredPerson);
            Assert.Equal(barbeque.Id, desiredPerson.BarbequeId);
            var expectedPerson = Translator.ToPerson(personDto);
            AssertExpectedPersonEqualsToSaved(expectedPerson, desiredPerson);            
        }

        [Fact]
        public async Task AddTwoPersonsToExistingBarbequeAndVerifyMoneyPredictionSuccess()
        {
            // Arrange
            long barbequeId = 1;
            var personName1 = "Fulano Testavel";
            var personName2 = "Fulano Testavel com predicao";
            var dbContext = factory.GetDbContext(factory);
            var personDto1 = new PersonDto
            {
                Name = personName1,
                DrinksMoney = 20,
                FoodsMoney = 20
            };

            var personDto2 = new PersonDto
            {
                Name = personName2,
                DrinksMoney = 0,
                FoodsMoney = 0
            };


            // Act
            _ = await HttpClient.PostAsync(
                $"api/barbeques/{barbequeId}/persons",
                new StringContent(JToken.FromObject(personDto1).ToString(), Encoding.UTF8, "application/json"));
            var httpResponse = await HttpClient.PostAsync(
                $"api/barbeques/{barbequeId}/persons",
                new StringContent(JToken.FromObject(personDto2).ToString(), Encoding.UTF8, "application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, httpResponse.StatusCode);
            var barbeque = dbContext.Barbeques.Include(b => b.Persons).FirstOrDefault(b => b.Id == barbequeId);
            Assert.NotNull(barbeque);
            Assert.True(barbeque.Persons.Count > 2);
            var desiredPerson = barbeque.Persons.FirstOrDefault(p => p.Name == personName2);
            Assert.NotNull(desiredPerson);
            Assert.Equal(barbeque.Id, desiredPerson.BarbequeId);
            Assert.Equal(personDto2.Name, desiredPerson.Name);
            Assert.Equal(35, desiredPerson.DrinksMoney); 
            Assert.Equal(20, desiredPerson.FoodsMoney); 
            // seededPerson.DrinksMoney=50; person1.DrinksMoney=20; (50 + 20) / 2 (count) = 35
            // seededPerson.FoodsMoney=20; person1.FoodsMoney=20; (20 + 20) / 2 = 20
        }


        [Fact]
        public async Task DeletePersonForGivenBarbequeSuccess()
        {
            // Arrange
            long barbequeId = 1;
            long personId = 1;

            using (var dbContext = factory.GetDbContext(factory))
            {
                var barbeque = dbContext.Barbeques.Include(b => b.Persons).FirstOrDefault(b => b.Id == barbequeId);
                Assert.NotNull(barbeque);
                var existingPerson = barbeque.Persons.FirstOrDefault(a => a.Id == personId);
                Assert.NotNull(existingPerson);
            }
            

            // Act
            var httpResponse = await HttpClient.DeleteAsync($"api/barbeques/{barbequeId}/persons/{personId}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, httpResponse.StatusCode);
            using (var dbContext = factory.GetDbContext(factory))
            {
                var updatedBarbeque = dbContext.Barbeques.Include(b => b.Persons).FirstOrDefault(b => b.Id == barbequeId);
                Assert.NotNull(updatedBarbeque);
                var nonExistingPerson = updatedBarbeque.Persons.FirstOrDefault(a => a.Id == personId);
                Assert.Null(nonExistingPerson);
            }
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

        private void AssertExpectedPersonEqualsToSaved(Person expectedPerson, Person desiredPerson)
        {
            Assert.Equal(expectedPerson.Name, desiredPerson.Name);
            Assert.Equal(expectedPerson.FoodsMoney, desiredPerson.FoodsMoney);
            Assert.Equal(expectedPerson.DrinksMoney, desiredPerson.DrinksMoney);
        }
    }
}
