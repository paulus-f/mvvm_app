using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using University_students.Messager;
using University_students.Models;

namespace University_students.ViewModel
{
    class NavigationViewModel
    {
        private UserControl StartUpPage;
        private ContentControl _currentControl;
        private Grid _grid;
        private UserControl _mainCurrentControl;
        public UserControl MainCurrentControl
        {
            get => _mainCurrentControl;
            set
            {
                _mainCurrentControl = value;
                OnPropertyChanged("MainCurrentControl");
            }
        }

        public NavigationViewModel(ContentControl currentControl, Grid GridBarTitle)
        {
            _currentControl = currentControl;
            _grid = GridBarTitle;
            StartUpPage = new View.StartUpPage();
            Color color = (Color)ColorConverter.ConvertFromString("#FF1B3453");
            _grid.Background = new SolidColorBrush(color);
            MainCurrentControl = StartUpPage;
            Messenger.Default.Register<ChangeNavigationPageMessage>(this, (action) => ReceiveMessage(action));
            Messenger.Default.Register<LogOutMessage>(this, (action) => ReceiveMessage(action));
        }

        private object ReceiveMessage(LogOutMessage action)
        {
            Color color = (Color)ColorConverter.ConvertFromString("#FF1B3453");
            _grid.Background = new SolidColorBrush(color);
            StartUpPage = new View.StartUpPage();
            MainCurrentControl = StartUpPage;
            _currentControl.Content = StartUpPage;
            _currentControl.UpdateLayout();
            return null;
        }

        private object ReceiveMessage(ChangeNavigationPageMessage action)
        {
            switch (action.CurrentUser.TypeUser)
            {
                case Enums.Role.Admin:
                    ChangeOnAdminPage(action.CurrentUser);
                    break;
                case Enums.Role.Students:
                    ChangeOnUserPage(action.CurrentUser);
                    break;
                case Enums.Role.Teacher:
                    ChangeOnTeacherPage(action.CurrentUser);
                    break;
            }
            _grid.UpdateLayout();
            return null;
        }

        private object SetCurrentUser(User user)
        {
            var msg = new SendCurrentUserMessage() { CurrentUser = user };
            Messenger.Default.Send<SendCurrentUserMessage>(msg);
            return null;
        }

        private void ChangeOnUserPage(User user)
        {
            Color color = (Color)ColorConverter.ConvertFromString("#FF46B6EA");
            _grid.Background = new SolidColorBrush(color);
            _currentControl.Content = new View.UserPage();
            SetCurrentUser(user);
            _currentControl.UpdateLayout();
        }

        private void ChangeOnAdminPage(User user)
        {
            Color color = (Color)ColorConverter.ConvertFromString("#FF344151");
            _grid.Background = new SolidColorBrush(color);
            _currentControl.Content = new View.AdminPage();
            SetCurrentUser(user);
            _currentControl.UpdateLayout();
        }

        private void ChangeOnTeacherPage(User user)
        {
            Color color = (Color)ColorConverter.ConvertFromString("#F57C00");
            _grid.Background = new SolidColorBrush(color);
            _currentControl.Content = new View.TeacherPage();
            SetCurrentUser(user);
            _currentControl.UpdateLayout();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
