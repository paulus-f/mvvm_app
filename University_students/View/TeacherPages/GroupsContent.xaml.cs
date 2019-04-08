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
using University_students.Models;

namespace University_students.View.TeacherPages
{
    /// <summary>
    /// Логика взаимодействия для GroupsContent.xaml
    /// </summary>
    public partial class GroupsContent : UserControl
    {
        public GroupsContent(User teacher)
        {
            InitializeComponent();
            dc = new ViewModel.TeacherVM.GroupsVM(teacher);
            DataContext = dc;
        }

        private ViewModel.TeacherVM.GroupsVM dc;

        private void onClickGroup(object sender, RoutedEventArgs e)
        {
            new CustomBoxes.ListStudentsOfGroup(dc.SelectedGroup.Group).Show();
        }
    }
}
