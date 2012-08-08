using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeFirstMVC.Models
{
  public class Blog
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string BloggerName { get; set; }
    public DateTime DateCreated { get; set; }
     public virtual ICollection<Post> Posts { get; set; }
  }


  public class Post
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime DateCreated { get; set; }
    public string Content { get; set; }
    public int BlogId { get; set; }

    public virtual ICollection<Comment> Comments { get; set; }

  }

  public class Comment
  {

    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public string Content { get; set; }
    public int PostId { get; set; }

    //navigation back to parent
    public Post Post { get; set; }
  }
}





