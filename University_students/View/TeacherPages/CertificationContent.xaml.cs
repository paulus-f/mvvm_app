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
    /// Логика взаимодействия для CertificationContent.xaml
    /// </summary>
    public partial class CertificationContent : UserControl
    {
        public CertificationContent(User teacher, Enums.TypeCertifiation tc)
        {
            InitializeComponent();
            DataContext = new ViewModel.TeacherVM.CertificationVM(teacher,tc);
        }
    }
}
