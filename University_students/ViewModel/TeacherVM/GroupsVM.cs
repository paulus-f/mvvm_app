using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using University_students.Models;

namespace University_students.ViewModel.TeacherVM
{
    class GroupsVM : ViewModelBase, INotifyPropertyChanged
    {
        USDbContext db;
        public GroupsVM(User teacher)
        {
            db = new USDbContext();
            CurrentTeacher = teacher;
            ListSubjects = teacher.Subjects.ToList();
            ListGroups = teacher?.Teaching?.TaughtGroups.ToList();
        }

        private List<Subject> _ListSubjects;
        public List<Subject> ListSubjects
        {
            get => _ListSubjects;
            set
            {
                _ListSubjects = value;
                OnPropertyChanged("ListSubjects");
            }
        }

        private User _currentTeacher;
        public User CurrentTeacher
        {
            get => _currentTeacher;
            set
            {
                _currentTeacher = value;
                OnPropertyChanged("CurrentTeacher");
            }
        }

        private TaughtGroups _SelectedGroup;
        public TaughtGroups SelectedGroup
        {
            get => _SelectedGroup;
            set
            {
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
