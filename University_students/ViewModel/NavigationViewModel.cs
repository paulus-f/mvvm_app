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
            AdminPage = new View.AdminPage();
            UserPage = new View.UserPage();
            MainCurrentControl = StartUpPage;
            Messenger.Default.Register<ChangeNavigationPageMessage> (this, (action) => ReceiveMessage(action));
        }

        private object ReceiveMessage(ChangeNavigationPageMessage action)
        {
            ChangePage("user");
            return null;
        }

        private void ChangePage(string typePage)
        {
            _currentControl.Content = new View.UserPage();
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
