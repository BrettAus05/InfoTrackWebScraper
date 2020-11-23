using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfoTrack.WebScraper.Core.Services
{
    public interface IScraper
    {
        string Url { get; }

        Dictionary<int, string> ResultLinksFound { get; }

        Task FindResultLinks(IEnumerable<string> searchTerms);
    }
}