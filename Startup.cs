using Boomerang.Data;
using Boomerang.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet.Security.OAuth.Discord;
using AspNet.Security.OAuth.GitHub;
using Microsoft.AspNetCore.Authentication.Google;

namespace Boomerang
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<BoomerangDbContext>();
            services.AddControllersWithViews();

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    var auth = Configuration.GetSection("Auth:Google");
                    options.ClientId = auth.GetValue<string>("ClientId");
                    options.ClientSecret = auth.GetValue<string>("ClientSecret");
                })
                .AddGitHub(options =>
                {
                    var auth = Configuration.GetSection("Auth:Github");
                    options.ClientId = auth.GetValue<string>("ClientId");
                    options.ClientSecret = auth.GetValue<string>("ClientSecret");
                })
                .AddDiscord(options =>
                {
                    var auth = Configuration.GetSection("Auth:Discord");
                    options.ClientId = auth.GetValue<string>("ClientId");
                    options.ClientSecret = auth.GetValue<string>("ClientSecret");
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
