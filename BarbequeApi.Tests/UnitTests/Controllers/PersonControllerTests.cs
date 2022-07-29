using BarbequeApi.Controllers;
using BarbequeApi.Models.Dtos;
using BarbequeApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace BarbequeApi.Tests.UnitTests
{
    public class PersonControllerTests
    {
        private readonly PersonController controller;
        private readonly Mock<IPersonService> serviceMock = new();

        public PersonControllerTests()
        {
            controller = new PersonController(serviceMock.Object);
        }

        [Fact]
        public async Task CreatePersonForGivenBarbequeSuccess()
        {
            // Arrange
            var barbequeId = "1";
            var obj = new PersonDto();
            serviceMock.Setup(s => s.Create(long.Parse(barbequeId), obj));

            // Act
            var response = await controller.Create(barbequeId, obj);

            // Assert
            var statusResult = Assert.IsType<NoContentResult>(response);
            Assert.Equal((int)HttpStatusCode.NoContent, statusResult.StatusCode);
            serviceMock.Verify(s => s.Create(
                long.Parse(barbequeId), obj), 
                Times.Once,
                "IPersonService.Create should be called once.");
        }

        [Fact]
        public async Task CreatePersonForGivenBarbequeFailure()
        {
            // Arrange
            var barbequeId = "1";
            var obj = new PersonDto();
            serviceMock.Setup(s => s.Create(long.Parse(barbequeId), obj)).Throws(new Exception("random exception"));

            // Act and assert
            Assert.ThrowsAsync<Exception>(() => controller.Create(barbequeId, obj));
            serviceMock.Verify(s => s.Create(
                long.Parse(barbequeId), obj),
                Times.Once,
                "IPersonService.Create should be called once.");
        }

        [Fact]
        public async Task DeletePersonForGivenBarbequeSuccess()
        {
            // Arrange
            var barbequeId = "1";
            var personId = "1";
            var obj = new PersonDto();
            serviceMock.Setup(s => s.Delete(long.Parse(barbequeId), long.Parse(personId)));

            // Act
            var response = await controller.Delete(barbequeId, personId);

            // Assert
            var statusResult = Assert.IsType<NoContentResult>(response);
            Assert.Equal((int)HttpStatusCode.NoContent, statusResult.StatusCode);
            serviceMock.Verify(s => s.Delete(
                long.Parse(barbequeId), long.Parse(personId)),
                Times.Once,
                "IPersonService.Delete should be called once.");
        }

        [Fact]
        public async Task DeletePersonForGivenBarbequeFailure()
        {
            // Arrange
            var barbequeId = "1";
            var personId = "1";
            serviceMock.Setup(s => s.Delete(long.Parse(barbequeId), long.Parse(personId))).Throws(new Exception("random exception"));

            // Act and assert
            Assert.ThrowsAsync<Exception>(() => controller.Delete(barbequeId, personId));
            serviceMock.Verify(s => s.Delete(
                long.Parse(barbequeId), long.Parse(personId)),
                Times.Once,
                "IPersonService.Create should be called once.");
        }
    }
}
