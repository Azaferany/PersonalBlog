using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalBlog.Domain.Identity;

namespace PersonalBlog.Infrastructure.Mappings.Identity
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(p => p.Roles)
                .WithMany(p => p.Users)
                .UsingEntity<UserRole>(
                 j => j
                    .HasOne(pt => pt.Role)
                    .WithMany(t => t.UserRoles)
                    .HasForeignKey(pt => pt.RoleId),
                 i => i
                    .HasOne(pt => pt.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(pt => pt.UserId),
                z =>
                {
                    z.HasKey(t => new { t.UserId, t.RoleId });
                });

            builder.ToTable("AppUsers");
        }
    }
}