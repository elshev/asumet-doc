namespace Asumet.Doc.Api.Controllers
{
    using Asumet.Doc.Repo;
    using Asumet.Entities;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("[controller]")]
    public class PsasController : ControllerBase
    {
        public PsasController(ILogger<PsasController> logger, DocDbContext dbContext)
        {
            _logger = logger;
            DbContext = dbContext;
        }

        private readonly ILogger<PsasController> _logger;

        public DocDbContext DbContext { get; }

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
    }
}