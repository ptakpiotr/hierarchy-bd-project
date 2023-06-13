using DataAccess.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;

namespace BDBlazorApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddSingleton<IDataGenerator, DataGenerator>();
            services.AddScoped<IFamilyService, FamilyService>();
            services.AddScoped<ICompanyService, CompanyService>();

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            CultureInfo[] supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("pl-PL"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pl-PL"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints((e) =>
            {
                e.MapBlazorHub();
                e.MapFallbackToPage("/_Host");
            });
        }
    }
}
