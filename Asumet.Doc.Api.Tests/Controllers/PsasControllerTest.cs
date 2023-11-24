using Asumet.Doc.Api.Controllers;
using Asumet.Doc.Api.Tests.Fixtures;
using Asumet.Doc.Dtos;
using Asumet.Doc.Services.Data;
using Asumet.Doc.Services.Office;
using Asumet.Doc.Services.Psas;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Asumet.Doc.Api.Tests.Controllers
{
    public class PsasControllerTest
    {
        private PsasController CreatePsasController(
            Mock<ILogger<PsasController>>? logger = null,
            Mock<IPsaService>? psaService = null,
            Mock<IPsaMatchService>? matchService = null,
            Mock<IExportDocService>? exportDocService = null)
        {
            logger ??= new Mock<ILogger<PsasController>>();
            psaService ??= new Mock<IPsaService>();
            matchService ??= new Mock<IPsaMatchService>();
            exportDocService ??= new Mock<IExportDocService>();
            
            var result = new PsasController(logger.Object, psaService.Object, matchService.Object, exportDocService.Object);
            return result;
        }
        
        [Fact]
        public async void Get_OnSuccess_ReturnsProperPsa()
        {
            // Arrange
            const int psaId = 2;
            var psaDto = PsaDtoFixture.GetById(psaId);
            var psaService = new Mock<IPsaService>();
            psaService
                .Setup(ps => ps.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(psaDto);
            var controller = CreatePsasController(psaService: psaService);

            // Act
            var actionResult = await controller.Get(psaId);

            // Assert
            var result = actionResult.Result as OkObjectResult;
            Assert.NotNull(result);
            var value = result.Value as PsaDto;
            Assert.NotNull(value);
            value.Id.Should().Be(psaId);
            Assert.NotNull(psaDto);
            value.ActNumber.Should().Be(psaDto.ActNumber);
            psaService.Verify(ps => ps.GetByIdAsync(psaId), Times.Once());
        }

        [Fact]
        public async void Get_WhenPsaNotFound_ReturnsNull()
        {
            // Arrange
            const int psaId = int.MaxValue;
            var psaService = new Mock<IPsaService>();
            psaService
                .Setup(ps => ps.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(null as PsaDto);
            var controller = CreatePsasController(psaService: psaService);

            // Act
            var actionResult = await controller.Get(psaId);

            // Assert
            actionResult.Result.Should().BeOfType<NotFoundObjectResult>();
            actionResult.Value.Should().BeNull();
            psaService.Verify(ps => ps.GetByIdAsync(psaId), Times.Once());
        }
    }
}