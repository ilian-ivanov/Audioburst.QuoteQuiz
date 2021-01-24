using System.ComponentModel.DataAnnotations;

namespace Audioburst.QuoteQuiz.Data.Models
{
    public class Quote
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
