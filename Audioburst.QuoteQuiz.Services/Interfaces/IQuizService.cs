using Audioburst.QuoteQuiz.Common.Data.Models;
using System.Threading.Tasks;

namespace Audioburst.QuoteQuiz.Services
{
    public interface IQuizService
    {
        Task<QuizModel> GenerateQuiz(int questionsNumber);
    }
}
