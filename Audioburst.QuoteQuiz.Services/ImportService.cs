using Audioburst.QuoteQuiz.Common.Data.Models;
using Audioburst.QuoteQuiz.Data.Models;
using Audioburst.QuoteQuiz.Data.Repositories;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Audioburst.QuoteQuiz.Services
{
    public class ImportService : IImportService
    {
        private readonly IGenericRepository<Quote> genericQuoteRepository;
        private readonly IGenericRepository<Author> genericAuthorRepository;
        private readonly IGenericRepository<Category> genericCategoryRepository;

        public ImportService(
            IGenericRepository<Quote> genericQuoteRepository, 
            IGenericRepository<Author> genericAuthorRepository, 
            IGenericRepository<Category> genericCategoryRepository)
        {
            this.genericQuoteRepository = genericQuoteRepository;
            this.genericAuthorRepository = genericAuthorRepository;
            this.genericCategoryRepository = genericCategoryRepository;
        }

        public async Task<IList<string>> Import(Stream stream)
        {
            return await Task.Run(() =>
            {
                using (TextReader streamReader = new StreamReader(stream))
                {
                    using (var csvreader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                        var rows = csvreader.GetRecords<QuoteModel>().ToList();
                        var errors = CheckForMissingData(rows);

                        if (errors.Count == 0)
                        {
                            var quotes = genericQuoteRepository.GetAll().ToList();
                            var authors = genericAuthorRepository.GetAll().ToList();
                            var categories = genericCategoryRepository.GetAll().ToList();

                            foreach (var row in rows)
                            {
                                var author = authors.FirstOrDefault(a => a.Name == row.Author);
                                var category = categories.FirstOrDefault(c => c.Title == row.Category);
                                var quote = quotes.FirstOrDefault(q => q.Text == row.Quote);
                                
                                var newQuote = new Quote()
                                {
                                    Text = row.Quote,
                                    Author = author == null ? new Author() { Name = row.Author } : author,
                                    Category = category == null ? new Category() { Title = row.Category } : category
                                };

                                genericQuoteRepository.Insert(newQuote);
                                quotes.Add(newQuote);
                                authors.Add(newQuote.Author);
                                categories.Add(newQuote.Category);
                            }

                            genericQuoteRepository.Save();
                        }

                        return errors;
                    }
                }
            });             
        }

        private IList<string> CheckForMissingData(IList<QuoteModel> rows)
        {
            var errors = new List<string>();
            var index = 2;
            foreach (var row in rows)
            {
                if (string.IsNullOrWhiteSpace(row.Author) || string.IsNullOrWhiteSpace(row.Category) || string.IsNullOrWhiteSpace(row.Quote))
                {
                    errors.Add($"Author, Category or Quote column on row {index} in the file is empty!");
                }

                index++;
            }

            return errors;
        }
    }
}
