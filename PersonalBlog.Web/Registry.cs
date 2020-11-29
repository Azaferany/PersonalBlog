using Castle.Core.Internal;
using DNT.Deskly.EFCore;
using DNT.Deskly.Localization;
using DNT.Deskly.Web.Caching;
using DNT.Deskly.Web.WebToolkit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using PersonalBlog.Application.Contracts;
using PersonalBlog.Application.Identity;
using PersonalBlog.Domain.Configuration;
using PersonalBlog.Infrastructure.Context;
using PersonalBlog.Web.Services;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace PersonalBlog.Web
{
    public static class Registry
    {
        public static IServiceCollection AddDynamicPermissions(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationHandler, DynamicPermissionsAuthorizationHandler>();
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy(
                    name: ConstantPolicies.DynamicPermission,
                    configurePolicy: policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.Requirements.Add(new DynamicPermissionRequirement());
                    });
            });

            return services;
        }
        public static void AddWebApp(this IServiceCollection services, SiteSettings siteSettings)
        {


            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();




            services.AddAntiforgery(x => x.HeaderName = "X-XSRF-TOKEN");

            services.AddLocalization(setup => setup.ResourcesPath = "Resources");

            services.AddControllersWithViews()
                .AddDataAnnotationsLocalization(o =>
                {
                    o.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        var localizationResource = type.GetTypeAttribute<LocalizationResourceAttribute>();
                        return localizationResource == null
                            ? factory.Create(type)
                            : factory.Create(localizationResource.Name, localizationResource.Location);
                    };
                });
            services.AddRazorPages();

            services.AddDataProtection()
                .PersistKeysToDbContext<ProjectDbContext>()
                .SetDefaultKeyLifetime(siteSettings.DataProtectionOptions.DataProtectionKeyLifetime)
                .SetApplicationName(siteSettings.DataProtectionOptions.ApplicationName);
            //TODO: 
            //.ProtectKeysWithCertificate(loadCertificateFromFile(siteSettings));

            if (siteSettings.CookieOptions.UseDistributedCacheTicketStore)
            {
                if (siteSettings.CookieOptions.UseDistributedMemoryCache)
                    services.AddCustomDistributedMemoryCache();
                else
                {
                    var cacheOptions = siteSettings.CookieOptions.DistributedSqlServerCacheOptions;
                    var connectionString = cacheOptions.ConnectionString ?? siteSettings.ConnectionStrings.DefaultConnectionstring;
                    services.AddCustomDistributSqlServerCache(options =>
                    {
                        options.ConnectionString = connectionString;
                        options.SchemaName = cacheOptions.SchemaName;
                        options.TableName = cacheOptions.TableName;
                    });
                }
            }
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .SetIsOriginAllowed(host => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddAntiforgery(x => x.HeaderName = "X-XSRF-TOKEN");
        }
        private static X509Certificate2 loadCertificateFromFile(SiteSettings siteSettings)
        {
            // NOTE:
            // You should check out the identity of your application pool and make sure
            // that the `Load user profile` option is turned on, otherwise the crypto susbsystem won't work.

            var certificate = siteSettings.DataProtectionX509Certificate;
            var fileName = Path.Combine(ServerInfo.GetAppDataFolderPath(), certificate.FileName);

            // For decryption the certificate must be in the certificate store. It's a limitation of how EncryptedXml works.
            using (var store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                store.Open(OpenFlags.ReadWrite);
                store.Add(new X509Certificate2(fileName, certificate.Password, X509KeyStorageFlags.Exportable));
            }

            var cert = new X509Certificate2(
                fileName,
                certificate.Password,
                keyStorageFlags: X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet
                            | X509KeyStorageFlags.Exportable);
            // TODO: If you are getting `Keyset does not exist`, run `wwwroot\App_Data\make-cert.cmd` again.
            Console.WriteLine($"cert private key: {cert.PrivateKey}");
            return cert;
        }

    }
}