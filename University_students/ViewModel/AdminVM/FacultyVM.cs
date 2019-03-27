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
    public class FacultyVM : ViewModelBase, INotifyPropertyChanged
    {
        USDbContext db = new USDbContext();

        private University _selectedUniversityModel;

        private Faculty _selectedFacultyDG;
        public Faculty SelectedFacultyDG
        {
            get => _selectedFacultyDG;
            set
            {
                _selectedFacultyDG = value;
                Name = value?.Name;
                Dean = value?.Dean;
                IsEnabledUD = true;
                OnPropertyChanged("SelectedFacultyDG");
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

        private string _dean;
        public string Dean
        {
            get => _dean;
            set
            {
                _dean = value;
                OnPropertyChanged("Dean");
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

        private List<Faculty> _listFaculties;
        public List<Faculty> AllFaculties
        {
            get => _listFaculties;
            set
            {
                _listFaculties = value;
                OnPropertyChanged("AllFaculties");
            }
        }

        private List<string> _listUniversity;
        public List<string> ListUniversity
        {
            get => _listUniversity;
            set
            {
                _listUniversity = value;
                OnPropertyChanged("ListUniversity");
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
                AllFaculties = _selectedUniversityModel.Faculties.ToList();
                OnPropertyChanged("SelectedUniversity");
            }
        }

        public FacultyVM()
        {
            IsEnabledUD = false;
            ListUniversity = db.Universities.Select(university => university.Name).ToList();
        }

        public ICommand AddFacultyCommand
        {
            get
            {
                return new RelayCommand(
                    () => CanAddFaculty()
                );
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

        public ICommand UpdateCommand
        {
            get
            {
                return new RelayCommand(
                    () => CanUpdateFaculty()
                );
            }
        }

        private void CanDeleteFaculty()
        {
            db.Faculties.Remove(db.Faculties.FirstOrDefault(f => f.Id == SelectedFacultyDG.Id));
            db.SaveChanges();
            AllFaculties = db?.Universities.FirstOrDefault(f => f.Name == _selectedUniversity).Faculties.ToList();
            SelectedFacultyDG = null;
            IsEnabledUD = false;
        }

        private void CanUpdateFaculty()
        {
            db.Faculties.FirstOrDefault((f) => f.Id == SelectedFacultyDG.Id).Name = Name;
            db.SaveChanges();
            AllFaculties = db?.Universities.FirstOrDefault(f => f.Name == _selectedUniversity).Faculties.ToList();
            SelectedFacultyDG = null;
            IsEnabledUD = false;
        }

        private void CanAddFaculty()
        {
            if(_selectedUniversityModel == null)
            {
                return;
            }

            Faculty newFaculty = new Faculty()
            {
                University = this._selectedUniversityModel,
                Name = this.Name
            };

            db.Faculties.Add(newFaculty);
            Name = String.Empty;
            Dean = String.Empty;
            db.SaveChanges();
            AllFaculties = db?.Universities.FirstOrDefault(f => f.Name == _selectedUniversity).Faculties.ToList();
            SelectedFacultyDG = null;
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

