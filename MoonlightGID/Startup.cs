using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoonlightGID.Models;

namespace MoonlightGID
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
            //dont forget to change this if using another system.
            //var conn = "Server=L110022;Database=MoonLight;Trusted_connection=true";
            //var conn = "Data Source=RHBL002\\SQLEXPRESS01;Initial Catalog=MoonLight;Integrated Security=True";

            var conn = "Data Source=getitdone2020.database.windows.net;Initial Catalog=MoonLight;User ID=git;Password=College2020;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //var conn = "Data Source=LAPTOP-N0HTAK58\\SQLEXPRESS;Initial Catalog=MoonLight;Integrated Security=True";
            services.AddMemoryCache();
            services.AddSession(so =>
            {
                so.IdleTimeout = TimeSpan.FromMinutes(60);
            
            });
            services.AddDbContext<MoonLightContext>(options => options.UseSqlServer(conn));
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
