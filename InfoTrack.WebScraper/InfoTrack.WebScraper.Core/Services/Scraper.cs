using InfoTrack.WebScraper.Dtos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace InfoTrack.WebScraper.Core.Services
{
    /// <summary>
    /// This class would need rework to query actual website instances
    /// </summary>
    public class Scraper : IScraper
    {

        public string Url { get; }
        public Dictionary<int, string> ResultLinksFound { get; } = new Dictionary<int, string>();
        private static readonly HttpClient _client = new HttpClient();
        private readonly string _pageNamingConvention;
        private readonly string _tagContainingSearchResult;
        private readonly int _pagesAvailable;
        private readonly int _maxResultsToProcess;
        private int _searchResultsFound;
        private const string _startOfLink = "<a href=\"";
        private const char _endOfLink = '"';

        public Scraper(SearchEngineDto searchEngine, int maxResultsToProcess)
        {
            Url = searchEngine.Url;
            _pageNamingConvention = searchEngine.PageNamingConvention;
            _pagesAvailable = searchEngine.PagesAvailable;
            _tagContainingSearchResult = searchEngine.TagContainingSearchResult;
            _maxResultsToProcess = maxResultsToProcess;
        }

        /// <summary>
        /// Find search result links from the source website
        /// </summary>
        /// <param name="searchTerms"></param>
        /// <returns></returns>
        /// <remarks>Search terms passed thru to allow quering of actual website</remarks>
        public async Task FindResultLinks(IEnumerable<string> searchTerms)
        {
            for (int pageIndex = 1; pageIndex <= _pagesAvailable; pageIndex++)
            {
                var pageName = GetPageName(pageIndex);

                var page = await GetPage(pageName);

                FindResultLinks(page);

                if (StopProcessingLinks)
                {
                    break;
                }
            }
        }

        private void FindResultLinks(string page)
        {
            var indexPosition = page.IndexOf(_tagContainingSearchResult);

            if (indexPosition < 0)
            {
                return;
            }

            _searchResultsFound++;

            var pageTextRemaining = page.Substring(indexPosition + _tagContainingSearchResult.Length);

            var startOfLinkIndex = pageTextRemaining.IndexOf(_startOfLink);

            pageTextRemaining = pageTextRemaining.Substring(startOfLinkIndex + _startOfLink.Length);

            var endOfLinkIndex = pageTextRemaining.IndexOf(_endOfLink);

            var searchResultLink = pageTextRemaining.Substring(0, endOfLinkIndex);

            ResultLinksFound.Add(_searchResultsFound, searchResultLink);

            if (StopProcessingLinks)
            {
                return;
            }

            FindResultLinks(pageTextRemaining);
        }

        private string GetPageName(int pageNumber)
        {
            return _pageNamingConvention.Replace("XX", pageNumber.ToString("00"));
        }

        private bool StopProcessingLinks => _searchResultsFound >= _maxResultsToProcess;

        private async Task<string> GetPage(string page)
        {
            var uriBuilder = new UriBuilder(Url);

            // += required as path is already partially set (Google / Bing)
            // eg http://infotrack-tests.infotrack.com.au/Google/ + Page01.html

            uriBuilder.Path += page;

            var pageContent = await _client.GetStringAsync(uriBuilder.Uri);

            return pageContent;
        }
    }
}
