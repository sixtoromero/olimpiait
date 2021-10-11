using MatBlazor;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using olimpiait.multiplo3.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tewr.Blazor.FileReader;

namespace olimpiait.multiplo3
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress), Timeout = TimeSpan.FromSeconds(15) });
            
            // Set up logging
            builder.Logging.SetMinimumLevel(LogLevel.Warning);

            ConfigureServices(builder.Services);

            await builder.Build().RunAsync();
        }

        private static void ConfigureServices(IServiceCollection Services)
        {            
            Services.AddOptions();
            Services.AddMatBlazor();
            Services.AddFileReaderService();

            Services.AddSingleton<IHostingEnvironment>(new HostingEnvironment());
            Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            Services.AddScoped<IConexionRest, ConexionREST>();
            Services.AddScoped<ISettings, Settings>();
            Services.AddSingleton<IMostrarMensajes, MostrarMensajes>();

            ConfigureViewModels(Services);
            ConfigureModels(Services);
        }

        private static void ConfigureViewModels(IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a
                .FullName.StartsWith("olimpiait.multiplo3"))
                .First();
            var classes = assembly.ExportedTypes.Where(a => a
                 .FullName.EndsWith("ViewModel"));
            foreach (Type t in classes)
            {
                foreach (Type i in t.GetInterfaces())
                {
                    services.AddTransient(i, t);
                }
            }
        }

        private static void ConfigureModels(IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => a
            .FullName.StartsWith("olimpiait.multiplo3"))
            .First();
            var classes = assembly.ExportedTypes.Where(a => a
                 .FullName.EndsWith("Model"));
            foreach (Type t in classes)
            {
                foreach (Type i in t.GetInterfaces())
                {
                    services.AddTransient(i, t);
                }
            }

        }
    }
}
