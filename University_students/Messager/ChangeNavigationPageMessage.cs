using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University_students.Models;

namespace University_students.Messager
{
    public class ChangeNavigationPageMessage
    {
        public User CurrentUser { get; set; }
    }
}
