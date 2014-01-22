using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AstCore.Models
{
    [Table("User")]
    public class User
    {
        public User()
        {
            this.Roles = new List<Role>();
        }

        public Guid ApplicationId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string AvatarFile { get; set; }
        public bool IsAnonymous { get; set; }
        public DateTime LastActivityDate { get; set; }
        public Application Application { get; set; }
        public Membership Membership { get; set; }
        public Profile Profile { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual Blog Blog { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<NewsItem> News { get; set; }
        public virtual ICollection<NewsComment> NewsComments { get; set; }
        public virtual ICollection<PostComment> PostComments { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual Address Address { get; set; }

        public override string ToString()
        {
            return UserName;
        }

        public bool IsAdmin
        {
            get { return Roles.Any(r => r.RoleName == "Administrator"); }
        }
    }
}
