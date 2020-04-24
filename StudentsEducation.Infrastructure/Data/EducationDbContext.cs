using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using StudentsEducation.Domain.Entities;

namespace StudentsEducation.Infrastructure.Data
{
    public class EducationDbContext:DbContext
    {
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Group> Groups { get; set; }

        public EducationDbContext(DbContextOptions<EducationDbContext> options):base(options)
        {
            Database.EnsureCreated();
        }

    }
}
