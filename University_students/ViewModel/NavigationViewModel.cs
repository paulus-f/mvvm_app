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
        private Page StartUpPage;
        private Page AdminPage;
        private Page UserPage;

        private Page _mainCurrentPage;
        public Page MainCurrentPage
        {
            get => _mainCurrentPage;
            set
            {
                _mainCurrentPage = value;
                OnPropertyChanged("MainCurrentPage");
            }
        }

        public NavigationViewModel()
        {
            StartUpPage = new View.StartUpPage();
            AdminPage = new View.AdminPage();
            UserPage = new View.UserPage();
            MainCurrentPage = StartUpPage;
            Messenger.Default.Register<ChangeNavigationPageMessage> (this, (action) => ReceiveMessage(action));
        }

        private object ReceiveMessage(ChangeNavigationPageMessage action)
        {
            MainCurrentPage = UserPage;
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
