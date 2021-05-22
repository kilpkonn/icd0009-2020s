using Microsoft.AspNetCore.Hosting;
using WebApp.Areas.Identity;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]
namespace WebApp.Areas.Identity
{
    /// <inheritdoc />
    public class IdentityHostingStartup : IHostingStartup
    {
        /// <inheritdoc />
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}