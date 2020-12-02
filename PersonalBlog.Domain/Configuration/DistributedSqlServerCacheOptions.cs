namespace PersonalBlog.Domain.Configuration
{
    public class DistributedSqlServerCacheOptions
    {
        public string ConnectionString { set; get; }
        public string TableName { set; get; } = "Cache";
        public string SchemaName { set; get; } = "dbo";
    }
}