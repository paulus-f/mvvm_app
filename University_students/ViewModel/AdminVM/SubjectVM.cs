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
using University_students.Models;

namespace University_students.ViewModel.AdminVM
{
    public class SubjectVM : ViewModelBase, INotifyPropertyChanged
    {

        private USDbContext db;
        private University _selectedUniversityModel;

        public SubjectVM()
        {
            db = new USDbContext();
            Teachers = new List<User>();
            RangeHours = Enumerable.Range(1, 100).ToArray();
            ListSubjects = db.Subjects.ToList();
            ListUniversities = db.Universities.Select(u => u.Name).ToList();
            ListTeachers = db.Users.Where(t => t.TypeUser == Enums.Role.Teacher).ToList();
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


        private int [] _rangeHours;
        public int [] RangeHours
        {
            get => _rangeHours;
            set
            {
                _rangeHours = value;
                OnPropertyChanged("RangeHours");
            }
        }

        private int _selectedHours;
        public int SelectedHours
        {
            get => _selectedHours;
            set
            {
                _selectedHours = value;
                OnPropertyChanged("SelectedHours");
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private List<User> _teachers;
        public List<User> Teachers
        {
            get => _teachers;
            set
            {
                _teachers = value;
                OnPropertyChanged("Teachers");
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

        private Subject _selectedSubjectDG;
        public Subject SelectedSubjectDG
        {
            get => _selectedSubjectDG;
            set
            {
                _selectedSubjectDG = value;
                Name = value?.Name;
                if (value != null)
                {
                    SelectedHours = value.Hour;
                    ListTeachers = db.Users
                        .Where(u => u.Subjects.All(s => s.Id != value.Id) && u.TypeUser == Enums.Role.Teacher)
                        .ToList();
                    Teachers = (List<User>)value?.Teachers.ToList();
                    IsEnabledUD = true;
                }
                OnPropertyChanged("SelectedSubjectDG");
            }
        }

        private User _addTeacherToSubject;
        public User AddTeacherToSubject
        {
            get => _addTeacherToSubject;
            set
            {
                _addTeacherToSubject = value;
                if (value != null)
                {
                    Teachers.Add(value);
                    Teachers = Teachers.ToList();
                    ListTeachers.Remove(value);
                    ListTeachers = ListTeachers.ToList();
                }
                _addTeacherToSubject = null;
                OnPropertyChanged("AddTeacherToSubject");
            }
        }

        private User _deleteTeacherFromSubject;
        public User DeleteTeacherFromSubject
        {
            get => _deleteTeacherFromSubject;
            set
            {
                if (value != null)
                {
                    ListTeachers.Add(value);
                    ListTeachers = ListTeachers.ToList();
                    Teachers.Remove(value);
                    Teachers = Teachers.ToList();
                }
                _deleteTeacherFromSubject = null;
                OnPropertyChanged("DeleteTeacherFromSubject");
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

        public ICommand AddSubjectCommand
        {
            get
            {
                return new RelayCommand(
                    () => CanAddSubject()
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
                    () => CanDeleteSubject()
                );
            }
        }

        public ICommand UpdateCommand
        {
            get
            {
                return new RelayCommand(
                    () => CanUpdateSubject()
                );
            }
        }

        private void CanUpdateSubject()
        {
            var sub = db.Subjects.FirstOrDefault( s => s.Id == SelectedSubjectDG.Id);
            sub.Name = Name;
            sub.Hour = SelectedHours;
            sub.Teachers = Teachers;
            db.SaveChanges();
            int pos = ListSubjects.IndexOf(SelectedSubjectDG);
            ListSubjects[pos] = sub;
            ListSubjects = ListSubjects.ToList();
            SelectedSubjectDG = null;
            IsEnabledUD = false;
        }

        private void CanDeleteSubject()
        {
            db.Subjects.Remove(SelectedSubjectDG);
            db.SaveChanges();
            ListSubjects.Remove(SelectedSubjectDG);
            ListSubjects = ListSubjects.ToList();
            SelectedSubjectDG = null;
            IsEnabledUD = false;
        }

        private void CanAddSubject()
        {
            if ( Name != null &&
                 Teachers != null
                )
            {

                Subject newSubject = new Subject()
                {
                    Name = this.Name,
                    Hour = this.SelectedHours
                };
                foreach(User u in this.Teachers)
                    newSubject.Teachers.Add(u);
                db.Subjects.Add(newSubject);
                db.SaveChanges();
                ListSubjects = db.Subjects.ToList();
            }
            else
            {
                //smth
            }
        }

        private void CanResetDG()
        {
            SelectedSubjectDG = null;
            Teachers = new List<User>();
            ListFaculties = null;
            Name = null;
            ListSubjects = db.Subjects.ToList();
            ListTeachers = db.Users.Where(t => t.TypeUser == Enums.Role.Teacher).ToList();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}


