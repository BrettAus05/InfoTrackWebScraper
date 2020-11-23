using System.Collections.Generic;

namespace InfoTrack.WebScraper.Web.Models
{
    public class ResultModel
    {
        public string Hits { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
