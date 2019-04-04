using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University_students.Models
{
    public class Pulpit
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [ForeignKey("Faculty")]
        public int? FacultyId { get; set; }
        public virtual Faculty Faculty { get; set; }
        public virtual ICollection<User> Teachers { get; set; }
        public Pulpit()
        {
            Teachers = new List<User>();
        }
    }
}
