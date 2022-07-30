using BarbequeApi.Models;
using BarbequeApi.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace BarbequeApi.Tests.IntegrationTests
{
    public class PersonControllerIntegrationTests : IClassFixture<TestWebApp<Program>>
    {
        private readonly TestWebApp<Program> factory;

        public HttpClient HttpClient { get; }

        public PersonControllerIntegrationTests(TestWebApp<Program> factory)
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
            var dbContext = factory.GetDbContext();
            var personDto = new PersonDto
            {
                Name = personName,
                BeverageMoneyShare = 20,
                FoodMoneyShare = 20
            };

            // Act
            var (statusCode, _) = await HttpClient.PostAsync($"api/barbeques/{barbequeId}/persons", personDto);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, statusCode);
            var barbeque = dbContext.Barbeques.Include(b => b.Persons).FirstOrDefault(b => b.Id == barbequeId);
            Assert.NotNull(barbeque);
            var desiredPerson = barbeque.Persons.FirstOrDefault(p => p.Name == personName);
            Assert.NotNull(desiredPerson);
            Assert.Equal(barbeque.Id, desiredPerson.BarbequeId);
            var expectedPerson = Translator.ToPerson(personDto);
            AssertExpectedPersonEqualsToSaved(expectedPerson, desiredPerson);
        }

        [Fact]
        public async Task DeletePersonForGivenBarbequeSuccess()
        {
            // Arrange
            long barbequeId = 0;
            long personId = 0;

            using (var dbContext = factory.GetDbContext())
            {
                var barbeque = new Barbeque
                {
                    Title = "Test DeletePersonForGivenBarbequeSuccess",
                    Date = new DateTime(2000, 1, 1),
                    Persons = new List<Person>
                    {
                        new Person
                        {
                            Name = "Test",
                            BeverageMoneyShare = 10,
                            FoodMoneyShare = 10
                        }
                    }
                };

                dbContext.Barbeques.Add(barbeque);
                dbContext.SaveChanges();

                var savedBarbeque = dbContext.Barbeques.FirstOrDefault(b => b.Title == "Test DeletePersonForGivenBarbequeSuccess");
                Assert.NotNull(savedBarbeque);
                barbequeId = savedBarbeque.Id;
                personId = savedBarbeque.Persons.First().Id;
            }

            // Act
            var statusCode = await HttpClient.Delete($"api/barbeques/{barbequeId}/persons/{personId}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, statusCode);
            using (var dbContext = factory.GetDbContext())
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
            Assert.Equal(expectedPerson.FoodMoneyShare, desiredPerson.FoodMoneyShare);
            Assert.Equal(expectedPerson.BeverageMoneyShare, desiredPerson.BeverageMoneyShare);
        }
    }
}
