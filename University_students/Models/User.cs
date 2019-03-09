using University_students.Enums;

namespace University_students.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public Role TypeUser { get; set; }
    }

}
