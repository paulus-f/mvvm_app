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

namespace University_students.ViewModel
{
    class SignUpViewModel : ViewModelBase, INotifyPropertyChanged
    {
        UserContext db;

        public SignUpViewModel()
        {
            db = new UserContext();
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

        private void CanSignUp()
        {
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
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
