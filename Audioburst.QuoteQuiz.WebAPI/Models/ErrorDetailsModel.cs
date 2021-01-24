﻿using System.Text.Json;

namespace Audioburst.QuoteQuiz.WebAPI.Models
{
    public class ErrorDetailsModel
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
