using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University_students.Models;

namespace University_students.Serializable
{
    public class ListGroupSer
    {
        static public List<GroupString> ToListGroup(List<Group> listGroup)
        {
            List<GroupString> res = new List<GroupString>();
            foreach(Group iter in listGroup)
            {
                res.Add(new GroupString(iter));
            }
            return res;
        }
    }

}
