using InfoTrack.WebScraper.Core.Services;
using InfoTrack.WebScraper.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfoTrack.WebScraper.Core
{
    public static class CoreServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<ISearchEngine, SearchEngine>();

            var settings = new Settings();

            configuration.GetSection("Settings").Bind(settings);

            services.AddSingleton(settings);

            foreach (var searchEngine in settings.SearchEngines)
            {
                services.AddScoped<IScraper>(s => new Scraper(searchEngine, settings.MaxResultsToSearch));
            }

            services.AddSingleton<IHitFinder>(h => new HitFinder(settings.CompanyName));

            return services;
        }
    }
}
