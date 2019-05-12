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
using University_students.Models;
using University_students.Messager;
using GalaSoft.MvvmLight.Messaging;

namespace University_students.ViewModel
{
    public class UserViewModel : ViewModelBase, INotifyPropertyChanged
    {

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

        public UserViewModel()
        {
            Messenger.Default.Register<SendCurrentUserMessage>(this, (action) => ReceiveMessage(action));
        }

        private bool _IsSelectedWorkOut;
        public bool IsSelectedWorkOut
        {
            get => _IsSelectedWorkOut;
            set
            {
                _IsSelectedWorkOut = value;
                OnPropertyChanged("IsSelectedWorkOut");
            }
        }

        private SubjectProgress _SelectedSubject;
        public SubjectProgress SelectedSubject
        {
            get => _SelectedSubject;
            set
            {
                _SelectedSubject = value;
                SelectedSubjectCertification = value?.ToResultCertifications();
                IsSelectedWorkOut =  SelectedSubject.WorkOuts.Count != 0 ? true : false;
                OnPropertyChanged("SelectedSubject");
                OnPropertyChanged("SelectedSubjectCertification");
            }
        }

        public string SelectedSubjectCertification { get; set; }

        public string Login
        {
            get => _currentUser?.Login;
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

        public ICommand GenerateDocCommand
        {
            get
            {
                return new RelayCommand(
                    () => CanGenerateDoc()
                );
            }
        }

        void CanGenerateDoc()
        {
            Services.StudentsDocs.CreateReport(SelectedSubject);
        }

        private string _UserUniversity;
        public string UserUniversity
        {
            get => _UserUniversity;
            set
            {
                _UserUniversity = "University: " + value;
                OnPropertyChanged("UserUniversity");   
            }
        }

        private object ReceiveMessage(SendCurrentUserMessage action)
        {
            CurrentUser = action.CurrentUser;
            UserUniversity = CurrentUser?.Group?.Speciality?.Faculty?.University?.Name;
            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
