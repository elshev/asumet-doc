namespace Asumet.Doc.Api.Controllers
{
    using Asumet.Doc.Dtos;
    using Asumet.Doc.Services;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class PsasController : ApiControllerBase
    {
        public PsasController(ILogger<PsasController> logger, IPsaService psaService)
        {
            Logger = logger;
            PsaService = psaService;
        }

        private ILogger<PsasController> Logger { get; }

        public IPsaService PsaService { get; }

        [HttpGet]
        public async Task<PsaDto?> Get(int id)
        {
            var result = await PsaService.GetByIdAsync(id);
            return result;
        }
    }
}
