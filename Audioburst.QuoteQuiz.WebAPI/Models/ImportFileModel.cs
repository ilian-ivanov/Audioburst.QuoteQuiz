using Audioburst.QuoteQuiz.WebAPI.Attributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Audioburst.QuoteQuiz.WebAPI
{
    public class ImportFileModel
    {
        [Required]
        [MaxFileSize(5*1024*1024)]
        [AllowedExtensions(new string[] {".csv"})]
        public IFormFile ImportedFile { get; set; }
    }
}
