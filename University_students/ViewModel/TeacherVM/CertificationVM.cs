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
        Enums.TypeCertifiation _tc;
        public CertificationVM(User teacher, Enums.TypeCertifiation tc)
        {
            db = new USDbContext();
            _tc = tc;
            //_currentTeacher = teacher;
        }

        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
