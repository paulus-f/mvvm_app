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

        private List<SubjectProgress> _ListProgressStudents;
        public List<SubjectProgress> ListProgressStudents
        {
            get => _ListProgressStudents;
            set
            {
                _ListProgressStudents = value;
                OnPropertyChanged("ListProgressStudents");
            }
        }

        public ICommand AddFailCertificationCommand
        {
            get
            {
                return new RelayCommand<object>(
                    (param) => CanAddFailCertification((SubjectProgress)param)
                );
            }
        }

        private void CanAddFailCertification(SubjectProgress stProgress)
        {
            var firstSP = db.SubjectProgress.FirstOrDefault(sp => sp.Id == stProgress.Id);
            switch(_tc)
            {
                case Enums.TypeCertifiation.FirstHalfFinish:
                case Enums.TypeCertifiation.SecondHalfFinish:
                    firstSP.IsFinishCertifiationPassed = Enums.StateCertification.Failed;
                    break;
                case Enums.TypeCertifiation.FirstHalfStart:
                case Enums.TypeCertifiation.SecondHalfStart:
                    firstSP.IsStartCertifiationPassed = Enums.StateCertification.Failed;
                    break;
            }
            db.SaveChanges();
            ListProgressStudents = db.SubjectProgress
                .Where(sp => sp.TaughtGroups.Id == SelectedGroup.Id)
                .ToList();
        }

        public ICommand AddSuccessCertificationCommand
        {
            get
            {
                return new RelayCommand<object>(
                    (param) => CanAddSuccessCertification((SubjectProgress)param)
                );
            }
        }

        private void CanAddSuccessCertification(SubjectProgress stProgress)
        {
            var firstSP = db.SubjectProgress.FirstOrDefault(sp => sp.Id == stProgress.Id);
            switch (_tc)
            {
                case Enums.TypeCertifiation.FirstHalfFinish:
                case Enums.TypeCertifiation.SecondHalfFinish:
                    firstSP.IsFinishCertifiationPassed = Enums.StateCertification.Passed;
                    break;
                case Enums.TypeCertifiation.FirstHalfStart:
                case Enums.TypeCertifiation.SecondHalfStart:
                    firstSP.IsStartCertifiationPassed = Enums.StateCertification.Passed;
                    break;
            }
            db.SaveChanges();
            ListProgressStudents = db.SubjectProgress
                .Where(sp => sp.TaughtGroups.Id == SelectedGroup.Id)
                .ToList();
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

        private SubjectProgress _SelectedProgressStudent;
        public SubjectProgress SelectedProgressStudent
        {
            get => _SelectedProgressStudent;
            set
            {
                _SelectedProgressStudent = value;
                OnPropertyChanged("SelectedProgressStudent");
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
                if (value != null)
                {
                    ListProgressStudents = db.SubjectProgress
                        .Where(sp => sp.TaughtGroups.Id == value.Id)
                        .ToList();
                }
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
