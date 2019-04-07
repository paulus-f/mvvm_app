using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_students.Models
{
    public class TaughtGroups
    {
        public int Id { get; set; }
        [ForeignKey("Subject")]
        public int? SubjectId { get; set; }
        [ForeignKey("Group")]
        public int? GroupId { get; set; }
        [ForeignKey("Teaching")]
        public int? TeachingId { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Group Group { get; set; }
        public virtual Teaching Teaching { get; set; }

        public override string ToString()
        {
            return Subject.Name + " " + Group.ToString();
        }
    }
}
