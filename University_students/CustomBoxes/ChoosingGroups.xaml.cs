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
using University_students.CustomBoxes.ViewModel;
using University_students.Models;

namespace University_students.CustomBoxes
{
    /// <summary>
    /// Логика взаимодействия для ChoosingGroups.xaml
    /// </summary>
    public partial class ChoosingGroups : Window
    {
        public ChoosingGroups(User teacher)
        {
            InitializeComponent();
            this.MouseLeftButtonDown += MouseDragMove;
            DataContext = new ChoosingGroupsVM(teacher);
        }

        private void closeBox(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MouseDragMove(object sender, EventArgs e)
        {
            this.DragMove();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lb1.SelectedIndex = -1;
            lb2.SelectedIndex = -1;
        }
    }
}
