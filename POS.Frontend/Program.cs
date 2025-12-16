using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using POS.Frontend.Auth;
using POS.Frontend.Services;

namespace POS.Frontend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            var urlLocal = "https://localhost:7256/";
            builder.Services.AddMudServices();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(urlLocal) });
            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped<AuthenticationProviderJWT>();
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IProductService, POS.Frontend.Services.ProductService>();
            builder.Services.AddScoped<ISaleService, POS.Frontend.Services.SaleService>();
            builder.Services.AddScoped<IStockService, POS.Frontend.Services.StockService>();
            builder.Services.AddScoped<ICashClosingService, POS.Frontend.Services.CashClosingService>();
            builder.Services.AddScoped<IDashboardService, POS.Frontend.Services.DashboardService>();
            builder.Services.AddScoped<IRequestService,RequestService>();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());
            builder.Services.AddScoped<ILoginService, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());
            await builder.Build().RunAsync();
        }
    }
}
