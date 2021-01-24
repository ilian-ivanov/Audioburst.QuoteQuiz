using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Audioburst.QuoteQuiz.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal readonly QuoteQuizContext context = null;
        private readonly DbSet<T> table = null;

        public GenericRepository(QuoteQuizContext context)
        {
            this.context = context;
            table = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return table.AsQueryable();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
