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
                ListStudentProgress = db.SubjectProgress
                    .Where(sp => sp.TaughtGroups.Id == value.Id && sp.IsOffsetPassed == false)
                    .ToList();
                ListStudentProgressExam = db.SubjectProgress
                    .Where(sp => sp.TaughtGroups.Id == value.Id && sp.IsOffsetPassed == true)
                    .ToList();
                _SelectedGroup = value;
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

        public ICommand AdmitingToExamCommand
        {
            get
            {
                return new RelayCommand(
                    () => CanAdmitingToExam()
                );
            }
        }
        
        private void CanAdmitingToExam()
        {
            var firstSP = db.SubjectProgress.FirstOrDefault(sp => sp.Id == SelectedStudent.Id);
            firstSP.IsOffsetPassed = true;
            db.SaveChanges();
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
