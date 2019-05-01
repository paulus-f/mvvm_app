using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using University_students.Messager;
using University_students.Models;

namespace University_students.ViewModel
{
    class LoginViewModel : ViewModelBase, INotifyPropertyChanged, IDataErrorInfo
    {
        USDbContext db;

        public LoginViewModel()
        {
            db = new USDbContext();
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }

        private bool _isActiveMessage;
        public bool IsActiveMessage
        {
            get => _isActiveMessage;
            set
            {
                _isActiveMessage = value;
                OnPropertyChanged("IsActiveMessage");
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged("Login");
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(
                    () => CanLogin()
                );
            }
        }

        public ICommand UndoCommand
        {
            get
            {
                return new RelayCommand(
                    () => CanUndoMessage()
                );
            }
        }


        private string _error;
        public string Error
        {
            get => _error;
            set
            {
                if (value != null) IsEnabled = false;
                _error = value;
            }
        }

        public string this[string columnName]
        {
            get
            {
                Regex regexLogin = new Regex(RegexPattern.loginPattern);
                Regex regexPassword = new Regex(RegexPattern.passwordPattern);
                Error = null;
                if (Login == null)
                {
                    IsEnabled = false;
                    return null;
                }
                IsEnabled = CheckField();
                switch (columnName)
                {
                    case "Login":
                        if (!regexLogin.IsMatch(Login))
                        {
                            Error = "Login is not validated";
                        }
                        break;
                    case "Password":
                        if (!regexPassword.IsMatch(Password))
                        {
                            Error = "Password is not validated";
                        }
                        break;
                }
                return Error;
            }
        }

        private void CanUndoMessage()
        {
            IsActiveMessage = false;
            Message = String.Empty;
        }

        private void CanLogin()
        {
            User currentUser = db.Users.FirstOrDefault(user => user.Login == Login);
            if (currentUser != null)
            {
                if (BCrypt.Net.BCrypt.Verify(Password, currentUser.Password) == true)
                {
                    GoToUserPage(currentUser);
                }
                else
                {
                    Message = "Login or Password isn't correctly";
                    IsActiveMessage = true;
                }
            }
            else
            {
                Message = "User don't exist";
                IsActiveMessage = true;
            }
        }

        private bool CheckField()
        {
            bool result = true;
            if (String.IsNullOrEmpty(Login)) result = false;
            if (String.IsNullOrEmpty(Password)) result = false;
            return result;
        }
        private object GoToUserPage(User user)
        {
            var msg = new ChangeNavigationPageMessage() { CurrentUser = user };
            Messenger.Default.Send<ChangeNavigationPageMessage>(msg);
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
