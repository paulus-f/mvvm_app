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
    class SubjectsVM
    {
        USDbContext db;
        public SubjectsVM(User teacher)
        {
            db = new USDbContext();
            _currentTeacher = teacher;
            ListSubjects = teacher.Subjects.ToList();
        }

        private User _currentTeacher;

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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
