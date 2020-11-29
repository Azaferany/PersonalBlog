using DNT.Deskly.Domain;
using PersonalBlog.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonalBlog.Domain.Blog
{
    public class Comment : IEntity , IHasRowLevelSecurity
    {
        #region Properties
        /// <summary>
        /// get or set identifier of record
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// gets or sets date of creation
        /// </summary>
        public virtual DateTime CreatedOn { get; set; }
        /// <summary>
        /// gets or sets displayName of this comment's Creator if  he/she is Anonymous
        /// </summary>
        public virtual string CreatorDisplayName { get; set; }
        /// <summary>
        /// gets or sets body of blog post's comment
        /// </summary>
        public virtual string Body { get; set; }
        /// <summary>
        /// gets or sets siteUrl of Creator if he/she is Anonymous
        /// </summary>
        public virtual string SiteUrl { get; set; }
        /// <summary>
        /// gets or sets Email of Creator if he/she is anonymous
        /// </summary>
        public virtual string Email { get; set; }
        /// <summary>
        /// gets or sets status of comment
        /// </summary>
        public virtual CommentStatus Status { get; set; }
        /// <summary>
        /// gets or sets Ip Address of Creator
        /// </summary>
        public virtual string CreatorIp { get; set; }
        /// <summary>
        /// gets or sets datetime that is modified
        /// </summary>
        public virtual DateTime? ModifiedOn { get; set; }
        /// <summary>
        /// gets or sets counter for report this comment
        /// </summary>
        public virtual int ReportsCount { get; set; }
        #endregion

        #region NavigationProperties

        /// <summary>
        /// get or set user that create this record
        /// </summary>
        public virtual User Creator { get; set; }

        /// <summary>
        /// get or set Id of user that create this record
        /// </summary>
        public virtual int? CreatorId { get; set; }
        /// <summary>
        /// gets or sets BlogComment's identifier for Replying and impelemention self referencing
        /// </summary>
        public virtual int? ReplyId { get; set; }
        /// <summary>
        /// gets or sets blog's comment for Replying and impelemention self referencing
        /// </summary>
        public virtual Comment Reply { get; set; }
        /// <summary>
        /// get or set collection of blog's comment for Replying and impelemention self referencing
        /// </summary>
        public virtual ICollection<Comment> Children { get; set; }
        /// <summary>
        /// gets or sets post that this comment sent to it
        /// </summary>
        public virtual Post Post { get; set; }
        /// <summary>
        /// gets or sets post'Id that this comment sent to it
        /// </summary>
        public virtual int PostId { get; set; }
        #endregion
    }

    public enum CommentStatus
    {
        /* 0 - approved, 1 - pending, 2 - spam, -1 - trash */
        [Display(Name = "تأیید شده")]
        Approved = 0,
        [Display(Name = "در انتظار بررسی")]
        Pending = 1,
        [Display(Name = "جفنگ")]
        Spam = 2,
        [Display(Name = "زباله دان")]
        Trash = -1
    }
}
