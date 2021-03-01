using BlazorHero.CleanArchitecture.Client.Extensions;
using BlazorHero.CleanArchitecture.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder
                          .CreateDefault(args)
                          .AddRootComponents()
                          .AddClientServices();
            var host = builder.Build();
            var storageService = host.Services.GetRequiredService<BrowserService>();
            if (storageService != null)
            {
                CultureInfo culture;
                var preference = await storageService.GetPreference();
                if (preference.LanguageCode != null || preference.LanguageCode != string.Empty)
                    culture = new CultureInfo(preference.LanguageCode);
                else
                    culture = new CultureInfo("en-US");
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
            }
            await builder.Build()
            .RunAsync();
        }
    }
}