using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsEducation.Infrastructure.Identity
{
    public class AccountDbContext:IdentityDbContext
    {
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
