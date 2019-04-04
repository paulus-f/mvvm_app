using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University_students.Models
{
    public class Group
    {
        public int Id { get; set; }
        [Required]
        public int NumberGroup { get; set; }
        [Required]
        public int FirstYear { get; set; }
        [Required]
        [ForeignKey("Speciality")]
        public int? SpecialityId { get; set; }
        public virtual Speciality Speciality { get; set; }
        public virtual ICollection<User> Teachers { get; set; }
        public virtual ICollection<User> Students { get; set; }
        public Group()
        {
            Teachers = new List<User>();
            Students = new List<User>();
        }

    }
}
