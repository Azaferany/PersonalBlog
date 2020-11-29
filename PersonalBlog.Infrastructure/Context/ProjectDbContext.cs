using System.Collections.Generic;
using DNT.Deskly.EFCore.Caching;
using DNT.Deskly.EFCore.Context;
using DNT.Deskly.EFCore.Context.Extensions;
using DNT.Deskly.EFCore.Context.Hooks;
using DNT.Deskly.EFCore.Cryptography;
using DNT.Deskly.EFCore.Logging;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Domain.Blog;
using PersonalBlog.Domain.Identity;
using PersonalBlog.Infrastructure.Mappings;
using PersonalBlog.Infrastructure.Mappings.Identity;

namespace PersonalBlog.Infrastructure.Context
{
    public class ProjectDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>,
        IUnitOfWork
    {
        public ProjectDbContext(
            DbContextOptions<ProjectDbContext> options,
            IEnumerable<IHook> hooks) : base(options, hooks)
        {
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<LinkBack> LinkBacks { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyLogConfiguration();
            modelBuilder.ApplyProtectionKeyConfiguration();
            modelBuilder.ApplySSqlCacheConfiguration(); 
            modelBuilder.AddCustomIdentityMappings();

            modelBuilder.AddTrackingFields<int>();
            //modelBuilder.AddTenancyField<long>();  TODO: Tenancy
            modelBuilder.AddIsDeletedField();
            modelBuilder.AddRowVersionField();
            modelBuilder.AddRowIntegrityField();
            modelBuilder.AddRowLevelSecurityField<int>();

            modelBuilder.NormalizeDateTime();
            modelBuilder.NormalizeDecimalPrecision(precision: 20, scale: 6);

            base.OnModelCreating(modelBuilder);
        }
    }
}