namespace Asumet.Doc.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class PsasController : ControllerBase
    {
/*        public PsasController(ILogger<PsasController> logger, DocDb dbContext)
        {
            _logger = logger;
            DbContext = dbContext;
        }

        private readonly ILogger<PsasController> _logger;

        public DocDb DbContext { get; }

        [HttpGet(Name = "GetPsa")]
        public IEnumerable<Psa> Get()
        {
            var result = DbContext.Psas
                .Include(p => p.Buyer)
                .Include(p => p.Supplier)
                .Include(p => p.PsaScraps)
                .ToList();
            return result;
        }
*/    }
}