using System;
//App
using LMSApp.Data.Configurations;
using LMSApp.Data.Models;
using LMSApp.Data.Models.AssignmentRelated;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Data.Models.Enums;
using LMSApp.Data.Models.GradeAwards;
using LMSApp.Data.Models.MaterialRelated;
using LMSApp.Data.Models.UserTypes;
//Microsoft packages
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LMSApp.Data
{
    public class LMSAppContext : IdentityDbContext<LMSAppUser>
    {
        public LMSAppContext(DbContextOptions<LMSAppContext> options)
            : base(options)
        {
        }

        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<StudentAssignment> StudentAssignments { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<EducatorCourse> EducatorCourses { get; set; }
        public DbSet<EducatorEvent> EducatorEvents { get; set; }
        public DbSet<EducatorLecturecise> LectureciseEducators { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Lecturecise> Lecturecises { get; set; }
        public DbSet<StudentCourse> CourseStudents { get; set; }
        public DbSet<StudentEvent> StudentEvents { get; set; }
        public DbSet<StudentLecturecise> LectureciseStudents { get; set; }

        public DbSet<Badge> Badges { get; set; }
        public DbSet<StudentBadge> StudentBadges { get; set; }

        public DbSet<Material> Materials { get; set; }
        public DbSet<StudentMaterial> StudentMaterials { get; set; }

        public DbSet<Educator> Educator { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }

        public DbSet<WeekTime> WeekTimes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Custom configurations are made for:
            //one to many relationships in many to many entities (to be sure),
            //composite keys
            //unmatching names in one to many relationship.

            builder.ApplyConfiguration(new StudentAssignmentConfiguration());
            builder.ApplyConfiguration(new EducatorCourseConfiguration());
            builder.ApplyConfiguration(new EducatorEventConfiguration());
            builder.ApplyConfiguration(new EducatorLectureciseConfiguration());
            builder.ApplyConfiguration(new StudentCourseConfiguration());
            builder.ApplyConfiguration(new StudentEventConfiguration());
            builder.ApplyConfiguration(new StudentLectureciseConfiguration());
            builder.ApplyConfiguration(new StudentBadgeConfiguration());
            builder.ApplyConfiguration(new StudentMaterialConfiguration());
            builder.ApplyConfiguration(new MaterialsConfiguration());

        }
    }
}
