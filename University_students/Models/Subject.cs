
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University_students.Models
{
    public class Subject
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Hour { get; set; }
        public virtual ICollection<User> Teachers { get; set; }
        public Subject()
        {
            Teachers = new List<User>();
        }
    }
}