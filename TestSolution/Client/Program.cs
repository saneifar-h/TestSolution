using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Blazor.FileReader;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace TestSolution.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient("TestSolution.ServerAPI",
                client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
                //.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp =>
                sp.GetRequiredService<IHttpClientFactory>().CreateClient("TestSolution.ServerAPI"));
            builder.Services.AddFileReaderService(options => options.UseWasmSharedBuffer = true);
            
        
            builder.Services.AddApiAuthorization();

            await builder.Build().RunAsync();
        }
    }
}