using GalaSoft.MvvmLight;
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

namespace University_students.ViewModel.TeacherVM
{
    class AccreditationVM : ViewModelBase, INotifyPropertyChanged
    {
        USDbContext db;
        public AccreditationVM(User teacher)
        {
            db = new USDbContext();
            _currentTeacher = teacher;
            ListGroups = teacher?.Teaching?.TaughtGroups.ToList();
        }

        private User _currentTeacher;

        private TaughtGroups _SelectedGroup;
        public TaughtGroups SelectedGroup
        {
            get => _SelectedGroup;
            set
            {
                db = new USDbContext();
                ListStudentProgress = db.SubjectProgress
                    .Where(sp => sp.TaughtGroups.Id == value.Id && sp.IsOffsetPassed == false)
                    .ToList();
                ListStudentProgressExam = db.SubjectProgress
                    .Where(sp => sp.TaughtGroups.Id == value.Id && sp.IsOffsetPassed == true)
                    .ToList();
                _SelectedGroup = value;
                IsSelected = true;
                OnPropertyChanged("SelectedGroup");
            }
        }

        private List<TaughtGroups> _ListGroups;
        public List<TaughtGroups> ListGroups
        {
            get => _ListGroups;
            set
            {
                _ListGroups = value;
                OnPropertyChanged("ListGroups");
            }
        }

        private List<SubjectProgress> _ListStudentProgressExam;
        public List<SubjectProgress> ListStudentProgressExam
        {
            get => _ListStudentProgressExam;
            set
            {
                _ListStudentProgressExam = value;
                OnPropertyChanged("ListStudentProgressExam");
            }
        }

        private List<SubjectProgress> _ListStudentProgress;
        public List<SubjectProgress> ListStudentProgress
        {
            get => _ListStudentProgress;
            set
            {
                _ListStudentProgress = value;
                OnPropertyChanged("ListStudentProgress");
            }
        }

        private SubjectProgress _SelectedStudent;
        public SubjectProgress SelectedStudent
        {
            get => _SelectedStudent;
            set
            {
                _SelectedStudent = value;
                IsSelectedOffset = true;
                OnPropertyChanged("SelectedStudent");
            }
        }

        private SubjectProgress _SelectedStudentExam;
        public SubjectProgress SelectedStudentExam
        {
            get => _SelectedStudentExam;
            set
            {
                _SelectedStudentExam = value;
                IsSelectedExam = true;
                OnPropertyChanged("SelectedStudentExam");
            }
        }

        private bool _IsSelectedOffset;
        public bool IsSelectedOffset
        {
            get => _IsSelectedOffset;
            set
            {
                _IsSelectedOffset = value;
                OnPropertyChanged("IsSelectedOffset");
            }
        }

        private bool _IsSelectedExam;
        public bool IsSelectedExam
        {
            get => _IsSelectedExam;
            set
            {
                _IsSelectedExam = value;
                OnPropertyChanged("IsSelectedExam");
            }
        }

        private bool _IsSelected;
        public bool IsSelected
        {
            get => _IsSelected;
            set
            {
                _IsSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public ICommand AddFailExamCommand
        {
            get
            {
                return new RelayCommand(
                    () => CanAddFailExam()
                );
            }
        }

        public ICommand RetakeExamCommand
        {
            get
            {
                return new RelayCommand(
                    () => CanRetakeExam()
                );
            }
        }

        public ICommand CreateReportCommand
        {
            get
            {
                return new RelayCommand(
                    () => CanCreateReport()
                );
            }
        }

        public ICommand AdmitingToExamCommand
        {
            get
            {
                return new RelayCommand(
                    () => CanAdmitingToExam()
                );
            }
        }

        public ICommand MarkExamPassedCommand
        {
            get
            {
                return new RelayCommand(
                    () => CanMarkExamPassed()
                );
            }
        }

        private void CanMarkExamPassed()
        {
            var subjectProgress = db.SubjectProgress.FirstOrDefault(sp => sp.Id == SelectedStudentExam.Id);
            subjectProgress.IsExamPassed = Enums.StateExam.Passed;
            UpdateGroup();
        }

        private void CanAddFailExam()
        {
            var subjectProgress = db.SubjectProgress.FirstOrDefault(sp => sp.Id == SelectedStudentExam.Id);
            subjectProgress.IsExamPassed = Enums.StateExam.Failed;
            UpdateGroup();
        }

        private void CanRetakeExam()
        {
            var subjectProgress = db.SubjectProgress.FirstOrDefault(sp => sp.Id == SelectedStudentExam.Id);
            subjectProgress.IsExamPassed = Enums.StateExam.Retake;
            UpdateGroup();
        }

        private void CanCreateReport()
        {
            var tg = db.TaughtGroups.FirstOrDefault(t => t.Id == SelectedGroup.Id);
            Services.TeacherDocs.CreateReport(tg);
        }

        private void CanAdmitingToExam()
        {
            var firstSP = db.SubjectProgress.FirstOrDefault(sp => sp.Id == SelectedStudent.Id);
            firstSP.IsOffsetPassed = true;
            db.SaveChanges();
            UpdateGroup();
        }

        private void UpdateGroup()
        {
            ListStudentProgress = db.SubjectProgress
               .Where(sp => sp.TaughtGroups.Id == SelectedGroup.Id && sp.IsOffsetPassed == false)
               .ToList();
            ListStudentProgressExam = db.SubjectProgress
               .Where(sp => sp.TaughtGroups.Id == SelectedGroup.Id && sp.IsOffsetPassed == true)
               .ToList();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
