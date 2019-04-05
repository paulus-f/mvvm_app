using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University_students.Models
{
    public class Faculty
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Dean { get; set; }
        [Required]
        [ForeignKey("University")]
        public int? UniversityId { get; set; }
        public virtual University University { get; set; }
        public virtual ICollection<Speciality> Specialites { get; set; }
        public virtual ICollection<Pulpit> Pulpits { get; set; }
        public Faculty()
        {
            Specialites = new List<Speciality>();
            Pulpits = new List<Pulpit>();
        }
    }
}
