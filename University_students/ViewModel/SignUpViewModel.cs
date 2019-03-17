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
using System.Windows.Controls;
using System.Windows.Input;
using University_students.Messager;
using University_students.Models;

namespace University_students.ViewModel
{
    class SignUpViewModel : ViewModelBase, INotifyPropertyChanged, IDataErrorInfo
    {
        USDbContext db;



        public SignUpViewModel()
        {
            _isEnabled = true;
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

        private string _confirmed_password;
        public string ConfirmedPassword
        {
            get { return _confirmed_password; }
            set
            {
                _confirmed_password = value;
                OnPropertyChanged("ConfirmedPassword");
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public ICommand SignUpCommand
        {
            get
            {
                return new RelayCommand(
                    () => CanSignUp()
                );
            }
        }
        private string _error;
        public string Error
        {
            get => _error;
        }

        public string this[string columnName] {
            get
            {
                string msg = null;
                IsEnabled = true;
                Regex regexLogin = new Regex(RegexPattern.loginPattern);
                Regex regexPassword = new Regex(RegexPattern.passwordPattern);
                Regex regexFirstName = new Regex(RegexPattern.firstNamePattern);
                Regex regexLastName = new Regex(RegexPattern.lastNamePattern);
                if (Login == null) return msg;

                switch (columnName)
                {
                    case "Login":
                        if (!regexLogin.IsMatch(Login))
                        {
                            IsEnabled = false;
                            msg = "Login is not validated";
                        }
                        break;
                    case "Password":
                        if (!regexPassword.IsMatch(Password) || Password.Length < 6)
                        {
                            IsEnabled = false;
                            msg = "Password is not validated";
                        }
                        break;
                    case "ConfirmedPassword":
                        if(Password != ConfirmedPassword)
                        {
                            IsEnabled = false;
                            msg = "Confirmed Password is not equal to Password";
                        }
                        break;
                    case "FirstName":
                        if (!regexFirstName.IsMatch(FirstName))
                        {
                            IsEnabled = false;
                            msg = "First Name is not validated";
                        }
                        break;
                    case "LastName":
                        if (!regexLastName.IsMatch(LastName))
                        {
                            IsEnabled = false;
                            msg = "Last Name is not validated";
                        }
                        break;
                }
                return msg;
            }
        }

        private void CanSignUp()
        {
            if(ConfirmedPassword != Password )
            {

                return;
            }
            User newUser = new User()
            {
                Login = _login,
                FirstName = _firstName,
                LastName = _lastName,
                TypeUser = Enums.Role.Students,
                Password = BCrypt.Net.BCrypt.HashPassword(_password)
            };
            db.Users.Add(newUser);
            Login = String.Empty;
            FirstName = String.Empty;
            LastName = String.Empty;
            Password = String.Empty;
            ConfirmedPassword = String.Empty;
            db.SaveChanges();
            GoToUserPage(newUser.Login);
        }

        private object GoToUserPage(string userLogin)
        {
            var msg = new ChangeNavigationPageMessage() { UserLogin = userLogin };
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
