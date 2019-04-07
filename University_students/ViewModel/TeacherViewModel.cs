using System;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows;
using System.Runtime.CompilerServices;
using GalaSoft.MvvmLight.Messaging;
using University_students.Messager;
using University_students.Models;

namespace University_students.ViewModel
{
    public class TeacherViewModel : ViewModelBase, INotifyPropertyChanged
    {

        public TeacherViewModel()
        {
            Messenger.Default.Register<SendCurrentUserMessage>(this, (action) => ReceiveMessage(action));
        }

        private object ReceiveMessage(SendCurrentUserMessage action)
        {
            CurrentUser = action.CurrentUser;
            GroupsContent = new View.TeacherPages.GroupsContent(_currentUser);
            ExamsContent = new View.TeacherPages.ExamContent(_currentUser);
            ReportsContent = new View.TeacherPages.ReportsContent(_currentUser);
            SubjectsContent = new View.TeacherPages.SubjectsContent(_currentUser);
            Models.Сertification certification = CurrentUser.Pulpit.Faculty.University.Сertification;
            if(certification.FirstAutumnStartDate >= DateTime.Today &&
                certification.FirstAutumnEndDate <= DateTime.Today )
            {
                CertificationContent = new View.TeacherPages.CertificationContent(_currentUser, Enums.TypeCertifiation.FirstHalfStart);
            }
            else if(certification.FirstSpringStartDate >= DateTime.Today &&
                certification.FirstSpringEndDate <= DateTime.Today)
            {
                CertificationContent = new View.TeacherPages.CertificationContent(_currentUser, Enums.TypeCertifiation.SecondHalfStart);
            }
            else if(certification.LastAutumnStartDate>= DateTime.Today &&
                certification.LastAutumnEndDate<= DateTime.Today)
            {
                CertificationContent = new View.TeacherPages.CertificationContent(_currentUser, Enums.TypeCertifiation.FirstHalfFinish);
            }
            else if(certification.LastSpringStartDate >= DateTime.Today &&
                certification.LastSpringEndDate <= DateTime.Today)
            {
                CertificationContent = new View.TeacherPages.CertificationContent(_currentUser, Enums.TypeCertifiation.SecondHalfFinish);
            }
            else
            {
                CertificationContent = new View.TeacherPages.NoCertification(_currentUser);
            }
            return null;
        }

        private User _currentUser;
        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged("CurrentUser");
            }
        }

        private UserControl _GroupsContent;
        public UserControl GroupsContent
        {
            get => _GroupsContent;
            set
            {
                _GroupsContent = value;
                OnPropertyChanged("GroupsContent");
            }
        }

        private UserControl _CertificationContent;
        public UserControl CertificationContent
        {
            get => _CertificationContent;
            set
            {
                _CertificationContent = value;
                OnPropertyChanged("CertificationContent");
            }
        }

        private UserControl _SubjectsContent;
        public UserControl SubjectsContent
        {
            get => _SubjectsContent;
            set
            {
                _SubjectsContent = value;
                OnPropertyChanged("SubjectsContent");
            }
        }

        private UserControl _ReportsContent;
        public UserControl ReportsContent
        {
            get => _ReportsContent;
            set
            {
                _ReportsContent = value;
                OnPropertyChanged("ReportsContent");
            }
        }

        private UserControl _ExamsContent;
        public UserControl ExamsContent
        {
            get => _ExamsContent;
            set
            {
                _ExamsContent = value;
                OnPropertyChanged("ExamsContent");
            }
        }

        public ICommand LogOutCommand
        {
            get
            {
                return new RelayCommand<object>(
                    (param) => CanLogOut()

                );
            }
        }
        void CanLogOut()
        {
            var msg = new LogOutMessage() { Message = "LogOut" };
            Messenger.Default.Send<LogOutMessage>(msg);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
