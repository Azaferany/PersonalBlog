using System;

namespace PersonalBlog.Domain.Configuration
{
    public class CookieOptions
    {
        public string AccessDeniedPath { get; set; }
        public string CookieName { get; set; }
        public TimeSpan ExpireTimeSpan { get; set; }
        public string LoginPath { get; set; }
        public string LogoutPath { get; set; }
        public bool SlidingExpiration { get; set; }
        public bool UseDistributedCacheTicketStore { set; get; } = true;
        public bool UseDistributedMemoryCache { set; get; } = false;
        public DistributedSqlServerCacheOptions DistributedSqlServerCacheOptions { set; get; }
    }
}