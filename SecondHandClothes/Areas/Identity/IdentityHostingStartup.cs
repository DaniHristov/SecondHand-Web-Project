using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(SecondHandClothes.Areas.Identity.IdentityHostingStartup))]
namespace SecondHandClothes.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}