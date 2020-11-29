using DNT.Deskly;
using DNT.Deskly.Web;
using DNT.Deskly.Web.Middlewares;
using DNT.Deskly.Web.Mvc;
using DNTFrameworkCore.TestAPI.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersonalBlog.Application;
using PersonalBlog.Domain.Configuration;
using PersonalBlog.Domain.Identity;
using PersonalBlog.Infrastructure.Context;
using PersonalBlog.Resouces;

namespace PersonalBlog.Web
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
            services.Configure<SiteSettings>(options => Configuration.Bind(options));
            services.Configure<ContentSecurityPolicyConfig>(options => Configuration.GetSection("ContentSecurityPolicyConfig").Bind(options));

            services.AddFramework()
                .WithModelValidation()
                .WithMemoryCache();

            services.AddWebFramework(options => Configuration.Bind(options))
            .AddMvcActionsDiscoveryService();


            services.AddIdentityOptions(Configuration.Get<SiteSettings>());


            services.AddDynamicPermissions();

            services.AddAuthentication().AddCookie();
            services.AddInfrastructure(Configuration.Get<SiteSettings>());
            services.AddResources();
            services.AddApplication();
            services.AddWebApp(Configuration.Get<SiteSettings>());





        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseExceptionHandler("/error/index/500");
            app.UseStatusCodePagesWithReExecute("/error/index/{0}");

            app.UseContentSecurityPolicy();

            app.UseStaticFiles();

            //app.UseIdentityServer(); 
            //TODO:

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapControllerRoute(
                    name: "areaRoute",
                    pattern: "{area:exists}/{controller=Account}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
