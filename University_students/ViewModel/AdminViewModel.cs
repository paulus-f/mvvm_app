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
    public class AdminViewModel : ViewModelBase, INotifyPropertyChanged
    {
        USDbContext db;

        private Page UniversitiesPage;
        private Page FacultiesPage;
        private Page GroupsPage;
        private Page SubjectsPage;
        private Page TeachersPage;
        private Page StudentsPage;
        private Page SpecialitiesPage;

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

        private Page _currentAdminPage;
        public Page CurrentAdminPage
        {
            get => _currentAdminPage;
            set
            {
                _currentAdminPage = value;
                OnPropertyChanged("CurrentAdminPage");
            }
        }


        public AdminViewModel()
        {
            UniversitiesPage = new View.AdminPages.Universities();
            CurrentAdminPage = UniversitiesPage;
            Messenger.Default.Register<SendCurrentUserMessage>(this, (action) => ReceiveMessage(action));
        }

        public ICommand ChooseAdminPageCommand
        {
            get
            {
                return new RelayCommand(
                    () => { }
                );
            }
        }

        public string Login
        {
            get => _currentUser?.Login;
        }

        private object ReceiveMessage(SendCurrentUserMessage action)
        {
            CurrentUser = action.CurrentUser;
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
