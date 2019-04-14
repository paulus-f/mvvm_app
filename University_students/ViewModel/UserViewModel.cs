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
