using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Loginpage.Models.data
{
    public class ApplicationContext:IdentityDbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) :base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
