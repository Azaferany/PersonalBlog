﻿using DNT.Deskly.Domain;
using Microsoft.AspNetCore.Identity;

namespace PersonalBlog.Domain.Identity
{
    /// <summary>
    /// More info: http://www.dotnettips.info/post/2577
    /// and http://www.dotnettips.info/post/2578
    /// </summary>
    public class UserRole : IdentityUserRole<int>, ICreationTracking, IModificationTracking, IHasRowIntegrity


    {
        public virtual User User { get; set; }

        public virtual Role Role { get; set; }
    }
}