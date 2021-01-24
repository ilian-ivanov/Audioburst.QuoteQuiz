using Audioburst.QuoteQuiz.Common.Data.Models;
using Audioburst.QuoteQuiz.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Audioburst.QuoteQuiz.WebAPI.Controllers
{
    [ApiController]
    [Route("quiz")]
    public class QuizController : ControllerBase
    {
        private readonly ILogger<QuizController> logger;
        private readonly IQuizService quizService;

        public QuizController(ILogger<QuizController> logger, IQuizService quizService)
        {
            this.logger = logger;
            this.quizService = quizService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<QuizModel>> Get(int questionsNumber = 10)
        {
            this.logger.LogInformation($"A new quiz is requested with {questionsNumber} questions.");
            if (questionsNumber < 1)
            {
                this.logger.LogInformation($"A new quiz was not generated because the questions number was {questionsNumber}!");
                return BadRequest();
            }

            var quiz = await quizService.GenerateQuiz(questionsNumber);
            this.logger.LogInformation($"A new quiz was generated with {quiz.Questions.Count} questions.");

            return Ok(quiz);
        }
    }
}
