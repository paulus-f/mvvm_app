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
    /// Логика взаимодействия для SubjectsContent.xaml
    /// </summary>
    public partial class AccreditationContent : UserControl
    {
        private ViewModel.TeacherVM.AccreditationVM dc;

        public AccreditationContent(User teacher)
        {
            InitializeComponent();
            dc = new ViewModel.TeacherVM.AccreditationVM(teacher);
            DataContext = dc;
        }

        private void OpenBackgroundStudent(object sender, MouseButtonEventArgs e)
        {
            new CustomBoxes.BackgroundStudentBox(dc.SelectedStudent.User, dc.SelectedStudent).Show();
        }

        private void OpenBackgroundStudentExam(object sender, MouseButtonEventArgs e)
        {
            new CustomBoxes.BackgroundStudentBox(dc.SelectedStudentExam.User, dc.SelectedStudentExam).Show();
        }

        private void FullInfoExam(object sender, MouseButtonEventArgs e)
        {
            new CustomBoxes.CustomMessageBox(dc.SelectedStudentExam.ToString()).Show();
        }
    }
}
