
using DNT.Deskly.Data;
using DNT.Deskly.Dependency;
using DNT.Deskly.EFCore.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PersonalBlog.Domain.Configuration;
using PersonalBlog.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalBlog.Application.Services
{
    public class DbSetup : IDbSetup
    {
        private readonly IUnitOfWork _uow;
        private readonly IOptionsSnapshot<SiteSettings> _settings;
        private readonly ILogger<DbSetup> _logger;

        public DbSetup(IUnitOfWork uow,
            IOptionsSnapshot<SiteSettings> settings,
            ILogger<DbSetup> logger)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Seed()
        {

            //SeedIdentity();











        }

        private void SeedIdentity()
        {


        }
    }
    public interface IModelValidator<in TModel, TValidatorCaller> : IModelValidator
    {
        /// <summary>
        /// Validate the specified instance synchronously.
        /// contains validation logic and business rules validation
        /// </summary>
        /// <param name="model">model to validate</param>
        /// <returns>
        /// A list of <see cref="ValidationFailure"/> indicating the results of validating the model value.
        /// </returns>
        Task<object> Validate(TValidatorCaller validatorCaller, TModel model);
    }
    public interface IModelValidator<in TModel> : IModelValidator
    {
        /// <summary>
        /// Validate the specified instance synchronously.
        /// contains validation logic and business rules validation
        /// </summary>
        /// <param name="model">model to validate</param>
        /// <returns>
        /// A list of <see cref="ValidationFailure"/> indicating the results of validating the model value.
        /// </returns>
        Task<object> Validate(object validatorCaller, TModel model);
    }

    public interface IModelValidator : ITransientDependency
    {
        /// <summary>
        /// Validate the specified instance synchronously.
        /// contains validation logic and business rules validation
        /// </summary>
        /// <param name="model">model to validate</param>
        /// <returns>
        /// A list of <see cref="ValidationFailure"/> indicating the results of validating the model value.
        /// </returns>
        Task<object> Validate(object validatorCaller, object model);

        bool CanValidateInstancesOfType(Type type);
    }
    public class validtor: IModelValidator<User, class1>, IModelValidator<User, class2>, IModelValidator
    {

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<object> Validate(class1 validatorCaller, User model)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            //return await ValidateAsync(validatorCaller, model);
            return null;
        }

        public Task<object> Validate(class2 validatorCaller, User model)
        {
            throw new NotImplementedException();
        }

        bool IModelValidator.CanValidateInstancesOfType(Type type)
        {
            throw new NotImplementedException();
        }

        Task<object> IModelValidator.Validate(object validatorCaller, object model)
        {
            throw new NotImplementedException();
        }
    }
    public class class1
    {

    }
    public class class2
    {

    }
}
