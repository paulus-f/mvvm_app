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
using System.Text.RegularExpressions;

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
                Subjects = value?.Subjects.ToList();
                LoginTeacher = value?.Login;
                FirstNameTeacher = value?.FirstName;
                LastNameTeacher = value?.LastName;
                if (value != null) {
                    ListSubjects = db.Subjects.ToList();
                    List<Subject> ResultSubjects  = new List<Subject>();
                    foreach (var sub in ListSubjects)
                    {
                        bool isFind = false;
                        foreach (var teach in sub.Teachers)
                        {
                            if (teach.Login == value.Login) isFind = true;                                
                        }
                        if (!isFind) ResultSubjects.Add(sub);
                    }
                    IsEnabledUD = true;
                    ListSubjects = ResultSubjects;
                }
                else IsEnabledUD = false;
                OnPropertyChanged("SelectedTeacherDG");
                OnPropertyChanged("SelectedFaculty");
                OnPropertyChanged("SelectedUniversity");
                OnPropertyChanged("SelectedPulpit");
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
                    ListPulpits = db.Pulpits
                        .Join(db.Faculties, p => p.FacultyId, f => f.Id, (p,f) => p)?
                        .Where(p => p.FacultyId == value.Id)
                        .ToList();
                    ListTeachers = db.Users
                       .Include("Pulpit.Faculty")
                       .Where(u => u.Pulpit.Faculty.Name == value.Name)
                       .ToList();
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

        private List<Pulpit> _UnivListPulpits;
        public List<Pulpit> UnivListPulpits
        {
            get => _UnivListPulpits;
            set
            {
                _UnivListPulpits = value;
                OnPropertyChanged("UnivListPulpits");
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
                if (value != null && SelectedTeacherDG != null)
                {

                    ListSubjects.Remove(value);
                    Subjects.Add(value);
                    new CustomBoxes.CustomMessageBox(value.ToString() + " was added").Show();
                    var sub = db.Subjects.FirstOrDefault(s => s.Id == value.Id);
                    var teacher = db.Users.FirstOrDefault(t => t.Id == SelectedTeacherDG.Id);
                    teacher.Subjects.Add(sub);
                    db.SaveChanges();
                }
                if(SelectedTeacherDG == null && value != null)
                    new CustomBoxes.CustomMessageBox("Please selecte a teacher").Show();
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
                    new CustomBoxes.CustomMessageBox(value.ToString() + " was deleted").Show();
                    var teacher = db.Users.FirstOrDefault(t => t.Id == SelectedTeacherDG.Id);
                    teacher.Subjects.Remove(value);
                    db.SaveChanges();
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

        private Pulpit _changePulpit;
        public Pulpit ChangePulpit
        {
            get => _changePulpit;
            set
            {
                _changePulpit = value;
                if (value != null)
                {
                    var user = db.Users.FirstOrDefault(u => u.Id == SelectedTeacherDG.Id);
                    var pulpit = db.Pulpits.FirstOrDefault(p => p.Id == value.Id);
                    user.Pulpit = pulpit;
                    new CustomBoxes.CustomMessageBox("OK").Show();
                    CanResetDG();
                    db.SaveChanges();
                }
                OnPropertyChanged("ChangePulpit");
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
                    UnivListPulpits = db.Pulpits
                        .Include("Faculty.University")
                        .Where(p => p.Faculty.University.Name == SelectedUniversity)
                        .ToList();
                    ListTeachers = db.Users
                        .Where(teacher => teacher.TypeUser == Enums.Role.Teacher &&
                                          teacher.Pulpit.Faculty.University.Id == _selectedUniversityModel.Id)
                        .ToList();
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
            _subjects = new List<Subject>();
            ListSubjects = db.Subjects.ToList();
            ListUniversities = db.Universities.Select(u => u.Name).ToList();
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
                var deleted = context.Users.Include("Teaching.TaughtGroups").Where(t => t.Id == SelectedTeacherDG.Id).FirstOrDefault();
                context.Users.Remove(deleted);
                context.SaveChanges();
                ListTeachers.Remove(SelectedTeacherDG);
                ListTeachers = ListTeachers.ToList();
                SelectedTeacherDG = null;
            }
        }

        private void CanResetDG()
        {
            AddedSubject = null;
            SelectedTeacherDG = null;
            SelectedPulpit = null;
            SelectedFaculty = null;
            ListFaculties = null;
            ListPulpits = null;
            FirstNameTeacher = null;
            LastNameTeacher = null;
            EmailTeacher = null;
            LoginTeacher = null;
            IsEnabledUD = false;
            ListTeachers = db.Users.Where(t => t.TypeUser == Enums.Role.Teacher).ToList();
            ListSubjects = db.Subjects.ToList();
            Subjects = new List<Subject>();
        }

        private void CanInviteTeacher()
        {
            if(EmailTeacher == null)
            {
                new CustomBoxes.CustomMessageBox("Fill fields").Show();
                return;
            }
            if (!new Regex(RegexPattern.emailPattern).IsMatch(EmailTeacher))
            {
                new CustomBoxes.CustomMessageBox("Email is not validated").Show();
                return;
            }
            if (db.Users.FirstOrDefault(t => t.Login == LoginTeacher) != null)
            {
                new CustomBoxes.CustomMessageBox($"Teacher login {LoginTeacher} already exists").Show();
                return;
            }
            if (SelectedPulpit == null)
            {
                new CustomBoxes.CustomMessageBox($"Select Pulpit").Show();
                return;
            }
            if (
                IsNet            == true &&
                LoginTeacher     != null &&
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
                new CustomBoxes.CustomMessageBox("OK").Show();
                SelectedTeacherDG = null;
                FirstNameTeacher = null;
                LastNameTeacher = null;
                EmailTeacher = null;
                LoginTeacher = null;
                IsEnabledUD = false;
                Subjects = new List<Subject>();
            }
            else new CustomBoxes.CustomMessageBox("Fill all fields").Show();
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
            try
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
                    $"<h3> Your login: {LoginTeacher} </h3>" +
                    $"<h3> Your password: {pw} </h3>" +
                    $"";
                message.IsBodyHtml = true;
                smtp.EnableSsl = true;
                smtp.Send(message);
            }
            catch (Exception e)
            {
                new CustomBoxes.CustomMessageBox(e.Message).Show();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}


