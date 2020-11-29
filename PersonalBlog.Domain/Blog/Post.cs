using DNT.Deskly.Domain;
using PersonalBlog.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonalBlog.Domain.Blog
{
    public class Post : IEntity, ICreationTracking, IModificationTracking, IHasRowIntegrity, IHasRowVersion
    {
        public virtual int Id { get; set; }
        /// <summary>
        /// gets or sets date of publishing content
        /// </summary>
        public virtual DateTime PublishedOn { get; set; }
        /// <summary>
        /// gets or sets Last Update's Date
        /// </summary>
        public virtual DateTime ModifiedOn { get; set; }
        /// <summary>
        /// gets or sets Last Update's Date
        /// </summary>
        public virtual DateTime CreatedOn { get; set; }
        /// <summary>
        /// gets or sets the blog pot body
        /// </summary>
        public virtual string Body { get; set; }
        /// <summary>
        /// gets or sets the image for post
        /// </summary>
        public virtual string CoverImage { get; set; }
        /// <summary>
        /// gets or sets the description for post
        /// </summary>
        public virtual string Description { get; set; }
        /// <summary>
        /// gets or sets the content title
        /// </summary>
        public virtual string Title { get; set; }
        /// <summary>
        /// gets or sets value  indicating Custom Slug
        /// </summary>
        public virtual string SlugUrl { get; set; }
        /// <summary>
        /// gets or sets meta title for seo
        /// </summary>
        public virtual string MetaTitle { get; set; }
        /// <summary>
        /// gets or sets meta keywords for seo
        /// </summary>
        public virtual string MetaKeywords { get; set; }
        /// <summary>
        /// gets or sets meta description of the content
        /// </summary>
        public virtual string MetaDescription { get; set; }
        /// <summary>
        /// gets or sets
        /// </summary>
        public virtual string FocusKeyword { get; set; }
        /// <summary>
        /// gets or sets value indicating whether the content user no Follow for Seo
        /// </summary>
        public virtual bool UseNoFollow { get; set; } = false;
        /// <summary>
        /// gets or sets value indicating whether the content user no Index for Seo
        /// </summary>
        public virtual bool UseNoIndex { get; set; } = false;
        /// <summary>
        /// gets or sets value indicating whether the content in sitemap
        /// </summary>
        public virtual bool IsInSitemap { get; set; } = true;
        /// <summary>
        /// gets or sets value indicating whether the content show with rssFeed
        /// </summary>
        public virtual bool ShowWithRss { get; set; } = true;

        /// <summary>
        /// gets or sets a value indicating whether the content comments are allowed
        /// </summary>
        public virtual bool AllowComments { get; set; } = true;
        /// <summary>
        /// gets or sets a value indicating whether the content comments are allowed for anonymouses
        /// </summary>
        public virtual bool AllowCommentForAnonymous { get; set; } = true;
        /// <summary>
        /// gets or sets  viewed count by rss
        /// </summary>
        public virtual int ViewCountByRss { get; set; }
        /// <summary>
        /// gets or sets viewed count
        /// </summary>
        public virtual int ViewCount { get; set; }
        /// <summary>
        /// Gets or sets the total number of  comments
        /// <remarks>The same as if we run Item.Comments.where(a=>a.Status==Status.Approved).Count()
        /// We use this property for performance optimization (no SQL command executed)
        /// </remarks>
        /// </summary>
        public virtual int ApprovedCommentsCount { get; set; }
        /// <summary>
        /// Gets or sets the total number of  comments
        /// <remarks>The same as if we run Item.Comments.where(a=>a.Status==Status.UnApproved).Count()
        /// We use this property for performance optimization (no SQL command executed)</remarks></summary>
        public virtual int UnApprovedCommentsCount { get; set; }
        /// <summary>
        /// gets or sets value indicating maximum days count that users can send comment
        /// </summary>
        public virtual int DaysCountForSupportComment { get; set; }
        /// <summary>
        /// gets or sets icon name with size 200*200 px for snippet
        /// </summary>
        public virtual string SocialSnippetIconName { get; set; }
        /// <summary>
        /// gets or sets title for snippet
        /// </summary>
        public virtual string SocialSnippetTitle { get; set; }
        /// <summary>
        /// gets or sets description for snippet
        /// </summary>
        public virtual string SocialSnippetDescription { get; set; }
        /// <summary>
        /// gets or sets name of tags seperated by comma that assosiated with this content fo increase performance
        /// </summary>
        public virtual string TagNames { get; set; }
        /// <summary>
        /// gets or sets counter for Content's report
        /// </summary>
        public virtual int ReportsCount { get; set; }
        /// <summary>
        /// gets or sets Status of LinkBack Notifications
        /// </summary>
        public virtual LinkBackStatus LinkBackStatus { get; set; }

        #region NavigationProperties

        /// <summary>
        /// get or set user that create this record
        /// </summary>
        public virtual User Author { get; set; }

        /// <summary>
        /// gets or sets Id of user that create this record
        /// </summary>
        public virtual int AuthorId { get; set; }
        /// <summary>
        /// get or set  the tags integrated with content
        /// </summary>
        public virtual ICollection<Tag> Tags { get; set; }
        /// <summary>
        /// get or set  the Categories integrated with content
        /// </summary>
        public virtual ICollection<Category> Categories { get; set; }
        /// <summary>
        /// get or set  the PostCategories integrated with content
        /// </summary>
        public virtual ICollection<PostCategory> PostCategories { get; set; }
        /// <summary>
        /// get or set  blog post's Reviews
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; }
        /// <summary>
        /// get or set collection of links that reference to this blog post
        /// </summary>
        public virtual ICollection<LinkBack> LinkBacks { get; set; }

        public byte[] Version { get ; set ; }

        #endregion
    }
    /// <summary>
    /// Represents Status for ReferrerLinks
    /// </summary>
    public enum LinkBackStatus
    {
        [Display(Name = "غیرفعال")]
        Disable,
        [Display(Name = "فعال")]
        Enable,
        [Display(Name = "لینک‌ها داخلی")]
        JustInternal,
        [Display(Name = "لینک‌ها خارجی")]
        JustExternal
    }
}
