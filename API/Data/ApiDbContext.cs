using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Secretary> Secretaries { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ExamRequest> ExamRequests { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Modification> Modifications { get; set; }

        public DbSet<Session> Sessions { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<LabHolders>LabHolders { get; set; }
        public DbSet<ExamRequestRoom> ExamRequestRooms { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Student>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Group)
                .WithMany()
                .HasForeignKey(s => s.GroupID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Professor)
                .WithMany()
                .HasForeignKey(c => c.ProfID)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Course>()
        .HasOne(c => c.Specialization)
        .WithMany()
        .HasForeignKey(c => c.SpecializationID)
        .OnDelete(DeleteBehavior.NoAction);

           
            modelBuilder.Entity<Professor>()
            .HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserID)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Professor>()
                .HasOne(p => p.Department)
                .WithMany()
                .HasForeignKey(p => p.DepartmentID)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Group>()
            .HasOne(g => g.Specialization)
            .WithMany()
            .HasForeignKey(g => g.SpecializationID)
            .OnDelete(DeleteBehavior.Cascade); 

           
            modelBuilder.Entity<ExamRequest>()
        .HasOne(er => er.Course)
        .WithMany()
        .HasForeignKey(er => er.CourseID)
        .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<ExamRequest>()
                .HasOne(er => er.Group)
                .WithMany()
                .HasForeignKey(er => er.GroupID)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<ExamRequest>()
                .HasOne(er => er.Assistant)
                .WithMany()
                .HasForeignKey(er => er.AssistantID)
                .OnDelete(DeleteBehavior.NoAction); 



            modelBuilder.Entity<ExamRequest>()
                .HasOne(er => er.Session)
                .WithMany()
                .HasForeignKey(er => er.SessionID)
                .OnDelete(DeleteBehavior.NoAction);
          
            modelBuilder.Entity<LabHolders>()
            .HasOne(lh => lh.Professor)
            .WithMany()
            .HasForeignKey(lh => lh.ProfID)
            .OnDelete(DeleteBehavior.Restrict);  
            modelBuilder.Entity<ExamRequestRoom>()
       .HasOne(er => er.ExamRequest)
       .WithMany(er => er.ExamRequestRooms)
       .HasForeignKey(er => er.ExamRequestID);

            modelBuilder.Entity<ExamRequestRoom>()
                .HasOne(er => er.Room)
                .WithMany(r => r.ExamRequestRooms)
                .HasForeignKey(er => er.RoomID);


            base.OnModelCreating(modelBuilder);
        }
    }
}
