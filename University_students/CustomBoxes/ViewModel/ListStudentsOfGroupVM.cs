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

        public ListStudentsOfGroupVM(Group gr)
        {
            db = new USDbContext();
            Group = gr;
            ListStudents = gr?.Students.ToList();
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

        public ICommand AddPassCommand
        {
            get
            {
                return new RelayCommand<object>(
                    (param) => CanAddPassToStudent((User)param)
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

        private void CanAddPassToStudent(User st)
        {

        }

        private void CanAddWorOutToStudent(User st)
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
