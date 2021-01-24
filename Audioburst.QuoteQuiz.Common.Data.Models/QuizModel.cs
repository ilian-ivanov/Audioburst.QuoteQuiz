using System.Collections.Generic;

namespace Audioburst.QuoteQuiz.Common.Data.Models
{
    public class QuizModel
    {
        public QuizModel()
        {
            this.Questions = new List<QuestionModel>();
        }

        public string QuizTheme { get; set; }

        public IList<QuestionModel> Questions { get; set; }
    }
}
