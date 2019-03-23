using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace University_students.Models
{
    public class Speciality
    {
        public int Id { get; set; }
        [StringLength(450)]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Qualification { get; set; }
        public int? FacultyId { get; set; }
        public virtual Faculty Faculty { get; set; }
    }
}
