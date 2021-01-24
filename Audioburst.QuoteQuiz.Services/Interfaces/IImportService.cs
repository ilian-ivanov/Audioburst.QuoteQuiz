using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Audioburst.QuoteQuiz.Services
{
    public interface IImportService
    {
        Task<IList<string>> Import(Stream stream);
    }
}
