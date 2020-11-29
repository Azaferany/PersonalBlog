using System;
using System.Linq;
using DNT.Deskly.Data;
using DNT.Deskly.EFCore.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PersonalBlog.Domain.Configuration;

namespace PersonalBlog.Application.Services
{
    public class DbSetup : IDbSetup
    {
        private readonly IUnitOfWork _uow;
        private readonly IOptionsSnapshot<SiteSettings> _settings;
        private readonly ILogger<DbSetup> _logger;

        public DbSetup(IUnitOfWork uow,
            IOptionsSnapshot<SiteSettings> settings,
            ILogger<DbSetup> logger)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Seed()
        {
            SeedIdentity();
        }

        private void SeedIdentity()
        {


        }
    }
}