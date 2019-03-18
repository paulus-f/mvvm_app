using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_students
{
    class RegexPattern
    {
        public static string loginPattern = @"^[A-Za-z0-9]+$";
        public static string lastNamePattern = @"^(\w+)$";
        public static string firstNamePattern = @"^(\w+)$";
        public static string passwordPattern = @"^[A-Za-z0-9]+$";
    }
}
