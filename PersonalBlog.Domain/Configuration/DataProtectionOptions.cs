using System;

namespace PersonalBlog.Domain.Configuration
{
    public class DataProtectionOptions
    {
        public TimeSpan DataProtectionKeyLifetime { get; set; }
        public string ApplicationName { get; set; }
    }
}