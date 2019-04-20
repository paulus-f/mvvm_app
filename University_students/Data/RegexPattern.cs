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
        public static string emailPattern = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
    }
}
