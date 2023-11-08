namespace Asumet.Doc.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class ExportController : ApiControllerBase
    {
/*        
        public ExportController(ILogger<ExportController> logger, IExportDocService exportDocService)
        {
            Logger = logger;
            ExportDocService = exportDocService;
        }

        private ILogger<ExportController> Logger { get; }

        public IExportDocService ExportDocService { get; }

        [HttpGet("psa")]
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
*/
    }
}
