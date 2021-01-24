using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Audioburst.QuoteQuiz.Common.Data.Models
{
    public class WeatherModel
    {
        [JsonPropertyName("weather")]
        public IList<Weather> Weather { get; set; }
    }

    public class Weather
    {
        [JsonPropertyName("main")]
        public string Main { get; set; }
    }
}
