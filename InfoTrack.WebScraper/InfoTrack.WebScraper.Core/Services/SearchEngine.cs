using InfoTrack.WebScraper.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoTrack.WebScraper.Core.Services
{
    public class SearchEngine : ISearchEngine
    {
        private readonly IEnumerable<SearchEngineDto> _searchEngines;
        private readonly IEnumerable<IScraper> _scrapers;
        private readonly IHitFinder _hitFinder;

        public SearchEngine(Settings settings, IEnumerable<IScraper> scrapers, IHitFinder hitFinder)
        {
            _searchEngines = settings.SearchEngines;
            _scrapers = scrapers;
            _hitFinder = hitFinder;
        }

        public IEnumerable<SearchEngineDto> GetSearchEngines() => _searchEngines;

        /// <summary>
        /// Checks a given url is a valid option to search
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool IsValidSearchEngine(string url)
        {
            return _searchEngines
                .Select(s => s.Url.ToLower())
                .Contains(url.ToLower());
        }

        /// <summary>
        /// Find the hits which match the company of interest
        /// </summary>
        /// <param name="url"></param>
        /// <param name="searchTerms"></param>
        /// <returns></returns>
        public async Task<IEnumerable<int>> FindHits(string url, IEnumerable<string> searchTerms)
        {
            var scraper = _scrapers.Single(s => s.Url == url);

            await scraper.FindResultLinks(searchTerms);

            return _hitFinder.FindHits(scraper.ResultLinksFound);
        }
    }
}
