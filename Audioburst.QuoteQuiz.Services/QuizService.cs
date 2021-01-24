using Audioburst.QuoteQuiz.Common.Data.Models;
using Audioburst.QuoteQuiz.Data.Models;
using Audioburst.QuoteQuiz.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Audioburst.QuoteQuiz.Services
{
    public class QuizService : IQuizService
    {
        private readonly IGenericRepository<Quote> genericQuoteRepository;
        private readonly IConfiguration configuration;

        public QuizService(IGenericRepository<Quote> genericQuoteRepository, IConfiguration configuration)
        {
            this.genericQuoteRepository = genericQuoteRepository;
            this.configuration = configuration;
        }

        public async Task<QuizModel> GenerateQuiz(int questionsNumber)
        {
            return await Task.Run(async () => {
                var quizTheme = await GetQuizTheme();
                var quiz = new QuizModel();
                quiz.QuizTheme = quizTheme;
                var quotes = genericQuoteRepository.GetAll().Include(q => q.Author).Include(q => q.Category).ToList();

                for (int i = 1; i <= questionsNumber; i++)
                {
                    QuestionModel question = GenerateQuestion(quotes, i);
                    quiz.Questions.Add(question);
                }

                return quiz;
            });
        }

        private QuestionModel GenerateQuestion(List<Quote> quotes, int index)
        {
            var random = new Random();
            var randQuote = quotes[random.Next(quotes.Count)];
            var wrongQuotes = quotes.Where(q => q.AuthorId != randQuote.AuthorId).ToList();

            var answers = new List<AnswerModel>() {
                        new AnswerModel() { Answer = randQuote.Text, IsCorrect = true },
                        new AnswerModel() { Answer = wrongQuotes[random.Next(wrongQuotes.Count)].Text, IsCorrect = false },
                        new AnswerModel() { Answer = wrongQuotes[random.Next(wrongQuotes.Count)].Text, IsCorrect = false }
                    };

            var question = new QuestionModel()
            {
                Question = $"{index}. What has {randQuote.Author.Name} tell for {randQuote.Category.Title}?",
                Answers = answers
            };
            return question;
        }

        private async Task<string> GetQuizTheme()
        {
            var quizTheme = "DarkTheme";

            using (var client = new HttpClient())
            {
                var endPoint = configuration.GetSection("QuizThemeAPI:EndPoint").Value;

                if (!string.IsNullOrWhiteSpace(endPoint))
                {
                    var response = await client.GetAsync(endPoint);

                    if (response.IsSuccessStatusCode)
                    {
                        var data = await JsonSerializer.DeserializeAsync<WeatherModel>(await response.Content.ReadAsStreamAsync());

                        if (data?.Weather?.First().Main == "Clear")
                        {
                            quizTheme = "LightTheme";
                        }
                    }
                }
            }

            return quizTheme;
        }
    }
}
