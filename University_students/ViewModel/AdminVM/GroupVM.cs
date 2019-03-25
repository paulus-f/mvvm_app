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

namespace University_students.ViewModel.AdminVM
{
    public class GroupVM : ViewModelBase, INotifyPropertyChanged
    {

        public GroupVM()
        {
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
        //    db.Faculties.Remove(db.Faculties.FirstOrDefault(f => f.Id == SelectedFacultyDG.Id));
        //    db.SaveChanges();
        //    AllFaculties = db?.Universities.FirstOrDefault(f => f.Name == _selectedUniversity).Faculties.ToList();
        //    SelectedFacultyDG = null;
        //    IsEnabledUD = false;
        }

        private void CanUpdateFaculty()
        {
            //db.Faculties.FirstOrDefault((f) => f.Id == SelectedFacultyDG.Id).Name = Name;
            //db.SaveChanges();
            //AllFaculties = db?.Universities.FirstOrDefault(f => f.Name == _selectedUniversity).Faculties.ToList();
            //SelectedFacultyDG = null;
            //IsEnabledUD = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}


