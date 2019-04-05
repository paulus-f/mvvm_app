using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University_students.Models;

namespace University_students.Serializable
{
    public class GroupString
    {
        public GroupString(Group group)
        {
            _grpoup = group;
            _stringGroup = "№ " + group.NumberGroup + " " +
                group.Speciality.Name + " " +
                group.FirstYear;
        }

        private Group _grpoup;
        public Group Group
        {
            get => _grpoup;
        }

        private string _stringGroup;
        public string GroupStr
        {
            get => _stringGroup;
        }

    }
}
