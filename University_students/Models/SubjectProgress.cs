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
        public Enums.StateExam IsExamPassed { get; set; }
        public Enums.StateCertification IsStartCertifiationPassed { get; set; }
        public Enums.StateCertification IsFinishCertifiationPassed { get; set; }
        public int ValidExcuses { get; set; }
        public int UnValidExcuses { get; set; }

        public virtual ICollection<WorkOut> WorkOuts { get; set; }
        public SubjectProgress()
        {
            WorkOuts = new List<WorkOut>();
        }

        public string ToResultExam()
        {
            string res = "";
            switch (IsExamPassed)
            {
                case Enums.StateExam.Waiting:
                    res += "Exam: Waiting ";
                    break;
                case Enums.StateExam.Passed:
                    res += "Exam: Passed ";
                    break;
                case Enums.StateExam.Failed:
                    res += "Exam: Failed ";
                    break;
            }
            return res;
        }

        public string ToResultCertifications()
        {
            string res = " --- Start Certifiation --- ";
            switch (IsStartCertifiationPassed)
            {
                case Enums.StateCertification.Failed:
                    res += "Certification: Failed ";
                    break;
                case Enums.StateCertification.Passed:
                    res += "Certification: Passed ";
                    break;
                case Enums.StateCertification.Waiting:
                    res += "Certification: Waiting ";
                    break;
            }
            res += " --- Finish Certifiation --- ";
            switch (IsFinishCertifiationPassed)
            {
                case Enums.StateCertification.Failed:
                    res += "Certification: Failed";
                    break;
                case Enums.StateCertification.Passed:
                    res += "Certification: Passed ";
                    break;
                case Enums.StateCertification.Waiting:
                    res += "Certification: Waiting ";
                    break;
            }
            return res;
        }

        public override string ToString()
        {
            string res = "";
            switch(IsExamPassed)
            {
                case Enums.StateExam.Waiting:
                    res += "Exam: Waiting ";
                    break;
                case Enums.StateExam.Passed:
                    res += "Exam: Passed ";
                    break;
                case Enums.StateExam.Failed:
                    res += "Exam: Failed ";
                    break;
                case Enums.StateExam.Retake:
                    res += "Exam: Retake ";
                    break;
            }

            switch (IsStartCertifiationPassed)
            {
                case Enums.StateCertification.Failed:
                    res += "1st cert.: Failed ";
                    break;
                case Enums.StateCertification.Passed:
                    res += "1st cert.: Passed ";
                    break;
                case Enums.StateCertification.Waiting:
                    res += "1st cert.: Waiting ";
                    break;
            }

            switch (IsFinishCertifiationPassed)
            {
                case Enums.StateCertification.Failed:
                    res += "1st cert.: Failed ";
                    break;
                case Enums.StateCertification.Passed:
                    res += "1st cert.: Passed";
                    break;
                case Enums.StateCertification.Waiting:
                    res += "1st cert.: Waiting ";
                    break;
            }
            return res;
        }
    }
}
