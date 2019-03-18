using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using University_students.Messager;
using University_students.Models;

namespace University_students.ViewModel
{
    class NavigationViewModel
    {
        private UserControl StartUpPage;
        private UserControl AdminPage;
        private UserControl UserPage;
        private ContentControl _currentControl;
        
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

        public NavigationViewModel(ContentControl currentControl)
        {
            _currentControl = currentControl;
            StartUpPage = new View.StartUpPage();
            MainCurrentControl = StartUpPage;
            Messenger.Default.Register<ChangeNavigationPageMessage> (this, (action) => ReceiveMessage(action));
        }

        private object ReceiveMessage(ChangeNavigationPageMessage action)
        {
            if(action.CurrentUser.TypeUser == Enums.Role.Students)
            {
                ChangeOnUserPage(action.CurrentUser);
            }
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
            _currentControl.Content = new View.UserPage();
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
