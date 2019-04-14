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
                if(value != null) ListPulpit = db.Pulpits.Where(p => p.Faculty.Id == value.Id)?.ToList();
                Name = value?.Name;
                Dean = value?.Dean;
                IsEnabledUD = true;
                OnPropertyChanged("SelectedFacultyDG");
            }
        }

        private List<Pulpit> _listPulpit;
        public List<Pulpit> ListPulpit
        {
            get => _listPulpit;
            set
            {
                _listPulpit = value;
                OnPropertyChanged("ListPulpit");
            }
        }

        private Pulpit _seletedPulpit;
        public Pulpit SelectedPulpit
        {
            get => _seletedPulpit;
            set
            {
                _seletedPulpit = value;
                OnPropertyChanged("SelectedPulpit");
            }
        }

        private string _searchPuplpits;
        public string SearchPuplpits
        {
            get => _searchPuplpits;
            set
            {
                _searchPuplpits = value;
                if (value != String.Empty) ListPulpit = db.Pulpits.Where(p => p.FacultyId == SelectedFacultyDG.Id && p.Name.Contains(value)).ToList();
                else ListPulpit = new List<Pulpit>();
                OnPropertyChanged("SearchPuplpits");
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

        private string _namePulpit;
        public string NamePulpit
        {
            get => _namePulpit;
            set
            {
                _namePulpit = value;
                OnPropertyChanged("NamePulpit");
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
                ListPulpit = null;
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

        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(
                    () => CanDeleteFaculty()
                );
            }
        }

        public ICommand AddPulpitCommand
        {
            get
            {
                return new RelayCommand(
                    () => CanAddPulpit()
                );
            }
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

        public ICommand DeletePulpitCommand
        {
            get
            {
                return new RelayCommand<object>(
                    (param) => CanDeletePulpit((Pulpit)param)
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

        private void CanDeletePulpit(Pulpit param)
        {
            db.Pulpits.Remove(param);
            db.SaveChanges();
            ListPulpit = db.Pulpits.ToList();
        }

        private void CanAddPulpit()
        {
            if(NamePulpit != null)
            {
                Pulpit newPulpit = new Pulpit()
                {
                    Name = this.NamePulpit,
                    Faculty = this.SelectedFacultyDG
                };
                db.Pulpits.Add(newPulpit);
                db.SaveChanges();
                ListPulpit = db.Pulpits.Where(p => p.Faculty.Id == _selectedFacultyDG.Id)?.ToList();
            }
            else
            {
                //smth
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
            var faculty = db.Faculties.FirstOrDefault((f) => f.Id == SelectedFacultyDG.Id);
            faculty.Name = Name;
            faculty.Dean = Dean;
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
                Dean = this.Dean,
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

