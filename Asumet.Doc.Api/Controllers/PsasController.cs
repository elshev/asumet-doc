namespace Asumet.Doc.Api.Controllers
{
    using Asumet.Doc.Dtos;
    using Asumet.Doc.Services;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class PsasController : ControllerBase
    {
        public PsasController(ILogger<PsasController> logger, IPsaService psaService)
        {
            _logger = logger;
            PsaService = psaService;
        }

        private readonly ILogger<PsasController> _logger;

        public IPsaService PsaService { get; }

        [HttpGet]
        public async Task<PsaDto?> Get(int id)
        {
            var result = await PsaService.GetByIdAsync(id);
            return result;
        }
    }
}
