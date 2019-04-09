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

namespace University_students.CustomBoxes.ViewModel
{
    class BackgroundStudentVM : ViewModelBase, INotifyPropertyChanged
    {
        private USDbContext db;
        private int teacherUniversity;

        private User _CurrentStudent;
        public User CurrentStudent
        {
            get => _CurrentStudent;
            set
            {
                _CurrentStudent = value;
                OnPropertyChanged("CurrentStudent");
            }
        }

        private SubjectProgress _SubjectProgress;
        public SubjectProgress SubjectProgress
        {
            get => _SubjectProgress;
            set
            {
                _SubjectProgress = value;
                OnPropertyChanged("SubjectProgress");
            }
        }

        private List<WorkOut> _ListWorkOut;
        public List<WorkOut> ListWorkOut
        {
            get => _ListWorkOut;
            set
            {
                _ListWorkOut = value;
                OnPropertyChanged("ListWorkOut");
            }
        }

        private WorkOut _SelecredWorkOut;
        public WorkOut SelecredWorkOut
        {
            get => _SelecredWorkOut;
            set
            {
                _SelecredWorkOut = value;
                OnPropertyChanged("SelecredWorkOut");
            }
        }

        public BackgroundStudentVM(User student,  SubjectProgress sp)
        {
            db = new USDbContext();
            SubjectProgress = sp;
            ListWorkOut = sp.WorkOuts.ToList();
            CurrentStudent = student;
        }

        public ICommand ConfirmWorkOutCommand
        {
            get
            {
                return new RelayCommand<object>(
                    (param) => CanConfirmWorkOut((WorkOut)param)
                );
            }
        }

        private void CanConfirmWorkOut(WorkOut wo)
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
