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
    }
}
