using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CodeFirstMVC.Models
{
  public class BlogContext: DbContext
  {
    public DbSet<Blog>  Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public class BlogContextInitializer : DropCreateDatabaseIfModelChanges<BlogContext>
    {
        protected override void Seed(BlogContext context)
        {
            new List<Blog>
                {
                    new Blog
                        {BloggerName = "Julie", Title = "My Code First Blog", DateCreated = new DateTime(2011, 3, 15),
            Posts=new List<Post>
            {new Post { Title="ForeignKeyAttribute Annotation",DateCreated=System.DateTime.Now, Content="Mark navigation property with ForeignKey"}}
            },
            new Blog {BloggerName = "Ingemaar", Title = "My Life as a Blog",DateCreated = new DateTime(2011, 3, 10),},
            new Blog { BloggerName = "Sampson", Title = "Tweeting for Dogs",DateCreated = new DateTime(2011, 3, 1),}
        }.ForEach(b => context.Blogs.Add(b));



            base.Seed(context);
        }
    }
  }
}