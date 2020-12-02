using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    public class ApplicationRoleManager :
        RoleManager<Role>,
        IApplicationRoleManager,
        IQueryableRoleManager
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUnitOfWork _uow;
        private readonly IdentityErrorDescriber _errors;
        private readonly ILookupNormalizer _keyNormalizer;
        private readonly ILogger<ApplicationRoleManager> _logger;
        private readonly IEnumerable<IRoleValidator<Role>> _roleValidators;
        private readonly IApplicationRoleStore _store;
        private readonly DbSet<User> _users;

        public ApplicationRoleManager(
            IApplicationRoleStore store,
            IEnumerable<IRoleValidator<Role>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<ApplicationRoleManager> logger,
            IHttpContextAccessor contextAccessor,
            IUnitOfWork uow) :
            base((RoleStore<Role, ProjectDbContext, int, UserRole, RoleClaim>)store, roleValidators, keyNormalizer, errors, logger)
        {
            _store = store ?? throw new ArgumentNullException(nameof(store));
            _roleValidators = roleValidators ?? throw new ArgumentNullException(nameof(roleValidators));
            _keyNormalizer = keyNormalizer ?? throw new ArgumentNullException(nameof(keyNormalizer));
            _errors = errors ?? throw new ArgumentNullException(nameof(errors));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _users = _uow.Set<User>();
        }

        #region BaseClass

        #endregion
    }
}
