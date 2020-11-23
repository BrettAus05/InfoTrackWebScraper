using InfoTrack.WebScraper.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfoTrack.WebScraper.Core.Services
{
    public interface ISearchEngine
    {
        IEnumerable<SearchEngineDto> GetSearchEngines();

        Task<IEnumerable<int>> FindHits(string url, IEnumerable<string> searchTerms);
        
        bool IsValidSearchEngine(string url);
    }
}