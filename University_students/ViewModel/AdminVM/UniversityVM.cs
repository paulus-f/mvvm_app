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
using System.Collections.ObjectModel;

namespace University_students.ViewModel.AdminVM
{
    public class UniversityVM : ViewModelBase, INotifyPropertyChanged
    {
        USDbContext db;

        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged("Login");
            }
        }

        private University _selectedUniversityDG;
        public University SelectedUniversityDG
        {
            get => _selectedUniversityDG;
            set
            {
                _selectedUniversityDG = value;
                Name = value?.Name;
                City = value?.City;
                TypeUniversity = value?.TypeUniversity;
                IsEnabledUD = true;
                OnPropertyChanged("SelectedUniversityDG");
            }
        }

        private DateTime _SelectedFirstAutumnStartDate;
        public DateTime SelectedFirstAutumnStartDate
        {
            get => _SelectedFirstAutumnStartDate;
            set
            {
                _SelectedFirstAutumnStartDate = value;
                OnPropertyChanged("SelectedFirstAutumnStartDate");
            }
        }

        private DateTime _SelectedFirstAutumnFinishDate;
        public DateTime SelectedFirstAutumnFinishDate
        {
            get => _SelectedFirstAutumnFinishDate;
            set
            {
                _SelectedFirstAutumnFinishDate = value;
                OnPropertyChanged("SelectedFirstAutumnFinishDate");
            }
        }


        private DateTime _SelectedLastAutumnStartDate;
        public DateTime SelectedLastAutumnStartDate
        {
            get => _SelectedLastAutumnStartDate;
            set
            {
                _SelectedLastAutumnStartDate = value;
                OnPropertyChanged("SelectedLastAutumnStartDate");
            }
        }

        private DateTime _SelectedLastAutumnFinishDate;
        public DateTime SelectedLastAutumnFinishDate
        {
            get => _SelectedLastAutumnFinishDate;
            set
            {
                _SelectedLastAutumnFinishDate = value;
                OnPropertyChanged("SelectedLastAutumnFinishDate");
            }
        }

        private DateTime _SelectedFirstSpringStartDate;
        public DateTime SelectedFirstSpringStartDate
        {
            get => _SelectedFirstSpringStartDate;
            set
            {
                _SelectedFirstSpringStartDate = value;
                OnPropertyChanged("SelectedFirstSpringStartDate");
            }
        }

        private DateTime _SelectedFirstSpringFinishDate;
        public DateTime SelectedFirstSpringFinishDate
        {
            get => _SelectedFirstSpringFinishDate;
            set
            {
                _SelectedFirstSpringFinishDate = value;
                OnPropertyChanged("SelectedFirstSpringFinishDate");
            }
        }

        private DateTime _SelectedLastSpringStartDate;
        public DateTime SelectedLastSpringStartDate
        {
            get => _SelectedLastSpringStartDate;
            set
            {
                _SelectedLastSpringStartDate = value;
                OnPropertyChanged("SelectedLastSpringStartDate");
            }
        }

        private DateTime _SelectedLastSpringFinishDate;
        public DateTime SelectedLastSpringFinishDate
        {
            get => _SelectedLastSpringFinishDate;
            set
            {
                _SelectedLastSpringFinishDate = value;
                OnPropertyChanged("SelectedLastSpringFinishDate");
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

        private string _city;
        public string City
        {
            get => _city;
            set
            {
                _city = value;
                CanAdd = CheckField();
                OnPropertyChanged("City");
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                CanAdd = CheckField();
                OnPropertyChanged("Name");
            }
        }

        private string _typeUniversity;
        public string TypeUniversity
        {
            get => _typeUniversity;
            set
            {
                _typeUniversity = value;
                CanAdd = CheckField();
                OnPropertyChanged("TypeUniversity");
            }
        }

        private List<University> _listUniversity;
        public List<University> AllUniversities
        {
            get => _listUniversity;
            set
            {
                _listUniversity = value;
                OnPropertyChanged("AllUniversities");
            }
        }

        public UniversityVM()
        {
            db = new USDbContext();
            AllUniversities = db.Universities.ToList();
            SelectedFirstAutumnStartDate = DateTime.Today;
            SelectedFirstAutumnFinishDate = DateTime.Today;
            SelectedFirstSpringFinishDate = DateTime.Today;
            SelectedLastAutumnFinishDate = DateTime.Today;
            SelectedLastSpringStartDate = DateTime.Today;
            SelectedLastSpringFinishDate = DateTime.Today;
            SelectedLastAutumnStartDate = DateTime.Today;
            SelectedFirstSpringStartDate = DateTime.Today;
        }

        public ICommand AddUniversityCommand
        {
            get
            {
                return new RelayCommand(
                    () => CanAddUniversity()
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

        private void CanUpdateFaculty()
        {
            var univ = db.Universities.FirstOrDefault(u => u.Id == SelectedUniversityDG.Id);
            univ.Name = Name;
            univ.City = City;
            univ.TypeUniversity = TypeUniversity;
            univ.Сertification.FirstAutumnStartDate = SelectedFirstAutumnStartDate;
            univ.Сertification.FirstAutumnEndDate = SelectedFirstAutumnFinishDate;
            univ.Сertification.FirstSpringEndDate = SelectedFirstSpringFinishDate;
            univ.Сertification.LastAutumnEndDate = SelectedLastAutumnFinishDate;
            univ.Сertification.LastSpringStartDate = SelectedLastSpringStartDate;
            univ.Сertification.LastSpringEndDate = SelectedLastSpringFinishDate;
            univ.Сertification.LastAutumnStartDate = SelectedLastAutumnStartDate;
            univ.Сertification.FirstSpringStartDate = SelectedFirstSpringStartDate;
            db.SaveChanges();
            AllUniversities = db.Universities.ToList();
            SelectedUniversityDG = null;
            IsEnabledUD = false;
        }

        private bool _canAdd;
        public bool CanAdd
        {
            get => _canAdd;
            set
            {
                _canAdd = value;
                OnPropertyChanged("CanAdd");
            }
        }

        private bool CheckField()
        {
            bool res = true;
            if (String.IsNullOrEmpty(Name)) res = false;
            if (String.IsNullOrEmpty(City)) res = false;
            if (String.IsNullOrEmpty(TypeUniversity)) res = false;
            return res;
        }

        private void CanAddUniversity()
        {
            Сertification newСertification = new Сertification()
            {
                FirstAutumnStartDate = SelectedFirstAutumnStartDate,
                FirstAutumnEndDate = SelectedFirstAutumnFinishDate,
                FirstSpringEndDate = SelectedFirstSpringFinishDate,
                LastAutumnEndDate = SelectedLastAutumnFinishDate,
                LastSpringStartDate =  SelectedLastSpringStartDate,
                LastSpringEndDate = SelectedLastSpringFinishDate,
                LastAutumnStartDate = SelectedLastAutumnStartDate,
                FirstSpringStartDate  = SelectedFirstSpringStartDate
            };

            University newUniversity = new University()
            {
                TypeUniversity = this.TypeUniversity,
                City = this.City,
                Name = this.Name,
                Сertification = newСertification
            };
            
            db.Universities.Add(newUniversity);
            City = String.Empty;
            Name = String.Empty;
            db.SaveChanges();
            AllUniversities = db.Universities.ToList();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}

