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
        //public DbSet<TeacherSubject> TeacherSubjects { get; set; }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<TeacherSubject>().HasKey(sc => new { sc.UserId, sc.SubjectId});
        //}
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>()
        //        .HasMany(c => c.Subjects)
        //        .WithMany(p => p.Teachers)
        //        .Map(m =>
        //        {
        //    // Ссылка на промежуточную таблицу
        //    m.ToTable("TeacherSubjects");

        //    // Настройка внешних ключей промежуточной таблицы
        //    m.MapLeftKey("UserId");
        //            m.MapRightKey("SubjectId");
        //        });
        //}
    }
}
