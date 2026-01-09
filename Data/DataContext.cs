using System.Data;
using EnglishCenterMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using EnglishCenterMVC.Models;
namespace EnglishCenterMVC.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }
        public virtual DbSet<Section> Sections{ get; set; }
        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<Submission> Submissions { get; set; }
    }
}
