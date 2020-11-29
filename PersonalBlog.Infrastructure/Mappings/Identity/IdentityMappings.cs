using Microsoft.EntityFrameworkCore;

namespace PersonalBlog.Infrastructure.Mappings.Identity
{
    public static class IdentityMappings
    {
        /// <summary>
        /// Adds all of the ASP.NET Core Identity related mappings at once.
        /// More info: http://www.dotnettips.info/post/2577
        /// and http://www.dotnettips.info/post/2578
        /// </summary>
        public static void AddCustomIdentityMappings(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityMappings).Assembly);
        }
    }
}