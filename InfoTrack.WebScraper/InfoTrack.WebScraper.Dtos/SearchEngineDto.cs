namespace InfoTrack.WebScraper.Dtos
{
    public class SearchEngineDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string PageNamingConvention { get; set; }
        public int PagesAvailable{ get; set; }
        public string TagContainingSearchResult { get; set; }
    }
}