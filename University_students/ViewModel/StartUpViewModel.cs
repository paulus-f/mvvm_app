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
    public class StartUpViewModel : ViewModelBase, INotifyPropertyChanged
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

        public StartUpViewModel()
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
