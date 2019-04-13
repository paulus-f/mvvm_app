using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace University_students.Models
{
    public class USDbContext : DbContext
    {
        public USDbContext() : base("DefaultConnection") { }
        public DbSet<User> Users { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Pulpit> Pulpits { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Сertification> Сertifications { get; set; }
        public DbSet<Teaching> Teachings { get; set; }
        public DbSet<TaughtGroups> TaughtGroups { get; set; }
        public DbSet<SubjectProgress> SubjectProgress { get; set; }
        public DbSet<WorkOut> WorkOuts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
              .HasOptional<Pulpit>(e => e.Pulpit)
              .WithMany(s => s.Teachers)
              .HasForeignKey(e => e.PulpitId)
              .WillCascadeOnDelete(false);
        }
    }
}
