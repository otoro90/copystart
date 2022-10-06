using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(CopyStart.Areas.Identity.IdentityHostingStartup))]
namespace CopyStart.Areas.Identity
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