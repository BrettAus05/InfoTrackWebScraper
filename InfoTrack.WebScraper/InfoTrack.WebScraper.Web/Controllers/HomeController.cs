using InfoTrack.WebScraper.Core.Services;
using InfoTrack.WebScraper.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace InfoTrack.WebScraper.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISearchEngine _searchEngine;

        public HomeController(ILogger<HomeController> logger, ISearchEngine searchEngine)
        {
            _logger = logger;
            _searchEngine = searchEngine;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Submit(SubmitModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ResultModel
                {
                    Success = false,
                    Errors = ModelState.Values.SelectMany(m => m.Errors).Select(m => m.ErrorMessage)
                }); ;
            }

            if (!_searchEngine.IsValidSearchEngine(model.Url))
            {
                var urls = new List<string>
                {
                    $"Available search engines -"
                };

                urls.AddRange(_searchEngine.GetSearchEngines().Select(s => s.Url));

                return Json(new ResultModel
                {
                    Success = false,
                    Errors = urls,
                });
            }

            var hits = await _searchEngine.FindHits(model.Url, model.SearchTerms.Split(' '));

            var result = new ResultModel
            {
                Hits = string.Join(", ", hits).Trim(),
                Success = true
            };

            return Json(result);
        }
    }
}
