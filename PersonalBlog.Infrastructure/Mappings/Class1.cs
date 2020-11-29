using DNT.Deskly.Caching;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Caching.SqlServer;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalBlog.Infrastructure.Mappings
{
    public static class ModelBuilderExtensions
    {
        public static void ApplySSqlCacheConfiguration(this ModelBuilder builder,string tableName="Cache",string schema="dbo")
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            builder.Entity<Cache>().Property(e => e.Id).HasMaxLength(449);
            builder.Entity<Cache>().Property(e => e.Value).IsRequired();

            builder.Entity<Cache>().HasIndex(e => e.ExpiresAtTime).HasDatabaseName("IX_Cache_ExpiresAtTime");

            builder.Entity<Cache>().ToTable(name: tableName, schema: schema);

        }
    }
}
