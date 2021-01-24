using System.Collections.Generic;

namespace Audioburst.QuoteQuiz.Common.Data.Models
{
    public class QuestionModel
    {
        public string Question { get; set; }

        public IList<AnswerModel> Answers { get; set; }
    }
}
