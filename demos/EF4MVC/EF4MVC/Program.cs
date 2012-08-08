using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
namespace EF4MVC
{
    public class BlogContext : DbContext
    {
        public BlogContext() : base("testing") { }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Blogger> Bloggers { get; set; }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }
        public string title { get; set; }
    }

    public class Blogger
    {
        [Key]
        public int ID { get; set; }
        public string first_name { get; set; }
        public List<Blog> Blogs { get; set; }
        
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.3.5");
            using (var db = new BlogContext())
            {
                db.Blogs.Add(new Blog { Name = "Another Blog " });
                db.SaveChanges();

                foreach (var blog in db.Blogs)
                {
                    Console.WriteLine(blog.Name);
                }


            }
        }
    }
}
