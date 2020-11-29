using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalBlog.Domain.Blog;
using PersonalBlog.Domain.Identity;

namespace PersonalBlog.Infrastructure.Mappings.Blog
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasMany(p => p.Categories)
            .WithMany(p => p.Posts)
            .UsingEntity<PostCategory>(
                j => j
                    .HasOne(pt => pt.Category)
                    .WithMany(t => t.CategoryPosts)
                    .HasForeignKey(pt => pt.CategoryId),
                i => i
                    .HasOne(pt => pt.Post)
                    .WithMany(p => p.PostCategories)
                    .HasForeignKey(pt => pt.PostId),
                z =>
                {
                    z.HasKey(t => new { t.PostId, t.CategoryId });
                });
        }
    }
}