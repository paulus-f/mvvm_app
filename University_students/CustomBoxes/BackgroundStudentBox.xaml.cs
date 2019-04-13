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
using System.Windows.Shapes;
using University_students.Models;

namespace University_students.CustomBoxes
{
    /// <summary>
    /// Логика взаимодействия для BackgroundStudentBox.xaml
    /// </summary>
    public partial class BackgroundStudentBox : Window
    {
        public BackgroundStudentBox(User stud, SubjectProgress sp)
        {
            InitializeComponent();
            DataContext = new ViewModel.BackgroundStudentVM(stud, sp);

        }

        private void closeBox(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MouseDragMove(object sender, EventArgs e)
        {
            this.DragMove();
        }

    }
}
