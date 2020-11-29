using DNT.Deskly.Domain;
using Microsoft.AspNetCore.Identity;

namespace PersonalBlog.Domain.Identity
{
    /// <summary>
    /// More info: http://www.dotnettips.info/post/2577
    /// and http://www.dotnettips.info/post/2578
    /// </summary>
    public class UserToken : IdentityUserToken<int>, ICreationTracking, IHasRowIntegrity

    {
        public virtual User User { get; set; }
    }
}