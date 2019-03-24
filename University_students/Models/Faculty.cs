using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University_students.Models
{
    public class Faculty
    {
        public int Id { get; set; }
        [StringLength(450)]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        public int? UniversityId { get; set; }
        public virtual University University { get; set; }
        public virtual ICollection<Speciality> Specialites { get; set; }
        public Faculty()
        {
            Specialites = new List<Speciality>();
        }
    }
}
