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

        private string _city;
        public string City
        {
            get => _city;
            set
            {
                _city = value;
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

        private void CanAddUniversity()
        {
            University newUniversity = new University()
            {
                TypeUniversity = this.TypeUniversity,
                City = this.City,
                Name = this.Name
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

