using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using University_students.ViewModel;

namespace University_students.View
{
    public partial class SignUp : Page
    {
        private SignUpViewModel signUpViewModel;
        public SignUp()
        {
            InitializeComponent();
            signUpViewModel = new SignUpViewModel();
            DataContext = signUpViewModel;
        }

        private void Pass_Changed(object sender, RoutedEventArgs e)
        {
            if(signUpViewModel != null)
                signUpViewModel.Password = pass?.Password;
        }

        private void PassCon_Changed(object sender, RoutedEventArgs e)
        {
            if(signUpViewModel != null)
                signUpViewModel.ConfirmedPassword = passCon?.Password;
        }
    }
}
