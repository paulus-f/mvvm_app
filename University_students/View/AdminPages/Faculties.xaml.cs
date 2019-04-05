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

namespace University_students.View.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для Faculties.xaml
    /// </summary>
    public partial class Faculties : Page
    {
        public Faculties()
        {
            InitializeComponent();
            DataContext = new ViewModel.AdminVM.FacultyVM();
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            var binding = ((TextBox)sender).GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
        }
    }
}
