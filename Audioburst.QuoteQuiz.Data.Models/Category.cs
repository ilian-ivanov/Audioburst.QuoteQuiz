using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Audioburst.QuoteQuiz.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public virtual ICollection<Quote> Quotes { get; set; }
    }
}
