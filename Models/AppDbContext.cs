using Microsoft.EntityFrameworkCore;
using CareerConnect.Models;

namespace CareerConnect.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> optionsBuilder) : base(optionsBuilder) { }

/*(hardcoded)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=SHAHIN\\SQLEXPRESS;Database=CareerConnectDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Salary precision for Job
            modelBuilder.Entity<Job>()
                .Property(j => j.Salary)
                .HasPrecision(18, 2);

            // Application Job (Many-to-One)
            modelBuilder.Entity<Application>()
                .HasOne(a => a.Job)
                .WithMany(j => j.Applications)
                .HasForeignKey(a => a.JobId)
                .OnDelete(DeleteBehavior.Restrict);

            // Application JobSeeker (Many-to-One)
            modelBuilder.Entity<Application>()
                .HasOne(a => a.JobSeeker)
                .WithMany(js => js.Applications)
                .HasForeignKey(a => a.JobSeekerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Resume  JobSeeker (One-to-One)
            modelBuilder.Entity<Resume>()
                .HasOne(r => r.JobSeeker)
                .WithOne(js => js.Resume)
                .HasForeignKey<Resume>(r => r.JobSeekerId)
                .OnDelete(DeleteBehavior.Cascade);

            // AuditLog  User (Many-to-One)
            modelBuilder.Entity<AuditLog>()
                .HasOne(al => al.User)
                .WithMany()
                .HasForeignKey(al => al.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //Notification   User(Many-to-One)
            modelBuilder.Entity<Notification>()
            .HasOne(n => n.User)
            .WithMany()
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            //softdeletion filter
            modelBuilder.Entity<Resume>().HasQueryFilter(r => !r.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }
     

        public DbSet<User> Users { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<JobSeeker> JobSeekers { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Notification> Notifications { get; set; }

    }
}
