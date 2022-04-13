using System;
using Boomerang.Data;
using Boomerang.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Boomerang.Areas.Identity.IdentityHostingStartup))]
namespace Boomerang.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<BoomerangDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("BoomerangContextConnection")));

            });
        }
    }
}