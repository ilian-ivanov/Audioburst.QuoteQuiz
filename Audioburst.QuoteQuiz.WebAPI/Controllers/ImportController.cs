using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Audioburst.QuoteQuiz.Services;
using Microsoft.AspNetCore.Http;

namespace Audioburst.QuoteQuiz.WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly ILogger<QuizController> logger;
        private readonly IImportService importService;

        public ImportController(ILogger<QuizController> logger, IImportService importService)
        {
            this.logger = logger;
            this.importService = importService;
        }

        [HttpPost]
        [Route("import")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Import([FromForm]ImportFileModel file)
        {
            this.logger.LogInformation($"A new file is imported for processing - {file.ImportedFile.FileName}.");

            var errorsInTheFile = await importService.Import(file.ImportedFile.OpenReadStream());

            if(errorsInTheFile.Count > 0)
            {
                foreach (var error in errorsInTheFile)
                {
                    ModelState.AddModelError("errors", error);
                }

                this.logger.LogInformation($"The file failed - {file.ImportedFile.FileName}.");
                return BadRequest(ModelState);
            }

            this.logger.LogInformation($"The file was processed successfully - {file.ImportedFile.FileName}.");
            return Ok();
        }
    }
}
