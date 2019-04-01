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
            RangeHours = Enumerable.Range(1, 100).ToArray();
            ListUniversities = db.Universities.Select(u => u.Name).ToList();
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
            get => SelectedHours;
            set
            {
                SelectedHours = value;
                OnPropertyChanged("SelectedHours");
            }
        }

        private string _name;
        public string Name
        {
            get => Name;
            set
            {
                Name = value;
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
                SelectedHours = (int)value?.Hour;
                //Teachers = (List<User>)value?.Teachers;
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
                    _teachers.Add(value);
                    Teachers = _teachers;
                    _listTeachers.Remove(value);
                    ListTeachers = _listTeachers;
                }
                OnPropertyChanged("AddTeacherToSubject");
            }
        }

        private User _deleteTeacherToSubject;
        public User DeleteTeacherToSubject
        {
            get => _deleteTeacherToSubject;
            set
            {
                if (value != null)
                {
                    _listTeachers.Add(value);
                    ListTeachers = _listTeachers;
                    _teachers.Remove(value);
                    Teachers = _teachers;
                }
                _deleteTeacherToSubject = value;
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

        public ICommand InviteTeacherCommand
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
                    () => CanDeleteTeacher()
                );
            }
        }

        private void CanDeleteTeacher()
        {
            //db.Users.Remove(SelectedTeacherDG);
            //db.SaveChanges();
            //ListTeachers.Remove(SelectedTeacherDG);
            //SelectedTeacherDG = null;
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
              //      newSubject.Teachers.Add(u);
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
            ListFaculties = null;
            // ListSubjects = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}


