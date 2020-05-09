using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentsEducation.Infrastructure.Identity.Data;

namespace StudentsEducation.Infrastructure.Identity
{
    public class AccountDbContext:IdentityDbContext
    {
        public DbSet<Role> Roles { get; set; }
        public  DbSet<AppUser> Users { get; set; }
        public AccountDbContext(DbContextOptions<AccountDbContext> options)
            :base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //change code
        }
    }
}
