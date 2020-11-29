using System.IO;
using System.Linq;
using DNT.Deskly.EFCore.Context.Hooks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PersonalBlog.Infrastructure.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ProjectDbContext>
    {
        public ProjectDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<ProjectDbContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnectionstring");

            builder.UseSqlServer(connectionString);

            return new ProjectDbContext(builder.Options, Enumerable.Empty<IHook>());
        }
    }
}