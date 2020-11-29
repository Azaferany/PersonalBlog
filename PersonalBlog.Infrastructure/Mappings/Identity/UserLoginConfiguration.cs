using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalBlog.Domain.Identity;

namespace PersonalBlog.Infrastructure.Mappings.Identity
{
    public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.HasOne(userLogin => userLogin.User)
                   .WithMany(user => user.Logins)
                   .HasForeignKey(userLogin => userLogin.UserId);

            builder.ToTable("AppUserLogins");
        }
    }
}