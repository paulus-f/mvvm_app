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
    class CertificationVM : ViewModelBase, INotifyPropertyChanged
    {
        USDbContext db;
        Enums.TypeCertifiation _tc;
        public CertificationVM(User teacher, Enums.TypeCertifiation tc)
        {
            db = new USDbContext();
            _tc = tc;
            ListGroups = teacher.Teaching.TaughtGroups.ToList();
            switch (tc)
            {
                case Enums.TypeCertifiation.FirstHalfFinish:
                    TypeCertification = "The second autumn certification";
                    break;
                case Enums.TypeCertifiation.FirstHalfStart:
                    TypeCertification = "The first autumn certification";
                    break;
                case Enums.TypeCertifiation.SecondHalfFinish:
                    TypeCertification = "The second spring certification";
                    break;
                case Enums.TypeCertifiation.SecondHalfStart:
                    TypeCertification = "The first spring certification";
                    break;
            }
        }

        private string _TypeCertifiation;
        public string TypeCertification
        {
            get => _TypeCertifiation;
            set
            {
                _TypeCertifiation = value;
                OnPropertyChanged("TypeCertifiation");
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


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
