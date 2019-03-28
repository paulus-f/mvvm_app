using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using University_students.Enums;

namespace University_students.Models
{
    public class User
    {
        public int Id { get; set; }
        [StringLength(450)]
        [Index(IsUnique = true)]
        public string Login { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        public Role TypeUser { get; set; }
        public virtual IList<TeacherSubject> Subjects { get; set; }
    }

}
