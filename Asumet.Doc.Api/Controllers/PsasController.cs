using Asumet.Doc.Repo;
using Asumet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asumet.Doc.Api.Controllers
{
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
            var result = DbContext.Psas.Include(p => p.Buyer).ToList();
            return result;
        }
    }
}