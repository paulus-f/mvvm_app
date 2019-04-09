using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_students.Models
{
    public class WorkOut
    {
        public int Id { get; set; }
        public bool IsWorkOut { get; set; }
        public int? SubjectProgressId { get; set; }
        [StringLength(100)]
        public string Reason { get; set; }
        public virtual SubjectProgress SubjectProgress { get; set; }
    }
}