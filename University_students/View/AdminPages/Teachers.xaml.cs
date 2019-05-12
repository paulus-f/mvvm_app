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

namespace University_students.View.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для Teachers.xaml
    /// </summary>
    public partial class Teachers : Page
    {
        public Teachers()
        {
            InitializeComponent();
            vm = new ViewModel.AdminVM.TeacherVM();
            DataContext = vm;
        }

        private ViewModel.AdminVM.TeacherVM vm;

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var binding = ((TextBox)sender).GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
        }

        private void openListGroups(object sender, EventArgs e)
        {
            new CustomBoxes.ChoosingGroups(vm.SelectedTeacherDG).Show();
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

        private void ShowDialog_OnClick(object sender, RoutedEventArgs e)
        {
            DialogHost.IsOpen = true;
        }

        private void CloseDialog_OnClick(object sender, RoutedEventArgs e)
        {
            DialogHost.IsOpen = false;
        }

        private void CheckConnection(object sender, RoutedEventArgs e)
        {
            if (CheckConnection())
            {
                vm.IsNet = true;
            }
            else
            {
                vm.IsNet = false;
                new CustomBoxes.CustomMessageBox("Connection is fail").Show();
            };
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) => subjItems.SelectedItem = null;

        private void SubjItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            subjItems.SelectedItem = null;
            subjItems.SelectedIndex = -1;
        }
    }
}
