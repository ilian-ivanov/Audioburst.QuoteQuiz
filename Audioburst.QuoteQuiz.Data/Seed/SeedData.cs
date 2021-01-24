using Audioburst.QuoteQuiz.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Audioburst.QuoteQuiz.Data
{
    public static class SeedData
    {
        public static void Seed(QuoteQuizContext context)
        {
            try
            {
                var directory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\Audioburst.QuoteQuiz.Data\Seed\SampleData.txt"));
                using (var file = new StreamReader(directory))
                {
                    var authors = new List<Author>();
                    var categories = new List<Category>();
                    string line = string.Empty;

                    while ((line = file.ReadLine()) != null)
                    {
                        line = line.Trim('(').Trim(')').Replace("\'\'", string.Empty);
                        var quoteIndexStart = line.IndexOf('\'');
                        var quoteIndexStop = line.IndexOf('\'', quoteIndexStart + 1);
                        var name = line.Substring(quoteIndexStart + 1, quoteIndexStop - quoteIndexStart - 1);
                        line = line.Substring(quoteIndexStop + 1);

                        quoteIndexStart = line.IndexOf('\'');
                        quoteIndexStop = line.IndexOf('\'', quoteIndexStart + 1);
                        var categoryTitle = line.Substring(quoteIndexStart + 1, quoteIndexStop - quoteIndexStart - 1);
                        line = line.Substring(quoteIndexStop + 1);

                        quoteIndexStart = line.IndexOf('\'');
                        quoteIndexStop = line.IndexOf('\'', quoteIndexStart + 1);
                        var quoteText = line.Substring(quoteIndexStart + 1, quoteIndexStop - quoteIndexStart - 1);
                        line = line.Substring(quoteIndexStop + 1);

                        var author = authors.FirstOrDefault(a => a.Name == name);
                        var category = categories.FirstOrDefault(c => c.Title == categoryTitle);

                        var newQuote = new Quote()
                        {
                            Text = quoteText,
                            Author = author == null ? new Author() { Name = name } : author,
                            Category = category == null ? new Category() { Title = categoryTitle } : category
                        };

                        context.Quotes.Add(newQuote);
                        if (author == null)
                        {
                            authors.Add(newQuote.Author);
                        }

                        if (category == null)
                        {
                            categories.Add(newQuote.Category);
                        }
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception ex) { }
        }
    }
}
