namespace Asumet.Doc.Api.Controllers
{
    using Asumet.Doc.Repo;
    using Asumet.Entities;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class PsasController : ControllerBase
    {
        public PsasController(ILogger<PsasController> logger, IPsaRepository psaRepository)
        {
            _logger = logger;
            PsaRepository = psaRepository;
        }

        private readonly ILogger<PsasController> _logger;

        public IPsaRepository PsaRepository { get; }

        [HttpGet(Name = "GetPsa")]
        public async Task<Psa?> Get(int id)
        {
            var result = await PsaRepository.GetByIdAsync(id);
            return result;
        }
    }
}