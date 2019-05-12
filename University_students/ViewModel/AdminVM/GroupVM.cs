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
    public class GroupVM : ViewModelBase, INotifyPropertyChanged
    {
        USDbContext db;

        private University _selectedUniversityModel;
        private Faculty _selectedFacultyModel;

        public GroupVM()
        {
            FirstYear = 2019;
            NumberGroup = 2;
            IsEnabledUD = false;
            db = new USDbContext();
            ListUniversities = db.Universities.Select(u => u.Name).ToList();
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
                _selectedUniversityModel = db?.Universities.FirstOrDefault(u => u.Name == value);
                ListFaculties = _selectedUniversityModel.Faculties.ToList();
                OnPropertyChanged("SelectedUniversity");
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

        private List<Speciality> _listSpeciality;
        public List<Speciality> ListSpeciality
        {
            get => _listSpeciality;
            set
            {
                _listSpeciality = value;
                OnPropertyChanged("ListSpeciality");
            }
        }

        private Speciality _selectedSpeciality;
        public Speciality SelectedSpeciality
        {
            get => _selectedSpeciality;
            set
            {
                if (value != null)
                {
                    _selectedSpeciality = value;
                    ListGroups = value.Groups.ToList();
                }
                OnPropertyChanged("SelectedSpeciality");
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
                    _selectedFacultyModel = db.Faculties.FirstOrDefault(f => f.Id == value.Id);
                    ListGroups = db.Groups.Where(gr => gr.Speciality.Faculty.Name == value.Name).ToList();
                }
                else
                    _selectedFacultyModel = null; 
                ListSpeciality = _selectedFacultyModel?.Specialites.ToList(); 
                OnPropertyChanged("SelectedFaculty");
            }
        }

        private int _firstYear;
        public int FirstYear
        {
            get => _firstYear;
            set
            {
                _firstYear = value;
                OnPropertyChanged("FirstYear");
            }
        }

        private int _numberGroup;
        public int NumberGroup
        {
            get => _numberGroup;
            set
            {
                _numberGroup = value;
                OnPropertyChanged("NumberGroup");
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

        private Group _selectedGroupsDG;
        public Group SelectedGroupsDG
        {
            get => _selectedGroupsDG;
            set
            {
                _selectedGroupsDG = value;
                if (value != null)
                {
                    FirstYear = value.FirstYear;
                    NumberGroup = value.NumberGroup;
                    IsEnabledUD = true;
                }
                OnPropertyChanged("SelectedGroupsDG");
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(
                    () => CanDeleteFaculty()
                );
            }
        }

        public ICommand AddGroupCommand
        {
            get
            {
                return new RelayCommand(
                    () => CanAddGroup()
                );
            }
        }

        public ICommand UpdateCommand
        {
            get
            {
                return new RelayCommand(
                    () => CanUpdateFaculty()
                );
            }
        }

        private void CanAddGroup()
        {
            if (SelectedSpeciality == null)
            {
                new CustomBoxes.CustomMessageBox("Fill all fields").Show();
                return;
            }
            Group newGroup = new Group()
            {
                Speciality = this.SelectedSpeciality,
                FirstYear = this.FirstYear,
                NumberGroup = this.NumberGroup
            };
            db.Groups.Add(newGroup);
            db.SaveChanges();
            ListGroups = db?.Specialities.FirstOrDefault(s => s.Id == SelectedSpeciality.Id).Groups.ToList();
            IsEnabledUD = false;
        }

        private void CanDeleteFaculty()
        {
            db.Groups.Remove(db.Groups.FirstOrDefault(f => f.Id == SelectedGroupsDG.Id));
            db.SaveChanges();
            ListGroups = db?.Specialities.FirstOrDefault(s => s.Id == SelectedSpeciality.Id).Groups.ToList();
            SelectedGroupsDG = null;
            IsEnabledUD = false;
        }

        private void CanUpdateFaculty()
        {
            db.Groups.FirstOrDefault(g => g.Id == SelectedGroupsDG.Id).FirstYear = FirstYear;
            db.Groups.FirstOrDefault(g => g.Id == SelectedGroupsDG.Id).NumberGroup = NumberGroup;
            db.SaveChanges();
            ListGroups = db?.Specialities.FirstOrDefault(s => s.Id == SelectedSpeciality.Id).Groups.ToList();
            SelectedGroupsDG = null;
            IsEnabledUD = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}


