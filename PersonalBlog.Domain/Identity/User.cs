using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using DNT.Deskly.Domain;

namespace PersonalBlog.Domain.Identity
{
    /// <summary>
    /// More info: http://www.dotnettips.info/post/2577
    /// and http://www.dotnettips.info/post/2578
    /// plus http://www.dotnettips.info/post/2559
    /// </summary>
    public class User : IdentityUser<int>, IEntity, ICreationTracking, IModificationTracking, IHasRowIntegrity, IHasRowVersion
    {
        public User()
        {
            UserTokens = new HashSet<UserToken>();
        }

        [StringLength(450)]
        public string FirstName { get; set; }

        [StringLength(450)]
        public string LastName { get; set; }

        [NotMapped]
        public string DisplayName
        {
            get
            {
                var displayName = $"{FirstName} {LastName}";
                return string.IsNullOrWhiteSpace(displayName) ? UserName : displayName;
            }
        }

        [StringLength(450)]
        public string PhotoFileName { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? LastVisitDateTime { get; set; }

        public bool IsEmailPublic { get; set; }

        public string Location { set; get; }

        public bool IsActive { get; set; } = true;

        public virtual ICollection<UserToken> UserTokens { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public virtual ICollection<UserLogin> Logins { get; set; }

        public virtual ICollection<UserClaim> Claims { get; set; }

        public byte[] Version { get; set; }
    }
}