namespace Asumet.Doc.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class MatchController : ApiControllerBase
    {
/*
        public MatchController(ILogger<ExportController> logger, IPsaMatchService matchService)
        {
            Logger = logger;
            MatchService = matchService;
        }

        private ILogger<ExportController> Logger { get; }

        public IPsaMatchService MatchService { get; }

        [HttpPost("psa")]
        public async Task<IActionResult> MatchPsa([FromForm] int psaId, IFormFile imageFile)
        {
            if (imageFile is null || imageFile.Length == 0)
            {
                return BadRequest($"Invalid file: {nameof(imageFile)}");
            }

            var imageFilePath = PathHelper.GetTempFileName();
            try
            {
                using var stream = new FileStream(imageFilePath, FileMode.Create);
                await imageFile.CopyToAsync(stream);

                var result = await MatchService.MatchAsync(psaId, imageFilePath);
                return Ok(result);
            }
            finally
            {
                System.IO.File.Delete(imageFilePath);
            }
        }
*/
    }
}
