using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_students.Models
{
    public class Сertification
    {
        [ForeignKey("University")]
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime FirstAutumnStartDate { get; set; }
        [Required]
        [Column(TypeName = "Date")]
        public DateTime FirstAutumnEndDate { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime LastAutumnStartDate { get; set; }
        [Required]
        [Column(TypeName = "Date")]
        public DateTime LastAutumnEndDate { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime FirstSpringStartDate { get; set; }
        [Required]
        [Column(TypeName = "Date")]
        public DateTime FirstSpringEndDate { get; set; }
        [Required]
        [Column(TypeName = "Date")]
        public DateTime LastSpringStartDate { get; set; }
        [Required]
        [Column(TypeName = "Date")]
        public DateTime LastSpringEndDate { get; set; }

        public virtual University University { get; set; }
    }
}
