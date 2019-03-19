using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University_students.Models
{
    public class University
    {
        public int Id { get; set; }
        [StringLength(450)]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string TypeUniversity { get; set; }
        public virtual ICollection<Faculty> Faculties { get; set; }
        public University()
        {
            Faculties = new List<Faculty>();
        }
    }
}
