using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_students.Models
{
    public class Teaching
    {
        [ForeignKey("User")]
        [Key]
        public int Id { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<TaughtGroups> TaughtGroups { get; set; }
        public Teaching()
        {
            TaughtGroups = new List<TaughtGroups>();
        }

    }
}
