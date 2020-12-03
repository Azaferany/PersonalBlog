using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalBlog.Domain.Identity;
using DNT.Deskly.EFCore.Services.Application;
using DNT.Deskly.Validation;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace PersonalBlog.Application.Identity.Validators
{
    /// <summary>
    /// Extending the Built-in User Validation
    /// More info: http://www.dotnettips.info/post/2579
    /// </summary>>
    public class UserValidator : DNT.Deskly.EFCore.Identity.Validation.UserValidator<User> 
    {



        public UserValidator(
            IdentityErrorDescriber describer// How to use CustomIdentityErrorDescriber
            ) : base(describer)
        {


        }

        public override async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {
            return await base.ValidateAsync(manager, user);
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public override async Task<IEnumerable<ValidationFailure>> Validate(object validatorCaller, User model)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            if (validatorCaller.GetType() != typeof(UserManager<User>))
            {
                throw new InvalidCastException("dont use User in another store or CrudServices Exept UserManager");
            }
            return Enumerable.Empty<ValidationFailure>();
        }
    }
}