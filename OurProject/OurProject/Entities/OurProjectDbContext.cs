using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OurProject.Entities
{
    public class OurProjectDbContext : IdentityDbContext<User>
    {
        public OurProjectDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<User> users { get; set; }
    }
}
