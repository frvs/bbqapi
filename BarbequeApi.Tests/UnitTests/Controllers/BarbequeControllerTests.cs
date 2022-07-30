using BarbequeApi.Controllers;
using BarbequeApi.Models.Dtos;
using BarbequeApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace BarbequeApi.Tests.UnitTests
{
    public class BarbequeControllerTests
    {
        private readonly BarbequeController controller;
        private readonly Mock<IBarbequeService> serviceMock = new();

        public BarbequeControllerTests()
        {
            controller = new BarbequeController(serviceMock.Object);
        }

        [Fact]
        public async Task CreateBarbequeSuccess()
        {
            // Arrange
            var barbequeDto = new BarbequeDto();
            serviceMock.Setup(s => s.Create(barbequeDto)).Returns((true, new List<string>()));

            // Act
            var response = await controller.Create(barbequeDto);

            // Assert
            var result = Assert.IsType<NoContentResult>(response);
            Assert.Equal((int)HttpStatusCode.NoContent, result.StatusCode);
            serviceMock.Verify(s => s.Create(barbequeDto), Times.Once, "IBarbequeService.Create should be called once.");
        }

        [Fact]
        public async Task GetBarbequeSuccess()
        {
            // Arrange
            var barbequeId = "1";
            serviceMock.Setup(s => s.Get(barbequeId)).Returns((new BarbequeDto(), new List<string>()));

            // Act
            var response = await controller.Get(barbequeId);

            // Assert
            var result = Assert.IsType<OkObjectResult>(response);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            serviceMock.Verify(s => s.Get(barbequeId), Times.Once, "IBarbequeService.Get should be called once.");
        }

        [Fact]
        public async Task CreateBarbequeFailure()
        {
            // TODO: refactor

            // Arrange
            var barbequeDto = new BarbequeDto();
            serviceMock.Setup(s => s.Create(barbequeDto)).Throws(new Exception("random exception"));

            // Act
            Assert.ThrowsAsync<Exception>(() => controller.Create(barbequeDto));

            serviceMock.Verify(s => s.Create(barbequeDto), Times.Once, "IBarbequeService.Create should be called once.");
        }

        [Fact]
        public async Task GetBarbequeFailure()
        {
            // TODO: refactor

            // Arrange
            var barbequeId = "1";

            serviceMock.Setup(s => s.Get(barbequeId)).Throws(new Exception("random exception"));

            // Act
            Assert.ThrowsAsync<Exception>(() => controller.Get(barbequeId));

            serviceMock.Verify(s => s.Get(barbequeId), Times.Once, "IBarbequeService.Get should be called once.");
        }
    }
}
