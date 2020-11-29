using DNT.Deskly.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalBlog.Domain.Blog
{
    public class PostCategory : IHasRowLevelSecurity
    {
        public virtual Post Post { get; set; }
        public virtual Category Category { get; set; }
        public virtual int PostId { get; set; }
        public virtual int CategoryId { get; set; }

    }
}
