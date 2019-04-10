using System;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows;
using System.Runtime.CompilerServices;
using System.Net;
using University_students.Models;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Web;

namespace University_students.ViewModel.AdminVM
{
    public class TeacherVM : ViewModelBase, INotifyPropertyChanged
    {

        USDbContext db;

        private University _selectedUniversityModel;

        private User _selectedTeacherDG;
        public User SelectedTeacherDG
        {
            get => _selectedTeacherDG;
            set
            {
                _selectedTeacherDG = value;
                LoginTeacher = value?.Login;
                FirstNameTeacher = value?.FirstName;
                LastNameTeacher = value?.LastName;
                SelectedUniversity = value?.Pulpit?.Faculty.University.Name;
                SelectedFaculty = value?.Pulpit?.Faculty;
                SelectedPulpit = value?.Pulpit;
                if (value != null) IsEnabledUD = true;
                else IsEnabledUD = false;
                OnPropertyChanged("SelectedTeacherDG");
            }
        }


        private List<User> _listTeachers;
        public List<User> ListTeachers
        {
            get => _listTeachers;
            set
            {
                _listTeachers = value;
                OnPropertyChanged("ListTeachers");
            }
        }

        private Faculty _selectedFaculty;
        public Faculty SelectedFaculty
        {
            get => _selectedFaculty;
            set
            {
                _selectedFaculty= value;
                if (value != null)
                {
                    ListPulpits = db.Pulpits.Join(db.Faculties, p => p.FacultyId, f => f.Id, (p,f) => p)?.ToList();
                    ListTeachers = (from u in db.Users
                                    join p in db.Pulpits on u.Id equals p.Id
                                    join f in db.Faculties on p.Id equals f.Id
                                    where u.TypeUser == Enums.Role.Teacher && f.Id == value.Id
                                    select u).ToList();
                }
                OnPropertyChanged("SelectedFaculty");
            }
        }

        private List<Pulpit> _listPulpits;
        public List<Pulpit> ListPulpits
        {
            get => _listPulpits;
            set
            {
                _listPulpits = value;
                OnPropertyChanged("ListPulpits");
            }
        }

        private List<Subject> _listSubjects;
        public List<Subject> ListSubjects
        {
            get => _listSubjects;
            set
            {
                _listSubjects = value;
                OnPropertyChanged("ListSubjects");
            }
        }

        private List<Subject> _subjects;
        public List<Subject> Subjects
        {
            get => _subjects;
            set
            {
                _subjects = value;
                OnPropertyChanged("Subjects");
            }
        }

        private Subject _addedSubject;
        public Subject AddedSubject
        {
            get => _addedSubject;
            set
            {
                if (value != null)
                {
                    ListSubjects.Remove(value);
                    Subjects.Add(value);
                }
                ListSubjects = ListSubjects.ToList();
                Subjects = Subjects.ToList();
                _addedSubject = null;
                OnPropertyChanged("AddedSubject");
            }
        }

        private Subject _deletedSubject;
        public Subject DeletedSubject
        {
            get => _deletedSubject;
            set
            {
                if (value != null)
                {
                    Subjects.Remove(value);
                    ListSubjects.Add(value);
                }
                ListSubjects = ListSubjects.ToList();
                Subjects = Subjects.ToList();
                _deletedSubject = null;
                OnPropertyChanged("DeletedSubject");
            }
        }


        private Pulpit _seletedPulpit;
        public Pulpit SelectedPulpit
        {
            get => _seletedPulpit;
            set
            {
                _seletedPulpit = value;
                if( value != null)
                {
                    ListTeachers = db.Users.Where(user => user.Pulpit.Id == value.Id).ToList();
                }
                OnPropertyChanged("SelectedPulpit");
            }
        }

        private string _firstName;
        public string FirstNameTeacher
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstNameTeacher");
            }
        }

        private string _emailTeacher;
        public string EmailTeacher
        {
            get => _emailTeacher;
            set
            {
                _emailTeacher = value;
                OnPropertyChanged("EmailTeacher");
            }
        }

        private string _searchSubjects;
        public string SearchSubjects
        {
            get => _searchSubjects;
            set
            {
                _searchSubjects = value;
                if(value != null) ListSubjects = db.Subjects.Where(sub => sub.Name.Contains(value)).ToList();
                OnPropertyChanged("SearchSubjects");
            }
        }

        private string _lastName;
        public string LastNameTeacher
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged("LastNameTeacher");
            }
        }

        private string _loginTeacher;
        public string LoginTeacher
        {
            get => _loginTeacher;
            set
            {
                _loginTeacher = value;
                OnPropertyChanged("LoginTeacher");
            }
        }

        private List<Faculty> _listFaculties;
        public List<Faculty> ListFaculties
        {
            get => _listFaculties;
            set
            {
                _listFaculties = value;
                OnPropertyChanged("ListFaculties");
            }
        }

        private List<string> _listUniversities;
        public List<string> ListUniversities
        {
            get => _listUniversities;
            set
            {
                _listUniversities = value;
                ListPulpits = null;
                OnPropertyChanged("ListUniversities");
            }
        }

        private string _selectedUniversity;
        public string SelectedUniversity
        {
            get => _selectedUniversity;
            set
            {
                _selectedUniversity = value;
                if (value != null)
                {
                    _selectedUniversityModel = db?.Universities.FirstOrDefault(f => f.Name == value);
                    ListFaculties = _selectedUniversityModel.Faculties.ToList();
                }
                OnPropertyChanged("SelectedUniversity");
            }
        }

        private bool _isEnabledUD;
        public bool IsEnabledUD
        {
            get => _isEnabledUD;
            set
            {
                _isEnabledUD = value;
                OnPropertyChanged("IsEnabledUD");
            }
        }

        public TeacherVM()
        {
            db = new USDbContext();
            //var task = Task.Run(async () => {
            //    for (; ; )
            //    {
            //        await Task.Delay(1000);
            //        IsNet = CheckConnection();
            //    }
            //});
            _subjects = new List<Subject>();
            ListSubjects = db.Subjects.ToList();
            ListUniversities = db.Universities.Select(u => u.Name).ToList();
            ListTeachers = db.Users.Where(t => t.TypeUser == Enums.Role.Teacher).ToList();
        }

        private bool CheckConnection()
        {
            WebClient client = new WebClient();
            try
            {
                using (client.OpenRead("http://www.google.com"))
                    return true;
            }
            catch (WebException)
            {
                return false;
            }
        }

        private bool _isNet;
        public bool IsNet
        {
            get => _isNet;
            set
            {
                _isNet = value;
                OnPropertyChanged("IsNet");
            }
        }

        public ICommand InviteTeacherCommand
        {
            get
            {
                return new RelayCommand(
                    () => CanInviteTeacher()
                );
            }
        }

        public ICommand ResetCommand
        {
            get
            {
                return new RelayCommand(
                    () => CanResetDG()
                );
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(
                    () => CanDeleteTeacher()
                );
            }
        }

        private void CanDeleteTeacher()
        {
            using (var context = new USDbContext())
            {
                var deleted = context.Users.Where(t => t.Id == SelectedTeacherDG.Id).FirstOrDefault();
                if (deleted.Teaching != null )context.Teachings.Remove(deleted.Teaching);
                context.Users.Remove(deleted);
                context.SaveChanges();
                ListTeachers.Remove(SelectedTeacherDG);
                SelectedTeacherDG = null;
            }
        }

        private void CanResetDG()
        {
            SelectedTeacherDG = null;
            SelectedPulpit = null;
            SelectedFaculty = null;
            ListFaculties = null;
            ListPulpits = null;
            SelectedTeacherDG = null;
            FirstNameTeacher = null;
            LastNameTeacher = null;
            EmailTeacher = null;
            LoginTeacher = null;
            IsEnabledUD = false;
            ListTeachers = db.Users.Where(t => t.TypeUser == Enums.Role.Teacher).ToList();
            Subjects = new List<Subject>();
        }

        private void CanInviteTeacher()
        {
            if (LoginTeacher     != null &&
                FirstNameTeacher != null &&
                LastNameTeacher  != null &&
                EmailTeacher     != null
                )
            {
                string password = CreateRandomPassword(10);
                Teaching newTeaching = new Teaching() { };

                User newTeacher = new User()
                {
                    Login = this.LoginTeacher,
                    FirstName = this.FirstNameTeacher,
                    LastName = this.LastNameTeacher,
                    Password = BCrypt.Net.BCrypt.HashPassword(password),
                    Pulpit = this.SelectedPulpit,
                    Teaching = newTeaching,
                    TypeUser = Enums.Role.Teacher
                };
                newTeacher.Subjects = Subjects;
                db.Users.Add(newTeacher);
                sendInviteToMail(password);
                db.SaveChanges();
                SelectedTeacherDG = null;
                FirstNameTeacher = null;
                LastNameTeacher = null;
                EmailTeacher = null;
                LoginTeacher = null;
                IsEnabledUD = false;
                Subjects = new List<Subject>();
                ListTeachers = db.Users.Where(p => p.TypeUser == Enums.Role.Teacher)?.ToList();
            }
            else
            {
                //smth
            }
        }

        private static string CreateRandomPassword(int passwordLength)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            char[] chars = new char[passwordLength];
            Random rd = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        private string GenerateToken(int length)
        {
            RNGCryptoServiceProvider cryptRNG = new RNGCryptoServiceProvider();
            byte[] tokenBuffer = new byte[length];
            cryptRNG.GetBytes(tokenBuffer);
            return Convert.ToBase64String(tokenBuffer);
        }

        private void sendInviteToMail(string pw)
        {
            MailAddress from;
            MailAddress to;
            MailMessage message;
            SmtpClient smtp;
            to = new MailAddress(EmailTeacher);
            smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(Secrets.EMAIL_TO, Secrets.PASSWORD);
            from = new MailAddress(Secrets.EMAIL_TO);
            message = new MailMessage(from, to);
            message.Subject = Secrets.SUBJECT_EMAIL;
            message.Body = $"" +
                $"<h1>Welcome to App University Student</h1>" +
                $"<h2>You was added to database in app</h2>" +
                $"<h3>Your login: {LoginTeacher}</h3>" +
                $"<h3>Your password: {pw}</h3>" +
                $"";
            message.IsBodyHtml = true;
            smtp.EnableSsl = true;
            smtp.Send(message);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}


