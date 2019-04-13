using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// <summary>
    /// Логика взаимодействия для Support.xaml
    /// </summary>
    public partial class Support : Page
    {

        private SupportViewModel dc;
        public Support()
        {
            InitializeComponent();
            dc = new SupportViewModel();
            DataContext = dc;
        }

        private bool CheckConnection()
        {
            WebClient client = new WebClient();
            try
            {
                using (client.OpenRead("http://www.google.com"))
                    return true;
            }
            catch (WebException)
            {
                return false;
            }
        }

        private void CheckConnection(object sender, RoutedEventArgs e)
        {
            if (CheckConnection())
            {
                dc.IsConnected = true;
                new CustomBoxes.CustomMessageBox("Complete").Show();
            }
            else
            {
                dc.IsConnected = false;
                new CustomBoxes.CustomMessageBox("Connection is fail").Show();
            };
        }
    }
}
