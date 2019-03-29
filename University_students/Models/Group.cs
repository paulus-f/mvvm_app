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
        public int? SpecialityId { get; set; }
        public virtual Speciality Speciality { get; set; }
        public virtual ICollection<User> Students { get; set; }
        public Group()
        {
            Students = new List<User>();
        }

    }
}
