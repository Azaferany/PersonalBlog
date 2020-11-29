using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using PersonalBlog.Domain.Identity;
using DNT.Deskly.EFCore.Context;
using PersonalBlog.Infrastructure.Context;
using PersonalBlog.Application.Identity.Contracts;

namespace PersonalBlog.Application.Identity
{
    /// <summary>
    /// More info: http://www.dotnettips.info/post/2578
    /// </summary>
    public class ApplicationUserManager :
        UserManager<User>,
        IApplicationUserManager
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUnitOfWork _uow;
        private readonly IdentityErrorDescriber _errors;
        private readonly ILookupNormalizer _keyNormalizer;
        private readonly ILogger<ApplicationUserManager> _logger;
        private readonly IOptions<IdentityOptions> _optionsAccessor;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IEnumerable<IPasswordValidator<User>> _passwordValidators;
        private readonly IServiceProvider _services;
        private readonly DbSet<User> _users;
        private readonly DbSet<Role> _roles;
        private readonly IApplicationUserStore _userStore;
        private readonly IEnumerable<IUserValidator<User>> _userValidators;
        private User _currentUserInScope;

        public ApplicationUserManager(
            IApplicationUserStore store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<ApplicationUserManager> logger,
            IHttpContextAccessor contextAccessor,
            IUnitOfWork uow)
            : base(
                (UserStore<User, Role, ProjectDbContext, int, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>)store,
                  optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _userStore = store ?? throw new ArgumentNullException(nameof(_userStore));
            _optionsAccessor = optionsAccessor ?? throw new ArgumentNullException(nameof(_optionsAccessor));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(_passwordHasher));
            _userValidators = userValidators ?? throw new ArgumentNullException(nameof(_userValidators));
            _passwordValidators = passwordValidators ?? throw new ArgumentNullException(nameof(_passwordValidators));
            _keyNormalizer = keyNormalizer ?? throw new ArgumentNullException(nameof(_keyNormalizer));
            _errors = errors ?? throw new ArgumentNullException(nameof(_errors));
            _services = services ?? throw new ArgumentNullException(nameof(_services));
            _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(_contextAccessor));
            _uow = uow ?? throw new ArgumentNullException(nameof(_uow));
            _users = uow.Set<User>();
            _roles = uow.Set<Role>();
        }

        #region BaseClass

        string IApplicationUserManager.CreateTwoFactorRecoveryCode()
        {
            return base.CreateTwoFactorRecoveryCode();
        }

        Task<PasswordVerificationResult> IApplicationUserManager.VerifyPasswordAsync(IUserPasswordStore<User> store, User user, string password)
        {
            return base.VerifyPasswordAsync(store, user, password);
        }

        public override async Task<IdentityResult> CreateAsync(User user)
        {
            return await base.CreateAsync(user);
        }

        public override async Task<IdentityResult> CreateAsync(User user, string password)
        {
            return await base.CreateAsync(user, password);
        }

        public override async Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword)
        {
            return await base.ChangePasswordAsync(user, currentPassword, newPassword);

        }

        public override async Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
        {
            return await base.ResetPasswordAsync(user, token, newPassword);
        }
        Task<IdentityResult> IApplicationUserManager.UpdatePasswordHash(User user, string newPassword, bool validatePassword)
        {
            return base.UpdatePasswordHash(user, newPassword, validatePassword);
        }
        #endregion
    }
}