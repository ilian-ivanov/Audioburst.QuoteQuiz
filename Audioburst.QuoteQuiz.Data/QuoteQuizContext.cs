using Audioburst.QuoteQuiz.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Audioburst.QuoteQuiz.Data
{
    public class QuoteQuizContext: DbContext
    {
        public QuoteQuizContext(DbContextOptions<QuoteQuizContext> options)
            : base(options)
        {
            Database.Migrate();
            if(Authors.Count() == 0)
            {
                SeedData.Seed(this);
            }
        }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Quote> Quotes { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(c => c.Title).IsUnique();
        }
    }
}
