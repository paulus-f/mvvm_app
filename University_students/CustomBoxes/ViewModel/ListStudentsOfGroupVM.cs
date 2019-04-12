using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using University_students.Models;

namespace University_students.CustomBoxes.ViewModel
{
    class ListStudentsOfGroupVM
    {
        private USDbContext db;
        private TaughtGroups _tg;

        public ListStudentsOfGroupVM(TaughtGroups gr)
        {
            db = new USDbContext();
            _tg = gr;
            Group = gr.Group;
            Subject = gr.Subject;
            ListStudents = gr?.Group?.Students.ToList();
        }

        private User _SelectedStudent;
        public User SelectedStudent
        {
            get => _SelectedStudent;
            set
            {
                _SelectedStudent = value;
                OnPropertyChanged("SelectedStudent");
            }
        }

        private Group _Group;
        public Group Group
        {
            get => _Group;
            set
            {
                _Group = value;
                OnPropertyChanged("Group");
            }
        }

        private Subject _Subject;
        public Subject Subject
        {
            get => _Subject;
            set
            {
                _Subject = value;
                OnPropertyChanged("Subject");
            }
        }

        private string _Reason;
        public string Reason
        {
            get => _Reason;
            set
            {
                _Reason = value;
                OnPropertyChanged("Reason");
            }
        }

        private List<User> _ListStudents;
        public List<User> ListStudents
        {
            get => _ListStudents;
            set
            {
                _ListStudents = value;
                OnPropertyChanged("ListStudents");
            }
        }

        public ICommand AddUnValidPassCommand
        {
            get
            {
                return new RelayCommand<object>(
                    (param) => CanAddUnValidExcusesToStudent((User)param)
                );
            }
        }

        public ICommand AddValidPassCommand
        {
            get
            {
                return new RelayCommand<object>(
                    (param) => CanAddValidExcusesToStudent((User)param)
                );
            }
        }

        public ICommand AddWorOutCommand
        {
            get
            {
                return new RelayCommand<object>(
                    (param) => CanAddWorOutToStudent((User)param)
                );
            }
        }

        private void CanAddValidExcusesToStudent(User st)
        {
            var result = CheckProgress(_tg.Subject, st);
            db.SubjectProgress.FirstOrDefault(sp => sp.Id == result.Id).ValidExcuses += 2;
            db.SaveChanges();
        }

        private void CanAddUnValidExcusesToStudent(User st)
        {
            var result = CheckProgress(_tg.Subject, st);
            db.SubjectProgress.FirstOrDefault(sp => sp.Id == result.Id).UnValidExcuses += 2;
            db.SaveChanges();
        }

        private void CanAddWorOutToStudent(User st)
        {
            var result = CheckProgress(_tg.Subject, st);
            var subjectProgress = db.SubjectProgress.FirstOrDefault(sp => sp.Id == result.Id);
            db.WorkOuts.Add(new WorkOut()
            {
                IsWorkOut = false,
                Reason = this.Reason,
                SubjectProgress = subjectProgress
            });  
            db.SaveChanges();
        }

        private SubjectProgress CheckProgress(Subject sub, User st)
        {
            var subRes = db.SubjectProgress.FirstOrDefault(subPro => subPro.UserId == st.Id && subPro.TaughtGroupsId == _tg.Id);
            if(subRes == null)
            {
                var student = db.Users.FirstOrDefault(u => u.Id == st.Id);
                var tg = db.TaughtGroups.FirstOrDefault(u => u.Id == _tg.Id);
                var newSP = new SubjectProgress()
                {
                    User = student,
                    UnValidExcuses = 0,
                    ValidExcuses = 0,
                    TaughtGroups = tg,
                    IsExamPassed = Enums.StateExam.Waiting,
                    IsStartCertifiationPassed = Enums.StateCertification.Waiting,
                    IsFinishCertifiationPassed = Enums.StateCertification.Waiting,
                };
                db.SubjectProgress.Add(newSP);
                db.SaveChanges();
                ListStudents = db.Groups.FirstOrDefault(gr => gr.Id == Group.Id).Students.ToList();
                SelectedStudent = db.Users.FirstOrDefault(stud => stud.Id == student.Id);

                return newSP;
            }
            return subRes;
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
