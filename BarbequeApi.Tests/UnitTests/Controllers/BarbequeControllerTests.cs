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
            var obj = new BarbequeDto();
            serviceMock.Setup(s => s.Create(obj)).Returns((true, new List<string>()));

            // Act
            var response = await controller.Create(obj);

            // Assert
            var statusResult = Assert.IsType<NoContentResult>(response);
            Assert.Equal((int)HttpStatusCode.NoContent, statusResult.StatusCode);
            serviceMock.Verify(s => s.Create(obj), Times.Once, "IBarbequeService.Create should be called once.");
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
            var statusResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal((int)HttpStatusCode.OK, statusResult.StatusCode);
            serviceMock.Verify(s => s.Get(barbequeId), Times.Once, "IBarbequeService.Get should be called once.");
        }

        [Fact]
        public async Task CreateBarbequeFailure()
        {
            // Arrange
            var obj = new BarbequeDto();
            serviceMock.Setup(s => s.Create(obj)).Throws(new Exception("random exception"));

            // Act
            Assert.ThrowsAsync<Exception>(() => controller.Create(obj));
            
            serviceMock.Verify(s => s.Create(obj), Times.Once, "IBarbequeService.Create should be called once.");
        }

        [Fact]
        public async Task GetBarbequeFailure()
        {
            // Arrange
            var barbequeId = "1";

            serviceMock.Setup(s => s.Get(barbequeId)).Throws(new Exception("random exception"));

            // Act
            Assert.ThrowsAsync<Exception>(() => controller.Get(barbequeId));

            serviceMock.Verify(s => s.Get(barbequeId), Times.Once, "IBarbequeService.Get should be called once.");
        }
    }
}
