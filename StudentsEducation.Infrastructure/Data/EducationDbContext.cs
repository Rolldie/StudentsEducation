using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using StudentsEducation.Domain.Entities;

namespace StudentsEducation.Infrastructure.Data
{
    public class EducationDbContext:DbContext
    {
        public virtual DbSet<Cathedra> Cathedras { get; set; } 
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<ControlType> ControlTypes { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Work> Works { get; set; }
        public virtual DbSet<FinalControl> FinalControls { get; set; }

        public EducationDbContext(DbContextOptions<EducationDbContext> options):base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
