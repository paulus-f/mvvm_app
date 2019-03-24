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
    public class SpecialityVM : ViewModelBase, INotifyPropertyChanged
    {
        USDbContext db = new USDbContext();

        private University _selectedUniversityModel;
        private Faculty _selectedFacultyModel;

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

        private List<Speciality> _specialities;
        public List<Speciality> Specialities
        {
            get => _specialities;
            set
            {
                _specialities = value;
                OnPropertyChanged("Specialities");
            }
        }

        private List<string> _listFaculties;
        public List<string> ListFaculties
        {
            get => _listFaculties;
            set
            {
                _listFaculties = value;
                OnPropertyChanged("ListFaculties");
            }
        }

        private string _qualification;
        public string Qualification
        {
            get => _qualification;
            set
            {
                _qualification = value;
                OnPropertyChanged("Qualification");
            }
        }

        private string _code;
        public string CodeSpec
        {
            get => _code;
            set
            {
                _code = value;
                OnPropertyChanged("CodeSpec");
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
                _selectedUniversityModel = db?.Universities.FirstOrDefault(f => f.Name == value);
                ListFaculties = _selectedUniversityModel.Faculties.Select(f => f.Name).ToList();
                OnPropertyChanged("SelectedUniversity");
            }
        }

        private string _selectedFaculty;
        public string SelectedFaculty
        {
            get => _selectedFaculty;
            set
            {
                _selectedFaculty = value;
                _selectedFacultyModel = db?.Faculties.FirstOrDefault(f => f.Name == value);
                Specialities = _selectedFacultyModel.Specialites.ToList();
                OnPropertyChanged("SelectedFaculty");
            }
        }

        public SpecialityVM()
        {
            ListUniversities = db.Universities.Select(university => university.Name).ToList();
        }

        public ICommand AddSpecialityCommand
        {
            get
            {
                return new RelayCommand(
                    () => CanAddSpeciality()
                );
            }
        }

        private void CanAddSpeciality()
        {
            if (_selectedUniversityModel == null || _selectedFacultyModel == null)
            {
                return;
            }

            Speciality newSpeciality = new Speciality()
            {
                Faculty = this._selectedFacultyModel,
                Name = this.Name,
                Qualification = this.Qualification,
                Code = this.CodeSpec
            };

            db.Specialities.Add(newSpeciality);
            Name = String.Empty;
            Qualification = String.Empty;
            CodeSpec = String.Empty;
            db.SaveChanges();
            Specialities = db?.Faculties.FirstOrDefault(f => f.Name == _selectedFacultyModel.Name).Specialites.ToList();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}


