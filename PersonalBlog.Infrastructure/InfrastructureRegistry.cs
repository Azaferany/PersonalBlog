using System;
using DNT.Deskly.EFCore;
using DNT.Deskly.EFCore.Context.Hooks;
using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PersonalBlog.Domain.Configuration;
using PersonalBlog.Infrastructure.Context;
using PersonalBlog.Infrastructure.Hooks;

namespace DNTFrameworkCore.TestAPI.Infrastructure
{
    public static class InfrastructureRegistry
    {
        public static void AddInfrastructure(this IServiceCollection services, SiteSettings settings)
        {
            services.AddEFCore<ProjectDbContext>()
                .WithTrackingHook<int>()
                .WithDeletedEntityHook()
                .WithRowLevelSecurityHook<int>()
                .WithRowIntegrityHook();

            services.AddDbContext<ProjectDbContext>((serviceProvider, optionsBuilder) =>
            {
                //optionsBuilder.EnableSensitiveDataLogging();
                optionsBuilder.UseSqlServer(settings.ConnectionStrings.DefaultConnectionstring,
                                sqlServerOptionsBuilder =>
                                {
                                    sqlServerOptionsBuilder
                                        .CommandTimeout((int)TimeSpan.FromMinutes(3).TotalSeconds)
                                        //.EnableRetryOnFailure()
                                        .MigrationsAssembly(typeof(InfrastructureRegistry).Assembly.FullName);
                                });
                //.AddInterceptors(serviceProvider.GetRequiredService<SecondLevelCacheInterceptor>()
            });
                //.AddInterceptors(serviceProvider.GetRequiredService<SecondLevelCacheInterceptor>()
                //.ConfigureWarnings(warnings =>
                //{
                //    //...
                //})


            services.AddTransient<IHook, DateInsertHooks>();
            services.AddTransient<IHook, DateUpdateHooks>();

            // TODO: add more conf
            services.AddEFSecondLevelCache(options =>
                options.UseMemoryCacheProvider(CacheExpirationMode.Absolute, TimeSpan.FromMinutes(10))
                    .DisableLogging(false));
        }
    }
}