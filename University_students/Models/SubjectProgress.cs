using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University_students.Models
{
    public class SubjectProgress
    {
        public int Id { get; set; }
        [Required]
        [ForeignKey("User")]
        public int? UserId { get; set; }
        public virtual User User { get; set; }
        [Required]
        [ForeignKey("TaughtGroups")]
        public int? TaughtGroupsId { get; set; }
        public virtual TaughtGroups TaughtGroups { get; set; }

        public bool IsOffsetPassed { get; set; }
        public bool IsExamPassed { get; set; }
        public bool IsStartCertifiationPassed { get; set; }
        public bool IsFinishCertifiationPassed { get; set; }
        public int ValidExcuses { get; set; }
        public int UnValidExcuses { get; set; }

        public virtual ICollection<WorkOut> WorkOuts { get; set; }
        public SubjectProgress()
        {
            WorkOuts = new List<WorkOut>();
        }
    }
}
