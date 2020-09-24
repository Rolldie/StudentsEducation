using Microsoft.EntityFrameworkCore;
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
        public virtual DbSet<Mark> Marks { get; set; }
        public virtual DbSet<Skip> Skips { get; set; }
        public virtual DbSet<WorksSchedule> WorksSchedules { get; set; }
        public EducationDbContext(DbContextOptions<EducationDbContext> options):base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasIndex(e => e.GradeBookNumber).IsUnique();
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasIndex(e =>new { e.Name,e.CourseNumber,e.StartEducationDate}).IsUnique();
            });
            modelBuilder.Entity<ControlType>(entity =>
            {
                entity.HasIndex(e => e.ControlName).IsUnique();
            });
            modelBuilder.Entity<Mark>(entity =>
            {
                entity.HasIndex(e => new { e.WorkId, e.StudentId }).IsUnique();
            });
            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.HasIndex(e => new {  e.SubjectId, e.GroupId }).IsUnique();
            });
            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasIndex(e => new { e.ControlTypeId, e.Name }).IsUnique();
            });
            modelBuilder.Entity<Work>(entity =>
            {
                entity.HasIndex(e => new { e.Name, e.SubjectId }).IsUnique();
                entity.HasIndex(e => new { e.SubjectId, e.WorkNumber }).IsUnique();
            });
            modelBuilder.Entity<FinalControl>(entity =>
            {
                entity.HasIndex(e => new { e.StudentId, e.ScheduleId }).IsUnique();
            });
            modelBuilder.Entity<WorksSchedule>(entity =>
            {
                entity.HasIndex(e => new { e.ScheduleId, e.WorkId }).IsUnique();
            });
        }

    }
}
