using DNT.Deskly.Validation;
using Microsoft.AspNetCore.Identity;
using PersonalBlog.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Application.Identity.Validators
{

    public class RoleValidator : DNT.Deskly.EFCore.Identity.Validation.RoleValidator<Role>
    {

        public RoleValidator(
            IdentityErrorDescriber describer// How to use CustomIdentityErrorDescriber
            ) : base(describer)
        {


        }

        public override async Task<IdentityResult> ValidateAsync(RoleManager<Role> manager, Role user)
        {
            return await base.ValidateAsync(manager, user);
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public override async Task<IEnumerable<ValidationFailure>> Validate(object validatorCaller, Role model)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            if (validatorCaller.GetType() != typeof(UserManager<User>))
            {
                throw new InvalidCastException("dont use Role in another store or CrudServices Exept RoleManager");
            }
            return Enumerable.Empty<ValidationFailure>();
        }
    }
}
