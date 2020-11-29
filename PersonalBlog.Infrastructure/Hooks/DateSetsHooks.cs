using DNT.Deskly.Domain;
using DNT.Deskly.EFCore.Context;
using DNT.Deskly.EFCore.Context.Extensions;
using DNT.Deskly.EFCore.Context.Hooks;
using DNT.Deskly.Runtime;
using DNT.Deskly.Timing;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalBlog.Infrastructure.Hooks
{
    public class DateInsertHooks : PreInsertHook<IEntity>
    {
        private readonly IUserSession _session;
        private readonly IClock _clock;

        public DateInsertHooks(IUserSession session, IClock clock)
        {
            _session = session ?? throw new ArgumentNullException(nameof(session));
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));
        }
        public override string Name => nameof(DateInsertHooks);

        protected override void Hook(IEntity entity, HookEntityMetadata metadata, IUnitOfWork uow)
        {
            if (metadata.Entry.ToDictionary(x => x.Metadata.Name == "CreatedOn").GetValueOrDefault("CreatedOn") != null)
            {
                metadata.Entry.Property("CreatedOn").CurrentValue = _clock.Now;
            }
        }
    }
    public class DateUpdateHooks : PreUpdateHook<IEntity>
    {
        private readonly IUserSession _session;
        private readonly IClock _clock;

        public DateUpdateHooks(IUserSession session, IClock clock)
        {
            _session = session ?? throw new ArgumentNullException(nameof(session));
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));
        }
        public override string Name => nameof(DateUpdateHooks);

        protected override void Hook(IEntity entity, HookEntityMetadata metadata, IUnitOfWork uow)
        {
            if (metadata.Entry.ToDictionary(x => x.Metadata.Name == "ModifiedOn").GetValueOrDefault("ModifiedOn") != null)
            {
                metadata.Entry.Property("ModifiedOn").CurrentValue = _clock.Now;
            }
        }
    }

}
