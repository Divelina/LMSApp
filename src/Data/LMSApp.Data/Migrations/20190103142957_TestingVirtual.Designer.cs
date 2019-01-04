﻿// <auto-generated />
using System;
using LMSApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LMSApp.Data.Migrations
{
    [DbContext(typeof(LMSAppContext))]
    [Migration("20190103142957_TestingVirtual")]
    partial class TestingVirtual
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LMSApp.Data.Models.AssignmentRelated.Assignment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AssignmentType");

                    b.Property<string>("Content");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<string>("EducatorId")
                        .IsRequired();

                    b.Property<string>("LectureciseId")
                        .IsRequired();

                    b.Property<decimal>("MaxGrade")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Test");

                    b.HasKey("Id");

                    b.HasIndex("EducatorId");

                    b.HasIndex("LectureciseId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("LMSApp.Data.Models.AssignmentRelated.StudentAssignment", b =>
                {
                    b.Property<string>("AssignmentId");

                    b.Property<string>("StudentId");

                    b.Property<int>("AssignmentStatus");

                    b.Property<DateTime?>("DueDate");

                    b.Property<bool>("Expired");

                    b.Property<decimal>("Grade")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<string>("GradeComment");

                    b.Property<string>("GraderId");

                    b.Property<string>("LectureciseId");

                    b.HasKey("AssignmentId", "StudentId");

                    b.HasIndex("GraderId");

                    b.HasIndex("LectureciseId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentAssignments");
                });

            modelBuilder.Entity("LMSApp.Data.Models.CourseRelated.Course", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("Major");

                    b.Property<string>("MajorDescription");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Semester");

                    b.Property<string>("Year")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("LMSApp.Data.Models.CourseRelated.EducatorCourse", b =>
                {
                    b.Property<string>("CourseId");

                    b.Property<string>("EducatorId");

                    b.Property<int>("TeachingRole");

                    b.HasKey("CourseId", "EducatorId");

                    b.HasIndex("EducatorId");

                    b.ToTable("EducatorCourses");
                });

            modelBuilder.Entity("LMSApp.Data.Models.CourseRelated.EducatorEvent", b =>
                {
                    b.Property<string>("EducatorId");

                    b.Property<string>("EventId");

                    b.Property<int>("EventRole");

                    b.HasKey("EducatorId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("EducatorEvents");
                });

            modelBuilder.Entity("LMSApp.Data.Models.CourseRelated.EducatorLecturecise", b =>
                {
                    b.Property<string>("EducatorId");

                    b.Property<string>("LectureciseId");

                    b.HasKey("EducatorId", "LectureciseId");

                    b.HasIndex("LectureciseId");

                    b.ToTable("LectureciseEducators");
                });

            modelBuilder.Entity("LMSApp.Data.Models.CourseRelated.Event", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GroupId");

                    b.Property<string>("LectureciseId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime?>("StartTime");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("LectureciseId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("LMSApp.Data.Models.CourseRelated.Lecturecise", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CourseId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Lecturecises");
                });

            modelBuilder.Entity("LMSApp.Data.Models.CourseRelated.StudentCourse", b =>
                {
                    b.Property<string>("CourseId");

                    b.Property<string>("StudentId");

                    b.HasKey("CourseId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("CourseStudents");
                });

            modelBuilder.Entity("LMSApp.Data.Models.CourseRelated.StudentEvent", b =>
                {
                    b.Property<string>("StudentId");

                    b.Property<string>("EventId");

                    b.HasKey("StudentId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("StudentEvents");
                });

            modelBuilder.Entity("LMSApp.Data.Models.CourseRelated.StudentLecturecise", b =>
                {
                    b.Property<string>("LectureciseId");

                    b.Property<string>("StudentId");

                    b.HasKey("LectureciseId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("LectureciseStudents");
                });

            modelBuilder.Entity("LMSApp.Data.Models.GradeAwards.Badge", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AssignmentId");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("AssignmentId");

                    b.ToTable("Badges");
                });

            modelBuilder.Entity("LMSApp.Data.Models.GradeAwards.StudentBadge", b =>
                {
                    b.Property<string>("BadgeId");

                    b.Property<string>("StudentId");

                    b.HasKey("BadgeId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentBadges");
                });

            modelBuilder.Entity("LMSApp.Data.Models.LMSAppUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FamilyName")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("LMSApp.Data.Models.MaterialRelated.Material", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AssignmentId");

                    b.Property<string>("Content");

                    b.Property<string>("CourseId");

                    b.Property<string>("EducatorId");

                    b.Property<string>("EventId");

                    b.Property<DateTime>("LastChangeDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("UrlLink");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentId");

                    b.HasIndex("CourseId");

                    b.HasIndex("EducatorId");

                    b.HasIndex("EventId");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("LMSApp.Data.Models.MaterialRelated.StudentMaterial", b =>
                {
                    b.Property<string>("MaterialId");

                    b.Property<string>("StudentId");

                    b.HasKey("MaterialId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentMaterials");
                });

            modelBuilder.Entity("LMSApp.Data.Models.UserTypes.Educator", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FacultyName");

                    b.Property<string>("Info");

                    b.Property<string>("PersonalPageLink");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Educator");
                });

            modelBuilder.Entity("LMSApp.Data.Models.UserTypes.Group", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AssignmentId");

                    b.Property<string>("Description");

                    b.Property<int>("Major");

                    b.Property<int>("Number");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("LMSApp.Data.Models.UserTypes.Student", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FacultyName");

                    b.Property<string>("GroupId");

                    b.Property<DateTime?>("LastSeen");

                    b.Property<int>("Major");

                    b.Property<int>("StudentUniId");

                    b.Property<decimal>("TaskCompletionRating")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<decimal>("TaskGradeRating")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<decimal>("TotalGradeExcercise")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<decimal>("TotalGradeLectures")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("LMSApp.Data.Models.WeekTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DayOfWeek");

                    b.Property<string>("EndHour");

                    b.Property<string>("LectureciseId");

                    b.Property<string>("StartHour")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("LectureciseId");

                    b.ToTable("WeekTimes");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("LMSApp.Data.Models.AssignmentRelated.Assignment", b =>
                {
                    b.HasOne("LMSApp.Data.Models.UserTypes.Educator", "Educator")
                        .WithMany("AssignmentsGiven")
                        .HasForeignKey("EducatorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LMSApp.Data.Models.CourseRelated.Lecturecise", "Lecturecise")
                        .WithMany()
                        .HasForeignKey("LectureciseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LMSApp.Data.Models.AssignmentRelated.StudentAssignment", b =>
                {
                    b.HasOne("LMSApp.Data.Models.AssignmentRelated.Assignment", "Assignment")
                        .WithMany("StudentsAssignedTo")
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LMSApp.Data.Models.UserTypes.Educator", "Grader")
                        .WithMany("AssignmentsGraded")
                        .HasForeignKey("GraderId");

                    b.HasOne("LMSApp.Data.Models.CourseRelated.Lecturecise", "Lecturecise")
                        .WithMany("StudentTasks")
                        .HasForeignKey("LectureciseId");

                    b.HasOne("LMSApp.Data.Models.UserTypes.Student", "Student")
                        .WithMany("StudentAssignments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("LMSApp.Data.Models.CourseRelated.EducatorCourse", b =>
                {
                    b.HasOne("LMSApp.Data.Models.CourseRelated.Course", "Course")
                        .WithMany("CourseEducators")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LMSApp.Data.Models.UserTypes.Educator", "Educator")
                        .WithMany("Courses")
                        .HasForeignKey("EducatorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LMSApp.Data.Models.CourseRelated.EducatorEvent", b =>
                {
                    b.HasOne("LMSApp.Data.Models.UserTypes.Educator", "Educator")
                        .WithMany("Events")
                        .HasForeignKey("EducatorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LMSApp.Data.Models.CourseRelated.Event", "Event")
                        .WithMany("EducatorEvents")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LMSApp.Data.Models.CourseRelated.EducatorLecturecise", b =>
                {
                    b.HasOne("LMSApp.Data.Models.UserTypes.Educator", "Educator")
                        .WithMany("Lecturecises")
                        .HasForeignKey("EducatorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LMSApp.Data.Models.CourseRelated.Lecturecise", "Lecturecise")
                        .WithMany("LectureciseEducators")
                        .HasForeignKey("LectureciseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LMSApp.Data.Models.CourseRelated.Event", b =>
                {
                    b.HasOne("LMSApp.Data.Models.UserTypes.Group")
                        .WithMany("GroupEvents")
                        .HasForeignKey("GroupId");

                    b.HasOne("LMSApp.Data.Models.CourseRelated.Lecturecise", "Lecturecise")
                        .WithMany("Events")
                        .HasForeignKey("LectureciseId");
                });

            modelBuilder.Entity("LMSApp.Data.Models.CourseRelated.Lecturecise", b =>
                {
                    b.HasOne("LMSApp.Data.Models.CourseRelated.Course", "Course")
                        .WithMany("Lecturecises")
                        .HasForeignKey("CourseId");
                });

            modelBuilder.Entity("LMSApp.Data.Models.CourseRelated.StudentCourse", b =>
                {
                    b.HasOne("LMSApp.Data.Models.CourseRelated.Course", "Course")
                        .WithMany("StudentsInCourse")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LMSApp.Data.Models.UserTypes.Student", "Student")
                        .WithMany("StudentCourses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LMSApp.Data.Models.CourseRelated.StudentEvent", b =>
                {
                    b.HasOne("LMSApp.Data.Models.CourseRelated.Event", "Event")
                        .WithMany("StudentEvents")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LMSApp.Data.Models.UserTypes.Student", "Student")
                        .WithMany("StudentEvents")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LMSApp.Data.Models.CourseRelated.StudentLecturecise", b =>
                {
                    b.HasOne("LMSApp.Data.Models.CourseRelated.Lecturecise", "Lecturecise")
                        .WithMany("LectureciseStudents")
                        .HasForeignKey("LectureciseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LMSApp.Data.Models.UserTypes.Student", "Student")
                        .WithMany("StudentLecturecises")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LMSApp.Data.Models.GradeAwards.Badge", b =>
                {
                    b.HasOne("LMSApp.Data.Models.AssignmentRelated.Assignment", "Assignment")
                        .WithMany()
                        .HasForeignKey("AssignmentId");
                });

            modelBuilder.Entity("LMSApp.Data.Models.GradeAwards.StudentBadge", b =>
                {
                    b.HasOne("LMSApp.Data.Models.GradeAwards.Badge", "Badge")
                        .WithMany("StudentsAwarded")
                        .HasForeignKey("BadgeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LMSApp.Data.Models.UserTypes.Student", "Student")
                        .WithMany("StudentBadges")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LMSApp.Data.Models.MaterialRelated.Material", b =>
                {
                    b.HasOne("LMSApp.Data.Models.AssignmentRelated.Assignment", "Assignment")
                        .WithMany("Materials")
                        .HasForeignKey("AssignmentId");

                    b.HasOne("LMSApp.Data.Models.CourseRelated.Course", "Course")
                        .WithMany("Materials")
                        .HasForeignKey("CourseId");

                    b.HasOne("LMSApp.Data.Models.UserTypes.Educator", "Educator")
                        .WithMany("MaterialsGiven")
                        .HasForeignKey("EducatorId");

                    b.HasOne("LMSApp.Data.Models.CourseRelated.Event", "Event")
                        .WithMany("Materials")
                        .HasForeignKey("EventId");
                });

            modelBuilder.Entity("LMSApp.Data.Models.MaterialRelated.StudentMaterial", b =>
                {
                    b.HasOne("LMSApp.Data.Models.MaterialRelated.Material", "Material")
                        .WithMany("StudentMaterials")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LMSApp.Data.Models.UserTypes.Student", "Student")
                        .WithMany("StudentMaterials")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LMSApp.Data.Models.UserTypes.Educator", b =>
                {
                    b.HasOne("LMSApp.Data.Models.LMSAppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LMSApp.Data.Models.UserTypes.Group", b =>
                {
                    b.HasOne("LMSApp.Data.Models.AssignmentRelated.Assignment")
                        .WithMany("GroupsAssignedTo")
                        .HasForeignKey("AssignmentId");
                });

            modelBuilder.Entity("LMSApp.Data.Models.UserTypes.Student", b =>
                {
                    b.HasOne("LMSApp.Data.Models.UserTypes.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId");

                    b.HasOne("LMSApp.Data.Models.LMSAppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LMSApp.Data.Models.WeekTime", b =>
                {
                    b.HasOne("LMSApp.Data.Models.CourseRelated.Lecturecise")
                        .WithMany("WeekTimes")
                        .HasForeignKey("LectureciseId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("LMSApp.Data.Models.LMSAppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("LMSApp.Data.Models.LMSAppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LMSApp.Data.Models.LMSAppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("LMSApp.Data.Models.LMSAppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
