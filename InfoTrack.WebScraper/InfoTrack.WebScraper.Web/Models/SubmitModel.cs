using System.ComponentModel.DataAnnotations;

namespace InfoTrack.WebScraper.Web.Models
{
    public class SubmitModel
    {

        [Display(Name = "Search Terms")]
        [Required(ErrorMessage = "Search term(s) are required")]
        public string SearchTerms { get; set; }

        [Required(ErrorMessage = "Url is required")]
        public string Url { get; set; }
    }
}
