using DNT.Deskly.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalBlog.Domain.Blog
{
    public class LinkBack : IEntity , IHasRowLevelSecurity
    {
        #region Properties
        /// <summary>
        /// gets or sets link's Id
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// gets or sets text for show Link
        /// </summary>
        public virtual string Title { get; set; }
        /// <summary>
        /// gets or sets link's address
        /// </summary>
        public virtual string Url { get; set; }
        /// <summary>
        /// gets or set value indicating whether this link is internal o external
        /// </summary>
        public virtual LinkBackType Type { get; set; }
        /// <summary>
        /// gets or sets date that this record is added
        /// </summary>
        public virtual DateTime CreatedOn { get; set; }
        public byte[] Version { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// gets or sets Post that associated
        /// </summary>
        public virtual Post Post { get; set; }
        /// <summary>
        /// gets or sets id of Post that associated
        /// </summary>
        public virtual int PostId { get; set; }
        #endregion
    }


    /// <summary>
    /// represents Type of ReferrerLinks
    /// </summary>
    public enum LinkBackType
    {
        /// <summary>
        /// Internal link
        /// </summary>
        Internal,
        /// <summary>
        /// External Link
        /// </summary>
        External
    }
}