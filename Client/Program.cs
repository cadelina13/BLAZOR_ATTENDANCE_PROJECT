using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MudBlazor.Services;
using MudBlazor;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authorization;
using Client.Data;
using Refit;

namespace Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("httpClient", sp =>
            {
                new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
            });
            builder.Services.AddOidcAuthentication(options =>
            {
                // Configure your authentication provider options here.
                // For more information, see https://aka.ms/blazor-standalone-auth
                /*builder.Configuration.Bind("google_dev", options.ProviderOptions);
                options.ProviderOptions.DefaultScopes.Add("email");
                options.ProviderOptions.DefaultScopes.Add("profile");*/
            });
            builder.Services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomCenter;

                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 5000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddSingleton<TableHelper>();
            builder.Services.AddTransient<DataAccess>();
#if DEBUG
            builder.Services.AddRefitClient<IDataAccess>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://localhost:44328/attendance");
            });
#else
            builder.Services.AddRefitClient<IDataAccess>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://intranet.misamisoriental.gov.ph/efms_api/attendance");
                /*c.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
                c.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
                c.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");*/
            });
            builder.Logging.SetMinimumLevel(LogLevel.None);
#endif

            await builder.Build().RunAsync();
        }
    }
}
