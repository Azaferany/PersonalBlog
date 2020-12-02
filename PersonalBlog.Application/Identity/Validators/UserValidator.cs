using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PersonalBlog.Domain.Configuration;
using PersonalBlog.Domain.Identity;
using PersonalBlog.Common.GuardToolkit;
using DNT.Deskly.Validation;
using System.Reflection;
using DNT.Deskly.GuardToolkit;
using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.Application.Identity.Validators
{
    /// <summary>
    /// Extending the Built-in User Validation
    /// More info: http://www.dotnettips.info/post/2579
    /// </summary>>
    public class UserValidator : ModelValidator<User, UserManager<User>>, IUserValidator<User>
    {
        public IdentityErrorDescriber Describer { get; private set; }


        public UserValidator(
            IdentityErrorDescriber describer,// How to use CustomIdentityErrorDescriber
            IOptionsSnapshot<SiteSettings> configurationRoot
            )
        {
            Describer = describer ?? throw new ArgumentNullException(nameof(configurationRoot));


        }

        public override Task<IEnumerable<ValidationFailure>> Validate(UserManager<User> validatorCaller, User model)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var errors = new List<IdentityError>();
            await ValidateUserName(manager, user, errors);
            if (manager.Options.User.RequireUniqueEmail)
            {
                await ValidateEmail(manager, user, errors);
            }
            return errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
        }

        #region Validate
        private async Task ValidateUserName(UserManager<User> manager, User user, ICollection<IdentityError> errors)
        {
            var userName = await manager.GetUserNameAsync(user);
            if (string.IsNullOrWhiteSpace(userName))
            {
                errors.Add(Describer.InvalidUserName(userName));
            }
            else if (!string.IsNullOrEmpty(manager.Options.User.AllowedUserNameCharacters) &&
                userName.Any(c => !manager.Options.User.AllowedUserNameCharacters.Contains(c)))
            {
                errors.Add(Describer.InvalidUserName(userName));
            }
            else
            {
                var owner = await manager.FindByNameAsync(userName);
                if (owner != null &&
                    !string.Equals(await manager.GetUserIdAsync(owner), await manager.GetUserIdAsync(user)))
                {
                    errors.Add(Describer.DuplicateUserName(userName));
                }
            }
        }

        // make sure email is not empty, valid, and unique
        private async Task ValidateEmail(UserManager<User> manager, User user, List<IdentityError> errors)
        {
            var email = await manager.GetEmailAsync(user);
            if (string.IsNullOrWhiteSpace(email))
            {
                errors.Add(Describer.InvalidEmail(email));
                return;
            }
            if (!new EmailAddressAttribute().IsValid(email))
            {
                errors.Add(Describer.InvalidEmail(email));
                return;
            }
            var owner = await manager.FindByEmailAsync(email);
            if (owner != null &&
                !string.Equals(await manager.GetUserIdAsync(owner), await manager.GetUserIdAsync(user)))
            {
                errors.Add(Describer.DuplicateEmail(email));
            }
        }
        #endregion
    }
}