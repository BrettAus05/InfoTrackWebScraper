using System;
using System.Collections.Generic;
using System.Linq;

namespace InfoTrack.WebScraper.Core.Services
{
    public class HitFinder : IHitFinder
    {
        private readonly Uri _companyUri;

        public HitFinder(string companyName)
        {
            _companyUri = new Uri(companyName);
        }

        /// <summary>
        /// Find hits which match the company name
        /// </summary>
        /// <param name="searchResults"></param>
        /// <returns></returns>
        public IEnumerable<int> FindHits(Dictionary<int, string> searchResults)
        {
            var matchingHitsFound = new List<int>();

            foreach (var searchResult in searchResults)
            {
                var searchResultUri = new Uri(searchResult.Value);

                if (searchResultUri.Scheme == _companyUri.Scheme && searchResultUri.Host == _companyUri.Host)
                {
                    matchingHitsFound.Add(searchResult.Key);
                }
            }

            if (!matchingHitsFound.Any())
            {
                matchingHitsFound.Add(0);
            }

            return matchingHitsFound;
        }
    }
}
