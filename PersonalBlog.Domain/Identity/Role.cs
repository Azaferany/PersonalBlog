using System.Collections.Generic;
using DNT.Deskly.Domain;
using Microsoft.AspNetCore.Identity;

namespace PersonalBlog.Domain.Identity
{
    /// <summary>
    /// More info: http://www.dotnettips.info/post/2577
    /// and http://www.dotnettips.info/post/2578
    /// </summary>
    public class Role : IdentityRole<int>, IEntity, ICreationTracking, IModificationTracking, IHasRowIntegrity
    {
        public Role()
        {
        }

        public Role(string name)
            : this()
        {
            Name = name;
        }

        public Role(string name, string description)
            : this(name)
        {
            Description = description;
        }

        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public virtual ICollection<RoleClaim> Claims { get; set; }
    }
}