using Asumet.Doc.Api.Controllers;
using Asumet.Doc.Api.Tests.Fixtures;
using Asumet.Doc.Services.Data;
using Asumet.Doc.Services.Office;
using Asumet.Doc.Services.Psas;
using FluentAssertions;
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
            var psaService = new Mock<IPsaService>();
            psaService.Setup(ps => ps.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(PsaDtoFixture.GetById(psaId));
            var controller = CreatePsasController(psaService: psaService);

            // Act
            var result = await controller.Get(psaId);

            // Assert
            Assert.NotNull(result);
            result.Id.Should().Be(psaId);
        }
    }
}