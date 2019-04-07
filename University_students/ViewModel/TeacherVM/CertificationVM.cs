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
    class CertificationVM
    {
        USDbContext db;
        public CertificationVM(User teacher)
        {
            db = new USDbContext();
            _currentTeacher = teacher;
        }

        private User _currentTeacher;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
