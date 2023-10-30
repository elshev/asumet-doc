namespace Asumet.Doc.Api.Controllers
{
    using Asumet.Doc.Dtos;
    using Asumet.Doc.Services;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class ExportDocController : ApiControllerBase
    {
        public ExportDocController(ILogger<ExportDocController> logger, IExportDocService exportDocService)
        {
            Logger = logger;
            ExportDocService = exportDocService;
        }

        private ILogger<ExportDocController> Logger { get; }

        public IExportDocService ExportDocService { get; }

        [HttpGet("export-psa")]
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

        [HttpPost("export-psa")]
        public async Task<IActionResult> ExportPsaToWord([FromBody] PsaDto psaDto)
        {
            if (psaDto == null)
            {
                return BadRequest("Psa parameter is required.");
            }

            string? filePath = await ExportDocService.ExportPsaToWordFileAsync(psaDto);
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return NoContent();
            }
            
            var result = GetDocxFileResult(filePath);
            return result;
        }

    }
}
