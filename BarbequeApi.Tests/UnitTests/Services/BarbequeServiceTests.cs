using BarbequeApi.Models;
using BarbequeApi.Models.Dtos;
using BarbequeApi.Repositories;
using BarbequeApi.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BarbequeApi.Tests.UnitTests
{
    public class BarbequeServiceTests
    {
        private readonly BarbequeService service;
        private readonly Mock<IBarbequeRepository> repositoryMock = new();
        public BarbequeServiceTests()
        {
            service = new BarbequeService(repositoryMock.Object);
        }

        [Fact]
        public async Task CreateBarbequeSuccess()
        {
            // Arrange
            var barbequeDto = new BarbequeDto
            {
                Title = "Random name",
                Date = new DateTime(2000, 1, 1),
                Notes = "some notes",
                Persons = new List<PersonDto>
                {
                    new PersonDto
                    {
                        Name = "Random name 2",
                        BeverageMoneyShare = 10,
                        FoodMoneyShare = 10
                    }
                }
            };

            repositoryMock.Setup(r => r.Save(It.IsAny<Barbeque>())).Returns(true);

            // Act
            var (successful, errorMessages) = service.Create(barbequeDto);

            // Assert
            Assert.True(successful);
            Assert.Empty(errorMessages);
            repositoryMock.Verify(
              r => r.Save(It.IsAny<Barbeque>()),
              Times.Once,
              "IBarbequeRepository.Save should be called once.");
        }

        [Fact]
        public async Task GetBarbequeSuccess()
        {
            // Arrange
            var barbequeId = 1l;
            var expectedBarbeque = new BarbequeDto { Title = "example", Persons = new List<PersonDto>() };

            repositoryMock.Setup(r => r.Get(barbequeId)).Returns(
              new Barbeque
              {
                  Id = barbequeId,
                  Title = "example",
                  Persons = new List<Person>()
              });

            // Act
            var (barbeque, errorMessages) = service.Get(barbequeId.ToString());

            // Assert
            Assert.NotNull(barbeque);
            Assert.Empty(errorMessages);
            Assert.Equal(barbequeId, barbeque.Id);
            Assert.Equal(expectedBarbeque.Title, barbeque.Title);
            Assert.Equal(expectedBarbeque.Persons.Count, barbeque.Persons.Count);
            repositoryMock.Verify(r => r.Get(barbequeId), Times.Once, "IBarbequeRepository.Get should be called once.");
        }

        [Fact]
        public async Task CreateBarbequeFailure() // translator test?
        {

        }

        [Fact]
        public async Task CreateBarbequeWhenDatabaseFailsGivesFailure()
        {
            // Arrange
            var barbequeDto = new BarbequeDto
            {
                Title = "Random name",
                Date = new DateTime(2000, 1, 1),
                Notes = "some notes",
                Persons = new List<PersonDto>
                {
                    new PersonDto
                    {
                        Name = "Random name 2",
                        BeverageMoneyShare = 10,
                        FoodMoneyShare = 10
                    }
                }
            };

            repositoryMock.Setup(r => r.Save(It.IsAny<Barbeque>())).Throws(new Exception("any db exception"));

            // Act
            Assert.Throws<Exception>(() => service.Create(barbequeDto));

            // Assert
            repositoryMock.Verify(
              r => r.Save(It.IsAny<Barbeque>()),
              Times.Once,
              "IBarbequeRepository.Save should be called once.");
        }

        [Fact]
        public async Task CreateBarbequeShouldReturnProperErrorMessageWhenDatabaseDontFound()
        {
            // Arrange
            var barbequeDto = new BarbequeDto
            {
                Title = "Random name",
                Date = new DateTime(2000, 1, 1),
                Notes = "some notes",
                Persons = new List<PersonDto>
                {
                    new PersonDto
                    {
                        Name = "Random name 2",
                        BeverageMoneyShare = 10,
                        FoodMoneyShare = 10
                    }
                }
            };

            repositoryMock.Setup(r => r.Save(It.IsAny<Barbeque>())).Returns(false);

            // Act
            var (successful, errorMessages) = service.Create(barbequeDto);

            // Assert
            Assert.False(successful);
            Assert.Equal("500: Error in barbequeRepository.Save()", errorMessages.Last());
            repositoryMock.Verify(
              r => r.Save(It.IsAny<Barbeque>()),
              Times.Once,
              "IBarbequeRepository.Save should be called once.");
        }

        [Fact]
        public async Task GetBarbequeFailure() // error for id <=0, db error (null or exception) and translator
        {

        }

        [Fact]
        public async Task GetBarbequeGivesErrorWhenIdIsNotValid() // id = 0 ou = -1
        {

        }

        [Fact]
        public async Task GetBarbequeGivesErrorWhenDbThrowsAnException()
        {
            // Arrange
            var barbequeId = 1L;
            repositoryMock.Setup(r => r.Get(barbequeId)).Throws(new Exception("any db exception"));

            // Act
            Assert.Throws<Exception>(() => service.Get(barbequeId.ToString()));

            // Assert
            repositoryMock.Verify(
              r => r.Get(barbequeId),
              Times.Once,
              "IBarbequeRepository.Save should be called once.");
        }
    }
}
