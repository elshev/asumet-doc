namespace Asumet.Doc.Api.Controllers
{
    using Asumet.Doc.Services;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class MatchController : ApiControllerBase
    {
        public MatchController(ILogger<ExportController> logger, IMatchService matchService)
        {
            Logger = logger;
            MatchService = matchService;
        }

        private ILogger<ExportController> Logger { get; }

        public IMatchService MatchService { get; }

        [HttpPost("psa")]
        public async Task<IActionResult> MatchPsa([FromForm] int psaId, IFormFile imageFile)
        {
            if (imageFile is null || imageFile.Length == 0)
            {
                return BadRequest($"Invalid file: {nameof(imageFile)}");
            }

            var imageFilePath = Path.GetTempFileName();

            using var stream = new FileStream(imageFilePath, FileMode.Create);
            await imageFile.CopyToAsync(stream);

            var result = await MatchService.MatchPsaAsync(psaId, imageFilePath);
            return Ok(result);
        }
    }
}
