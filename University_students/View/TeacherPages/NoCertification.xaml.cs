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
    /// Логика взаимодействия для NoCertification.xaml
    /// </summary>
    public partial class NoCertification : UserControl
    {
        public NoCertification(User teacher)
        {
            InitializeComponent();
            Сertification cer = teacher.Pulpit.Faculty.University.Сertification;
            calendar.BlackoutDates.Add(new CalendarDateRange(cer.FirstAutumnStartDate, cer.FirstAutumnEndDate));
            calendar.BlackoutDates.Add(new CalendarDateRange(cer.FirstSpringStartDate, cer.FirstSpringEndDate));
            calendar.BlackoutDates.Add(new CalendarDateRange(cer.LastAutumnStartDate, cer.LastAutumnEndDate));
            calendar.BlackoutDates.Add(new CalendarDateRange(cer.LastSpringStartDate, cer.LastSpringEndDate));
            calendar.UpdateLayout();
        }
    }
}
