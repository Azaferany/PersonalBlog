using DNT.Deskly.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalBlog.Domain.Blog
{
    public class Category : IEntity, ICreationTracking, IModificationTracking
    {
        #region Properties
        /// <summary>
        /// sets or gets Tag's identifier
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// sets or gets Tag's name
        /// </summary>
        public virtual string Title { get; set; }
        /// <summary>
        /// gets or sets the description for post
        /// </summary>
        public virtual string Description { get; set; }
        /// <summary>
        /// gets or sets name of tags seperated by comma that assosiated with this content fo increase performance
        /// </summary>
        public virtual string TagNames { get; set; }

        /// <summary>
        /// gets or sets the image for post
        /// </summary>
        public virtual string CoverImage { get; set; }
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
        #endregion

        #region NavigationProperties
        /// <summary>
        /// sets or gets Categories posts
        /// </summary>
        public virtual ICollection<Post> Posts { get; set; }
        /// <summary>
        /// sets or gets Categories CategoryPosts
        /// </summary>
        public virtual ICollection<PostCategory> CategoryPosts { get; set; }
        #endregion

    }
}
