using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Audioburst.QuoteQuiz.Data.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Quote> Quotes { get; set; }
    }
}
