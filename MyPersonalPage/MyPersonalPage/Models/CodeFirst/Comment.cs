using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPersonalPage.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string AuthorId { get; set; }
        public string Body { get; set; }
        public DateTimeOffset Created {get; set;}
        public DateTimeOffset? Updated { get; set; }
        public string UpdatedReason { get; set; }

        public virtual Post Post { get; set; }                     //matches up with PostID
        public virtual ApplicationUser Author { get; set; }        //matches up with AuthorID


    }
}