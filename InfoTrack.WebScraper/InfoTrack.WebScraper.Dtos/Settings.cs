using System.Collections.Generic;

namespace InfoTrack.WebScraper.Dtos
{
    public class Settings
    {
        public IEnumerable<SearchEngineDto> SearchEngines { get; set; }
        public string CompanyName { get; set; }
        public int MaxResultsToSearch { get; set; }
    }
}
