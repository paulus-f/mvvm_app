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

namespace University_students.ViewModel
{
    public class LoginViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private Page LoginPage;
        private Page SignUpPage;

        private Page _currentPage;
        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }

        private double _frameOpacity;
        public double FrameOpacity
        {
            get => _frameOpacity;
            set => _frameOpacity = value;
        }

        public LoginViewModel()
        {
            LoginPage = new View.Login();
            SignUpPage = new View.SignUp();
            FrameOpacity = 1;
            CurrentPage = LoginPage;
        }

        public ICommand LogInCommand
        {
            get
            {
                return new RelayCommand(
                    () => CanLogin()
                );
            }
        }

        private void CanSignUp()
        {
            CurrentPage = SignUpPage;
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

        private void CanLogin()
        {
            CurrentPage = LoginPage;
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
                OnPropertyChanged("Confirmed Password");
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                OnPropertyChanged("Last Name");
            }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("Fisrt Name");
            }
        }

        public void LoginUser()
        {
            //TODO check username and password vs database here.
            //If using membershipprovider then just call Membership.ValidateUser(UserName, Password)
           
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
