using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonalBlog.Application.Identity;
using PersonalBlog.Application.Identity.Contracts;
using PersonalBlog.Domain.Configuration;
using PersonalBlog.Domain.Identity;
using PersonalBlog.Infrastructure.Context;

namespace PersonalBlog.Application
{
    public static class AddIdentityOptionsExtensions
    {
        public const string EmailConfirmationTokenProviderName = "ConfirmEmail";

        public static IServiceCollection AddIdentityOptions(
            this IServiceCollection services, SiteSettings siteSettings)
        {
            if (siteSettings == null) throw new ArgumentNullException(nameof(siteSettings));
            services.addConfirmEmailDataProtectorTokenOptions(siteSettings);


            services.AddIdentityCore<User>(identityOptions =>
            {
                setPasswordOptions(identityOptions.Password, siteSettings);
                setSignInOptions(identityOptions.SignIn, siteSettings);
                setUserOptions(identityOptions.User);
                setLockoutOptions(identityOptions.Lockout, siteSettings);
            })
                .AddRoles<Role>()
                .AddDefaultTokenProviders()
                .AddUserStore<ApplicationUserStore>()
                .AddUserManager<ApplicationUserManager>()
                .AddRoleStore<ApplicationRoleStore>()
                .AddRoleManager<ApplicationRoleManager>()
                .AddSignInManager<ApplicationSignInManager>()
                .AddErrorDescriber<CustomIdentityErrorDescriber>()
                .AddTokenProvider<DataProtectorTokenProvider<User>>(EmailConfirmationTokenProviderName);


            services.AddScoped<IUserClaimsPrincipalFactory<User>, UserClaimsPrincipalFactory<User>>();
            services.AddScoped<UserClaimsPrincipalFactory<User, Role>, UserClaimsPrincipalFactory<User , Role>>();


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IPrincipal>(provider =>
                provider.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.User ?? ClaimsPrincipal.Current);

            services.AddScoped<IApplicationUserManager, ApplicationUserManager>();
            services.AddScoped<UserManager<User>, ApplicationUserManager>();

            services.AddScoped<IApplicationRoleManager, ApplicationRoleManager>();
            services.AddScoped<RoleManager<Role>, ApplicationRoleManager>();

            services.AddScoped<IApplicationSignInManager, ApplicationSignInManager>();
            services.AddScoped<SignInManager<User>, ApplicationSignInManager>();

            services.AddScoped<IApplicationUserStore, ApplicationUserStore>();
            services.AddScoped<UserStore<User, Role, ProjectDbContext, int, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>, ApplicationUserStore>();

            services.AddScoped<IApplicationRoleStore, ApplicationRoleStore>();
            services.AddScoped<RoleStore<Role, ProjectDbContext, int, UserRole, RoleClaim>, ApplicationRoleStore>();


            services.AddAuthentication(o =>
            {
                o.DefaultScheme = IdentityConstants.ApplicationScheme;
                o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            }).AddIdentityCookies(o => { });



            services.ConfigureApplicationCookie(identityOptionsCookies =>
            {
                var provider = services.BuildServiceProvider();
                setApplicationCookieOptions(provider, identityOptionsCookies, siteSettings);
            });

            services.enableImmediateLogout();

            return services;
        }

        private static void addConfirmEmailDataProtectorTokenOptions(this IServiceCollection services, SiteSettings siteSettings)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Tokens.EmailConfirmationTokenProvider = EmailConfirmationTokenProviderName;
            });

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = siteSettings.EmailConfirmationTokenProviderLifespan;
            });
        }

        private static void enableImmediateLogout(this IServiceCollection services)
        {
            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                // enables immediate logout, after updating the user's stat.
                options.ValidationInterval = TimeSpan.Zero;
                options.OnRefreshingPrincipal = principalContext =>
                {
                    // Invoked when the default security stamp validator replaces the user's ClaimsPrincipal in the cookie.

                    //var newId = new ClaimsIdentity();
                    //newId.AddClaim(new Claim("PreviousName", principalContext.CurrentPrincipal.Identity.Name));
                    //principalContext.NewPrincipal.AddIdentity(newId);

                    return Task.CompletedTask;
                };
            });
        }

        private static void setApplicationCookieOptions(IServiceProvider provider, CookieAuthenticationOptions identityOptionsCookies, SiteSettings siteSettings)
        {
            identityOptionsCookies.Cookie.Name = siteSettings.CookieOptions.CookieName;
            identityOptionsCookies.Cookie.HttpOnly = true;
            identityOptionsCookies.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            identityOptionsCookies.Cookie.SameSite = SameSiteMode.Lax;
            identityOptionsCookies.Cookie.IsEssential = true; //  this cookie will always be stored regardless of the user's consent

            identityOptionsCookies.ExpireTimeSpan = siteSettings.CookieOptions.ExpireTimeSpan;
            identityOptionsCookies.SlidingExpiration = siteSettings.CookieOptions.SlidingExpiration;
            identityOptionsCookies.LoginPath = siteSettings.CookieOptions.LoginPath;
            identityOptionsCookies.LogoutPath = siteSettings.CookieOptions.LogoutPath;
            identityOptionsCookies.AccessDeniedPath = siteSettings.CookieOptions.AccessDeniedPath;

            if (siteSettings.CookieOptions.UseDistributedCacheTicketStore)
            {
                // To manage large identity cookies
                identityOptionsCookies.SessionStore = provider.GetRequiredService<ITicketStore>();
            }
        }

        private static void setLockoutOptions(LockoutOptions identityOptionsLockout, SiteSettings siteSettings)
        {
            identityOptionsLockout.AllowedForNewUsers = siteSettings.LockoutOptions.AllowedForNewUsers;
            identityOptionsLockout.DefaultLockoutTimeSpan = siteSettings.LockoutOptions.DefaultLockoutTimeSpan;
            identityOptionsLockout.MaxFailedAccessAttempts = siteSettings.LockoutOptions.MaxFailedAccessAttempts;
        }

        private static void setPasswordOptions(PasswordOptions identityOptionsPassword, SiteSettings siteSettings)
        {
            identityOptionsPassword.RequireDigit = siteSettings.PasswordOptions.RequireDigit;
            identityOptionsPassword.RequireLowercase = siteSettings.PasswordOptions.RequireLowercase;
            identityOptionsPassword.RequireNonAlphanumeric = siteSettings.PasswordOptions.RequireNonAlphanumeric;
            identityOptionsPassword.RequireUppercase = siteSettings.PasswordOptions.RequireUppercase;
            identityOptionsPassword.RequiredLength = siteSettings.PasswordOptions.RequiredLength;
        }

        private static void setSignInOptions(SignInOptions identityOptionsSignIn, SiteSettings siteSettings)
        {
            identityOptionsSignIn.RequireConfirmedEmail = siteSettings.EnableEmailConfirmation;
        }

        private static void setUserOptions(UserOptions identityOptionsUser)
        {
            identityOptionsUser.RequireUniqueEmail = true;
        }
    }
}