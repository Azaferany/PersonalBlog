using AutoMapper;
using Castle.DynamicProxy;
using DNT.Deskly.Application;
using DNT.Deskly.Configuration;
using DNT.Deskly.Dependency;
using DNT.Deskly.Domain;
using DNT.Deskly.EFCore.Services.Application;
using DNT.Deskly.EFCore.Transaction;
using DNT.Deskly.Eventing;
using DNT.Deskly.FluentValidation;
using DNT.Deskly.Validation;
using DNT.Deskly.Validation.Interception;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PersonalBlog.Application.Contracts;
using PersonalBlog.Application.Identity;
using PersonalBlog.Domain.Configuration;
using PersonalBlog.Domain.Identity;
using System.Linq;

namespace PersonalBlog.Application
{
    public static class ApplicationRegistry
    {
        private static readonly ProxyGenerator ProxyGenerator = new ProxyGenerator();


        public static void AddApplication(this IServiceCollection services)
        {

            //services.AddClassesAsImplementedInterface(Assembly.GetEntryAssembly(), typeof(ICrudService<>));
            services.AddScoped<IdentityErrorDescriber, CustomIdentityErrorDescriber>();

            services.AddScoped<ISecurityTrimmingService, SecurityTrimmingService>();



            services.AddScoped<ISecurityTrimmingService, SecurityTrimmingService>();

            services.AddScoped<IdentityErrorDescriber, CustomIdentityErrorDescriber>();

            services.AddAutoMapper(typeof(ApplicationRegistry));

            services.AddValidatorsFromAssembly(typeof(ApplicationRegistry).Assembly);

            services.AddScoped(typeof(ICrudService<,>), typeof(CrudService<,>));

            //services.AddScoped<ICrudService<,>> ();
            services.AddScoped<ICrudService<int, User>, CrudService<int, User>>();


            //var turget = new MyTaskService<User>();
            //var type = typeof(ITaskService<>);
            //var r = type.MakeGenericType(turget.GetType().GetGenericArguments());

            //var x = ProxyGenerator.CreateInterfaceProxyWithTargetInterface(r, turget,
            //    (IInterceptor)new ExceptionHandlingInterceptor(null));

            //(new CrudService<int, User>(new I(), null)).GetType().GetGenericArguments();
            services.Scan(scan => scan
                //          .FromCallingAssembly()
                //.AddClasses(x => x.AssignableTo(typeof(ICrudService<,>))) // Can close generic types
                //.AsMatchingInterface().WithScopedLifetime()
                //              .FromCallingAssembly()
                //.AddClasses(x => x.AssignableTo(typeof(ICrudService<>))) // Can close generic types
                //.AsMatchingInterface().WithScopedLifetime()

                .FromCallingAssembly()
                .AddClasses(classes => classes.AssignableTo<ISingletonDependency>())
                .AsMatchingInterface()
                .WithSingletonLifetime()
                .AddClasses(classes => classes.AssignableTo<IScopedDependency>())
                .AsMatchingInterface()
                .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo<ITransientDependency>())
                .AsMatchingInterface()
                .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(IBusinessEventHandler<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(IModelValidator<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
            //services.Decorate(typeof(ICrudService<, >), (target, serviceProvider) =>
            //                             ProxyGenerator.CreateInterfaceProxyWithTargetInterface(
            //                             typeof(ICrudService<int, User>),
            //                             target, serviceProvider.GetRequiredService<ValidationInterceptor>(),
            //                         (IInterceptor)serviceProvider.GetRequiredService<TransactionInterceptor>(),
            //                         (IInterceptor)serviceProvider.GetRequiredService<ExceptionHandlingInterceptor>()));

            foreach (var descriptor in services.Where(s => typeof(IApplicationService).IsAssignableFrom(s.ServiceType))
                .ToList())
            {
                if (descriptor.ServiceType == typeof(IKeyValueService))
                    services.TryDecorate(descriptor.ServiceType, (target, serviceProvider) =>
                         ProxyGenerator.CreateInterfaceProxyWithTargetInterface(
                              descriptor.ServiceType.MakeGenericType(target.GetType().GetGenericArguments()),
                              target, serviceProvider.GetRequiredService<ValidationInterceptor>(),
                      (IInterceptor)serviceProvider.GetRequiredService<TransactionInterceptor>()));
                //else if (descriptor.ServiceType == typeof(ICrudService<>))
                //{
                //    foreach (var entityType in typeof(SiteSettings).Assembly.GetTypes().Where(x=>x==typeof(IEntity)).ToList())
                //        services.TryDecorate(descriptor.ServiceType.MakeGenericType(entityType), (target, serviceProvider) =>
                //            ProxyGenerator.CreateInterfaceProxyWithTargetInterface(
                //            descriptor.ServiceType.MakeGenericType(target.GetType().GetGenericArguments()),
                //            target, serviceProvider.GetRequiredService<ValidationInterceptor>(),
                //            (IInterceptor)serviceProvider.GetRequiredService<TransactionInterceptor>()));
                //}
                else if (descriptor.ServiceType == typeof(ICrudService<,>))
                {
                    foreach (var entityType in typeof(SiteSettings).Assembly.GetTypes().Where(s => s.IsAssignableTo(typeof(IEntity))).ToList())
                    {
                        services.AddScoped(typeof(ICrudService<,>).MakeGenericType(entityType.GetGenericArguments().FirstOrDefault() ?? typeof(int), entityType),
    typeof(CrudService<,>).MakeGenericType(entityType.GetGenericArguments().FirstOrDefault() ?? typeof(int), entityType));


                        services.Decorate(descriptor.ServiceType.MakeGenericType(entityType.GetGenericArguments().FirstOrDefault() ?? typeof(int), entityType), (target, serviceProvider) =>
                            ProxyGenerator.CreateInterfaceProxyWithTargetInterface(
                            descriptor.ServiceType.MakeGenericType(typeof(int), entityType),
                            target, serviceProvider.GetRequiredService<ValidationInterceptor>(),
                        (IInterceptor)serviceProvider.GetRequiredService<TransactionInterceptor>()));
                    }
                        

                }
            }
            
            }

        }   
}
