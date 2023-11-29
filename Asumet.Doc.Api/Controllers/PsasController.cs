namespace Asumet.Doc.Api.Controllers
{
    using Asumet.Common;
    using Asumet.Doc.Dtos;
    using Asumet.Doc.Services.Data;
    using Asumet.Doc.Services.Office;
    using Asumet.Doc.Services.Psas;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class PsasController : ApiControllerBase
    {
        public PsasController(
            ILogger<PsasController> logger,
            IPsaService psaService,
            IPsaMatchService psaMatchService,
            IExportDocService exportDocService)
        {
            Logger = logger;
            PsaService = psaService;
            PsaMatchService = psaMatchService;
            ExportDocService = exportDocService;
        }

        private ILogger<PsasController> Logger { get; }

        public IPsaService PsaService { get; }
        public IPsaMatchService PsaMatchService { get; }
        public IExportDocService ExportDocService { get; }

        [HttpGet]
        public async Task<ActionResult<PsaDto>> Get(int id)
        {
            var result = await PsaService.GetByIdAsync(id);
            if (result == null)
            {
                return new NotFoundObjectResult(null);
            }

            return new OkObjectResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<PsaDto?>> Post([FromBody] PsaDto psaDto)
        {
            var result = await PsaService.InsertEntityAsync(psaDto);
            if (result == null)
            {
                return new BadRequestObjectResult("The error happened when creating PSA");
            }

            return new OkObjectResult(result);
        }
        
        [HttpGet("export")]
        public async Task<IActionResult> ExportPsaToWord(int id)
        {
            string? filePath = await ExportDocService.ExportPsaToWordFileAsync(id);
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return NotFound("Wrong Psa id.");
            }
            
            var result = GetDocxFileResult(filePath);
            return result;
        }

        
        [HttpPost("match")]
        public async Task<IActionResult> Match([FromForm] int psaId, IFormFile imageFile)
        {
            if (imageFile is null || imageFile.Length == 0)
            {
                return BadRequest($"Invalid file: {nameof(imageFile)}");
            }

            var imageFilePath = PathHelper.GetTempFileName();
            try
            {
                using (var stream = new FileStream(imageFilePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                var result = await PsaMatchService.MatchAsync(psaId, imageFilePath);
                return Ok(result);
            }
            finally
            {
                System.IO.File.Delete(imageFilePath);
            }
        }
    }
}
