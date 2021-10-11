using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace olimpiait.multiplo3.General
{
    public class Settings : ISettings
    {
        #region atributos
        private readonly HttpClient httpClient;
        public AppSetting AppSetting { get; set; }
        private readonly string JsonAmbiente;
        private readonly IJSRuntime js;
        #endregion

        #region constructor
        public Settings(HttpClient httpClient, IWebAssemblyHostEnvironment HostEnvironment)
        {
            this.httpClient = httpClient;
            JsonAmbiente = HostEnvironment.Environment.ToLower() == "Development" || string.IsNullOrWhiteSpace(HostEnvironment.Environment) ? "/appsettings.json" : $"/appsettings.{HostEnvironment.Environment.ToLower()}.json";
        }
        #endregion
        public async Task<string> GetApiUrl()
        {
            AppSetting = await httpClient.GetFromJsonAsync<AppSetting>(JsonAmbiente)
                   .ConfigureAwait(false);
            return AppSetting.ApiUrl;
        }

    }

    public class AppSetting
    {
        public string ApiUrl { get; set; }
        public string UrlSite { get; set; }        
    }
}
