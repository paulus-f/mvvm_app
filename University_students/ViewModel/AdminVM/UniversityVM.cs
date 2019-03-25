﻿using System;
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
            db.Universities.Remove(db.Universities.FirstOrDefault(u => u.Id == SelectedUniversityDG.Id));
            db.SaveChanges();
            AllUniversities = db.Universities.ToList();
            SelectedUniversityDG = null;
            IsEnabledUD = false;
        }

        private void CanUpdateFaculty()
        {
            var univ = db.Universities.FirstOrDefault(u => u.Id == SelectedUniversityDG.Id);
            univ.Name = Name;
            univ.City = City;
            univ.TypeUniversity = TypeUniversity;
            db.SaveChanges();
            AllUniversities = db.Universities.ToList();
            SelectedUniversityDG = null;
            IsEnabledUD = false;
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

