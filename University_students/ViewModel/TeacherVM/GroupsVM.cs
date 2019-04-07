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
            _currentTeacher = teacher;
            ListGroups = teacher.Teaching.TaughtGroups.ToList();
        }

        private User _currentTeacher;

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
