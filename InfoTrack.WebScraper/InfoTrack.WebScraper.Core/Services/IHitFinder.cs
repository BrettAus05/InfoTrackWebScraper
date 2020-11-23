using System.Collections.Generic;

namespace InfoTrack.WebScraper.Core.Services
{
    public interface IHitFinder
    {
        IEnumerable<int> FindHits(Dictionary<int, string> searchResults);
    }
}