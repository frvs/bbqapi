using BarbequeApi.Models;
using BarbequeApi.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BarbequeApi.Tests.IntegrationTests
{
    public class BarbequeControllerIntegrationTests : IClassFixture<TestWebApp<Program>>
    {
        private readonly TestWebApp<Program> factory;

        public HttpClient HttpClient { get; }

        public BarbequeControllerIntegrationTests(TestWebApp<Program> factory)
        {
            HttpClient = factory.CreateClient();
            this.factory = factory;
        }


        [Fact]
        public async Task CreateBarbequeWithoutPersonsSuccess()
        {
            // Arrange
            var bbqTitle = "Churrasco de teste sem pessoas";

            var barbequeDto = new BarbequeDto
            {
                Title = bbqTitle
            };

            // Act
            var httpResponse = await HttpClient.PostAsync(
                $"api/barbeques",
                new StringContent(JToken.FromObject(barbequeDto).ToString(), Encoding.UTF8, "application/json"));


            // Assert
            Assert.Equal(HttpStatusCode.NoContent, httpResponse.StatusCode);
            var dbContext = factory.GetDbContext(factory);
            var actualBarbeque = dbContext.Barbeques
                .FirstOrDefault(barbeque => barbeque.Title == bbqTitle);

            Assert.NotNull(actualBarbeque);
            Assert.NotEqual(0, actualBarbeque.Id);
            Assert.Equal(barbequeDto.Title, actualBarbeque.Title);
        }

        [Fact]
        public async Task CreateBarbequeWithOnePersonSuccess()
        {
            // Arrange
            var bbqTitle = "Churrasco de teste com uma pessoa";

            var barbequeDto = new BarbequeDto
            {
                Title = bbqTitle, 
                Date = new DateTime(2000, 1, 1), 
                Notes = "algumas notas",
                Persons = new List<PersonDto>
                {
                    new PersonDto
                    {
                        BeverageMoneyShare = 20, 
                        FoodMoneyShare = 20, 
                        Name = "Pessoa teste"
                    }
                }
            };

            // Act
            var httpResponse = await HttpClient.PostAsync(
                $"api/barbeques",
                new StringContent(JToken.FromObject(barbequeDto).ToString(), Encoding.UTF8, "application/json"));


            // Assert
            Assert.Equal(HttpStatusCode.NoContent, httpResponse.StatusCode);
            var dbContext = factory.GetDbContext(factory);
            var actualBarbeque = dbContext.Barbeques.Include(b => b.Persons)
                .FirstOrDefault(barbeque => barbeque.Title == bbqTitle);

            Assert.NotNull(actualBarbeque);
            Assert.NotEqual(0, actualBarbeque.Id);

            var expectedBarbeque = Translator.ToBarbeque(barbequeDto);
            foreach (var person in expectedBarbeque.Persons)
            {
                person.BarbequeId = actualBarbeque.Id;
            }

            AssertExpectedBarbequeEqualsToSaved(expectedBarbeque, actualBarbeque);
        }

        [Fact]
        public async Task CreateBarbequeWithTwoPersonsSuccess()
        {
            // Arrange
            var bbqTitle = "Churrasco de teste com duas pessoas";

            var barbequeDto = new BarbequeDto
            {
                Title = bbqTitle,
                Date = new DateTime(2000, 1, 1),
                Notes = "algumas notas mais aleatorias",
                Persons = new List<PersonDto>
                {
                    new PersonDto
                    {
                        BeverageMoneyShare = 20,
                        FoodMoneyShare = 20,
                        Name = "Pessoa teste 1"
                    },
                    new PersonDto
                    {
                        BeverageMoneyShare = 0,
                        FoodMoneyShare = 0,
                        Name = "Pessoa teste 2"
                    }
                }
            };

            // Act
            var httpResponse = await HttpClient.PostAsync(
                $"api/barbeques",
                new StringContent(JToken.FromObject(barbequeDto).ToString(), Encoding.UTF8, "application/json"));


            // Assert
            Assert.Equal(HttpStatusCode.NoContent, httpResponse.StatusCode);
            var dbContext = factory.GetDbContext(factory);
            var actualBarbeque = dbContext.Barbeques.Include(b => b.Persons)
                .FirstOrDefault(barbeque => barbeque.Title == bbqTitle);

            Assert.NotNull(actualBarbeque);
            Assert.NotEqual(0, actualBarbeque.Id);

            var expectedBarbeque = Translator.ToBarbeque(barbequeDto);
            foreach (var person in expectedBarbeque.Persons)
            {
                person.BarbequeId = actualBarbeque.Id;
            }

            AssertExpectedBarbequeEqualsToSaved(expectedBarbeque, actualBarbeque);
        }

        [Fact]
        public async Task GetBarbequeSuccess()
        {
            // Arrange
            var now = new DateTime(2000, 1, 1);
            var barbequeToInsert = new Barbeque
            {
                Title = "Motivo de teste",
                Date = now,
                Notes = "algumas notas aleatorias",
                Persons = new List<Person>
                {
                    new Person
                    {
                        Name = "Fulano Silva",
                        BeverageMoneyShare = 10,
                        FoodMoneyShare = 10
                    }
                }
            };

            var dbContext = factory.GetDbContext(factory);

            dbContext.Barbeques.Add(barbequeToInsert);
            var successful = dbContext.SaveChanges() > 0;
            var desiredId = dbContext.Barbeques.First(b => b.Title == "Motivo de teste").Id;

            // Act
            var httpResponse = await HttpClient.GetAsync($"api/barbeques/{desiredId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            var actualBarbeque = JToken.Parse(responseAsString).ToObject<Barbeque>();
            Assert.NotNull(actualBarbeque);
            Assert.True(successful);
            Assert.NotEqual(0, actualBarbeque.Id);
            Assert.Equal(desiredId, actualBarbeque.Id);
            AssertExpectedBarbequeEqualsToSaved(barbequeToInsert, actualBarbeque);
        }

        [Fact]
        public async Task TryToGetNonExistingBarbequeFailure()
        {
            // Arrange

            // Act
            var httpResponse = await HttpClient.GetAsync($"api/barbeques/{-1}");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        }

        private void AssertExpectedBarbequeEqualsToSaved(Barbeque barbequeToInsert, Barbeque actualBarbeque)
        {
            Assert.Equal(barbequeToInsert.Title, actualBarbeque.Title);
            Assert.Equal(barbequeToInsert.Date, actualBarbeque.Date);
            Assert.Equal(barbequeToInsert.Notes, actualBarbeque.Notes);

            Assert.Equal(barbequeToInsert.Persons.Count, actualBarbeque.Persons.Count);

            var expectedPersons = barbequeToInsert.Persons.ToList();
            var actualPersons = actualBarbeque.Persons.ToList();
            for (int i = 0; i < expectedPersons.Count; i++) // im aware of assert.collection and assert.all but...
            {
                var expectedPersonToVerify = expectedPersons[i];
                var actualPersonToVerify = actualPersons[i];
                Assert.Equal(expectedPersonToVerify.Name, actualPersonToVerify.Name);
                Assert.Equal(expectedPersonToVerify.FoodMoneyShare, actualPersonToVerify.FoodMoneyShare);
                Assert.Equal(expectedPersonToVerify.BeverageMoneyShare, actualPersonToVerify.BeverageMoneyShare);
                Assert.Equal(expectedPersonToVerify.BarbequeId, actualPersonToVerify.BarbequeId);
            }
        }
    }
}
