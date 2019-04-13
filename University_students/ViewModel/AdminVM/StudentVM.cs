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
    public class StudentVM : ViewModelBase, INotifyPropertyChanged
    {

        private USDbContext db;
        private University _selectedUniversityModel;
        private List<User> _tempResUsers;

        private User _selectedStudentDG;
        public User SelectedStudentDG
        {
            get => _selectedStudentDG;
            set
            {
                _selectedStudentDG = value;
                if (value != null) IsEnabledUD = true;
                else IsEnabledUD = false;
                OnPropertyChanged("SelectedStudentDG");
            }
        }

        private string _searchUsers;
        public string SearchUsers
        {
            get => _searchUsers;
            set
            {
                _searchUsers = value;
                if (value != String.Empty) ListStudents = _tempResUsers?.Where(st => st.FirstName.Contains(value) || st.LastName.Contains(value)).ToList();
                else ListStudents = _tempResUsers.ToList();
                OnPropertyChanged("SearchUsers");
            }
        }

        private List<User> _listStudents;
        public List<User> ListStudents
        {
            get => _listStudents;
            set
            {
                _listStudents = value;
                OnPropertyChanged("ListStudents");
            }
        }

        private List<Group> _listGroups;
        public List<Group> ListGroups
        {
            get => _listGroups;
            set
            {
                _listGroups = value;
                OnPropertyChanged("ListGroups");
            }
        }

        private List<Speciality> _listSpecialities;
        public List<Speciality> ListSpecialities
        {
            get => _listSpecialities;
            set
            {
                _listSpecialities = value;
                OnPropertyChanged("ListSpecialities");
            }
        }

        private Faculty _selectedFaculty;
        public Faculty SelectedFaculty
        {
            get => _selectedFaculty;
            set
            {
                _selectedFaculty = value;
                if (value != null)
                {
                    ListSpecialities = db.Faculties.Join(db.Specialities, f => f.Id, sp => sp.FacultyId, (f, sp) => sp)?.ToList();
                    _tempResUsers = (from u in db.Users
                                    join g in db.Groups on u.GroupId equals g.Id
                                    join sp in db.Specialities on g.SpecialityId equals sp.Id
                                    join f in db.Faculties on sp.FacultyId equals f.Id
                                    where u.TypeUser == Enums.Role.Students && f.Id == value.Id
                                    select u).ToList();
                    ListStudents = _tempResUsers;
                }
                OnPropertyChanged("SelectedFaculty");
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
                    _tempResUsers = (from u in db.Users
                                     join g in db.Groups on u.GroupId equals g.Id
                                     join sp in db.Specialities on g.SpecialityId equals sp.Id
                                     join f in db.Faculties on sp.FacultyId equals f.Id
                                     join un in db.Universities on f.UniversityId equals un.Id
                                     where u.TypeUser == Enums.Role.Students
                                     select u).ToList();
                    ListStudents = _tempResUsers.ToList();
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

        public StudentVM()
        {
            db = new USDbContext();
            ListUniversities = db.Universities.Select(u => u.Name).ToList();
            _tempResUsers = db.Users.Where( st => st.TypeUser == Enums.Role.Students).ToList();
            ListStudents = _tempResUsers.ToList();
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(
                    () => CanDeleteSpeciality()
                );
            }
        }

        private void CanDeleteSpeciality()
        {
            var deleteUser = db.Users.Include("SubjectsProgress.WorkOuts").FirstOrDefault(u => u.Id == _selectedStudentDG.Id);
            db.Users.Remove(deleteUser);
            db.SaveChanges();
            ListStudents = db.Users.Where(st => st.TypeUser == Enums.Role.Students).ToList();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}


